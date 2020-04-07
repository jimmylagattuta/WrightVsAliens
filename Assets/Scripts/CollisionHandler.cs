using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // ok as long as this is the only script that loads scenes

public class CollisionHandler : MonoBehaviour
{
    [Tooltip("In seconds")] [SerializeField] float levelLoadDelay = 1f;
    [Tooltip("Fx prefab on player")] [SerializeField] GameObject deathFx;


    void OnTriggerEnter(Collider other)
    {
            // print("OnTriggerEnter");
            StartDeathSequence();
            deathFx.SetActive(true);
            Invoke("ReloadScene", levelLoadDelay);
        
    }
    private void StartDeathSequence()
    {
        SendMessage("OnPlayerDeath");
    }
    private void ReloadScene()
    {
        SceneManager.LoadScene(1);
    }
}