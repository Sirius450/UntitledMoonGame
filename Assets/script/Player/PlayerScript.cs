using Sirius.Audio;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]

    public float walkSpeed = 5f;
    public float sprintSpeed = 2f;
    public float dashForce = 10f;
    public float dashDuration = 0.2f;
    public float turnSpeed = 360f;
    private Camera mainCamera;
    [SerializeField] private LayerMask groundMask;

    public bool IsMoving = false;
    public bool isSprint = false;
    public bool IsSwing = false;
    public bool isDashing = false;
    [SerializeField] private bool _isLookingRight = false;

    private AudioSource audioSource;

    [SerializeField] private string audioClipNameSword = "swordSwing";
    [SerializeField] private string audioClipNameDash = "dash";


    public bool IsLookingRight
    {
        get
        {
            return _isLookingRight;
        }
        set
        {
            _isLookingRight = value;
            //animator.SetBool(AnimationString.isRight, value);
        }

    }
    [SerializeField]
    private bool _isLookingLeft = false;
    public bool IsLookingLeft
    {
        get
        {
            return _isLookingLeft;
        }
        set
        {
            _isLookingLeft = value;
            //animator.SetBool(AnimationString.isRight, value);
        }

    }
    [SerializeField]
    private bool _isLookingUp = false;
    public bool IsLookingUp
    {
        get
        {
            return _isLookingUp;
        }
        set
        {
            _isLookingUp = value;
            //animator.SetBool(AnimationString.isRight, value);
        }

    }
    [SerializeField]
    private bool _isLookingDown = false;
    public bool IsLookingDown
    {
        get
        {
            return _isLookingDown;
        }
        set
        {
            _isLookingDown = value;
            //animator.SetBool(AnimationString.isRight, value);
        }

    }

    public bool DebugMode = false;


    [SerializeField] Rigidbody rb;
    [SerializeField] private TrailRenderer tr;

    PlayerInput input;
    public GameObject Sword;

    Vector3 moveInput;
    Vector2 direction;



    public float CurrentMoveSpeed
    {
        get
        {
            if (IsMoving)
            {
                return walkSpeed;
            }
            else
            {   //idle
                return 0;
            }
        }
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //Look();
        rb.velocity = new Vector3(moveInput.x * CurrentMoveSpeed, rb.velocity.y, moveInput.z * CurrentMoveSpeed);

    }

    public void OnMove(Vector2 inputPos)
    {
        //transforme inputPos en un Vector3
        Vector3 inputPosV3 = new Vector3(inputPos.x, 0, inputPos.y);

        //cree le decalage pour faire le movement isometric
        quaternion rotation = Quaternion.Euler(0, 45, 0);
        Matrix4x4 isoMatrix = Matrix4x4.Rotate(rotation);

        //transfer les movement iso inputPosV3 ver moveInput
        moveInput = isoMatrix.MultiplyPoint3x4(inputPosV3);

        //permet de voir si le joueur bouge
        IsMoving = moveInput != Vector3.zero;

        if (DebugMode)
        {
            Debug.Log($"moveInput {moveInput}");
            Debug.Log($"inputPos {inputPos}");
            Debug.Log($"inputPosV3 {inputPosV3}");
        }

        SetFacingDirection(inputPos);
    }

    public void OnJump(InputAction.CallbackContext context)
    {

    }

    void Look()
    {
        if (moveInput != Vector3.zero)
        {
            var relative = (transform.position + moveInput) - transform.position;
            var rot = Quaternion.LookRotation(relative, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, turnSpeed * Time.deltaTime);
        }
    }

    public void Onlook(Vector2 LookingPoss)
    {
        var (success, position) = GetMousePosition();
        if (success)
        {
            // Calculate the direction
            var direction = position - transform.position;

            // You might want to delete this line.
            // Ignore the height difference.
            direction.y = 0;

            // Make the transform look in the direction.
            transform.forward = direction;
        }
    }

    private (bool success, Vector3 position) GetMousePosition()
    {
        var ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, groundMask))
        {
            // The Raycast hit something, return with the position.
            return (success: true, position: hitInfo.point);
        }
        else
        {
            // The Raycast did not hit anything.
            return (success: false, position: Vector3.zero);
        }
    }


    private void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0)
        {
            //regarde la droite
            IsLookingRight = true;
            IsLookingLeft = false;

        }
        else if (moveInput.x < 0)
        {
            //regarde la gauche
            IsLookingRight = false;
            IsLookingLeft = true;
        }
        else if (moveInput.x == 0)
        {
            IsLookingRight = false;
            IsLookingLeft = false;
        }

        if (moveInput.y > 0)
        {
            //regarde ver le haut
            IsLookingUp = true;
            IsLookingDown = false;

        }
        else if (moveInput.y < 0)
        {
            //regarde ver le bas
            IsLookingUp = false;
            IsLookingDown = true;
        }
        else if (moveInput.y == 0)
        {
            IsLookingUp = false;
            IsLookingDown = false;
        }

    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (!isDashing && IsMoving)
            {
                //met les input du joueur pour la direction du dash
                direction.x = moveInput.x;
                direction.y = moveInput.z;
                rb.velocity = Vector3.zero; // Réinitialise la vitesse du Rigidbody pour éviter les interférences

                StartCoroutine(PerformDash(direction));
            }
            //debugMode pour voir si fonctione
            if (DebugMode)
            { Debug.Log("dash"); }
        }
    }

    public void OnSwing(InputAction.CallbackContext context)
    {
        if (context.started && !IsSwing)
        {
            //audioSource.Play();       FAIRE LES SONS PLUS TARD
            IsSwing = true;
            StartCoroutine(SwordSwing());
        }
    }

    private IEnumerator PerformDash(Vector2 direction)
    {
        //set a on le dash et le trailRenderer
        isDashing = true;
        tr.emitting = true;

        //demarre le timer pour la durer du dash
        float startTime = Time.time;
        //transforme la direction du jouer pour la mettre dans le dash
        Vector3 dashVector = new Vector3(direction.x, 0f, direction.y) * dashForce;
        AudioManager.Singleton.PlayAudio(audioClipNameDash);

        //boucle du dash temp que le timer n'est pas a zero
        while (Time.time < startTime + dashDuration)
        {
            //rb.AddForce(dashVector, ForceMode.VelocityChange);
            rb.velocity = new Vector3(dashVector.x, dashVector.y, dashVector.z);
            yield return null;
        }

        //set a off le dash et le trailRenderer
        isDashing = false;
        tr.emitting = false;
    }

    IEnumerator SwordSwing()
    {
        Sword.GetComponent<Animator>().Play("SwordSwing");
        AudioManager.Singleton.PlayAudio(audioClipNameSword);
        yield return new WaitForSeconds(0.4f);
        Sword.GetComponent<Animator>().Play("New State");
        IsSwing = false;
    }

    //public void OnRun(InputAction.CallbackContext context)
    //{
    //    if (context.started)
    //    {
    //        moveInput.x = moveInput.x * sprintSpeed;
    //        moveInput.y = moveInput.y * sprintSpeed;
    //        moveInput.z = moveInput.z * sprintSpeed;
    //    }
    //}
}
