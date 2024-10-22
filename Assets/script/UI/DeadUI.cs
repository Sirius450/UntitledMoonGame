using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class DeadUI : MonoBehaviour
{
    [SerializeField] int sceneIndex = 1;
    [SerializeField] SceneSwitcher sceneSwitcher;

    private void Start()
    {
        this.gameObject.SetActive(false);
    }
    
    public void OnContinue(InputAction.CallbackContext context)
    {
        if(context.performed && this.gameObject.active)
        {
            Time.timeScale = 1f;
            sceneSwitcher.SwitchScene(sceneIndex);
        }

    }


    public void Dead()
    {
        this.gameObject.SetActive(true);
    }
}
