using Sirius.Audio;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public static List<Enemy> enemyList = new List<Enemy>();
    public GameObject Sword;
    private bool IsSwing = false;
    [SerializeField] private string audioClipNameSword = "swordSwing";

    private void Start()
    {
        enemyList.Add(this);
    }

    private void OnDestroy()
    {
        enemyList.Remove(this);
    }

    public void OnAttack()
    {
        if (!IsSwing)
        {
            IsSwing = true;
            StartCoroutine(SwordSwing());
        }
    }

    IEnumerator SwordSwing()
    {
        Sword.GetComponent<Animator>().Play("SwordSwing");
        AudioManager.Singleton.PlayAudio(audioClipNameSword);
        yield return new WaitForSeconds(0.4f);
        Sword.GetComponent<Animator>().Play("New State");
        IsSwing = false;
    }

}
