using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class inputManager : MonoBehaviour
{
    [SerializeField] PlayerScript playerScript ;
    [SerializeField] FiringBehaviour firingBehaviour ;
    [SerializeField] GameManager gameManager ;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void OnMove(InputAction.CallbackContext context)
    {
        //lis les input 
        Vector2 inputPos = context.ReadValue<Vector2>();
        playerScript.OnMove(inputPos);
    }

    public void OnDash(InputAction.CallbackContext context)
    {
       playerScript.OnDash(context);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        playerScript.OnJump(context);
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        firingBehaviour.OnFire(context);
    }

    public void OnSwing(InputAction.CallbackContext context)
    {
        playerScript.OnSwing(context);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        //lis les input
        Vector2 inputPos = context.ReadValue<Vector2>();
        playerScript.Onlook(inputPos);
    }

    //public void OnPauseMenu(InputAction.CallbackContext context)
    //{
    //    gameManager.OnPauseMenu(context);
    //}


}
