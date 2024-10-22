using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Threading;
using Sirius.Audio;
using TMPro;
using UnityEngine.TextCore.LowLevel;

public class FiringBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject moonBeam;
    [SerializeField] private Transform projectileFiringPoint;
    [SerializeField] private float intenseShake = 5f;
    [SerializeField] private float timeShake = 0.1f;
    [SerializeField] private float coolDown = 0.5f;
    

    double nbProjectile = 0;
    public float timeRune = 0.2f;
    float timerRune;
    public float rotationSpeed = 10f;
    public float x = 0;
    public float y = 0;
    public float z = 0;
    private bool canshot = true;

    //For pool
    [SerializeField] private int poolSize = 5;
    private Queue<GameObject> pool = new Queue<GameObject>();
    private GameObject poolParent;

    private void Start()
    {
        poolParent = new GameObject();
        poolParent.name = "Projectile Pool";

        CreateProjectile();
    }

    private void CreateProjectile()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject projectile = Instantiate(projectilePrefab);
            projectile.SetActive(false);
            projectile.transform.parent = poolParent.transform;
            ReturnToPool(projectile);

            projectile.GetComponent<Projectile>().OnDeactivate += ReturnToPool;
        }
    }

    private void ReturnToPool(GameObject projectile)
    {
        pool.Enqueue(projectile);
    }

    private GameObject GetFromPool()
    {
        if (pool.Count == 0)
        {
            CreateProjectile();
        }

        return pool.Dequeue();
    }


    public void OnFire(InputAction.CallbackContext context)
    {
        y = transform.eulerAngles.y;
        Vector3 dir = new Vector3(0, y, 0);

        //Debug.Log(nbProjectile);
        CinamaChineShake.Instance.OnShakeCamera(intenseShake, timeShake);

        if (context.started && canshot)
        {
            //Instantiate(projectile, projectileFiringPoint.position, Quaternion.Euler(dir));

            AudioManager.Singleton.PlayAudio("fireBallCast");
            
            nbProjectile++;


            GameObject projectile = GetFromPool();
            projectile.transform.position = projectileFiringPoint.position;
            projectile.transform.rotation = Quaternion.Euler(dir);
            projectile.SetActive(true);


            //StartCoroutine(CoolDown());
            StartCoroutine(Rune());
        }

    }

    IEnumerator Rune()
    {
        timerRune = Time.time;

        while (Time.time < timerRune + timeRune)
        {
            moonBeam.SetActive(true);
            canshot = false;
            yield return null;
        }
        moonBeam.SetActive(false);
        canshot = true;

    }

    IEnumerable CoolDown()
    {
        canshot = false;
        yield return coolDown;
        canshot = true;
    }
}
