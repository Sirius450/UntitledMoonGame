using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    [SerializeField] SceneSwitcher SceneSwitcher;
    [SerializeField] private int SceneIndex;
    [SerializeField] private bool ChangeScene;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(ChangeScene)
            {
                SceneSwitcher.SwitchScene(SceneIndex);
            }
            else
            {
                SceneSwitcher.RestartScene();
            }
        }
    }
}
