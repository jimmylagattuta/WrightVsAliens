using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        print("OnTriggerEnter");
        StartDeathSequence();
    }
    private void StartDeathSequence()
    {
        print("player dying");
        SendMessage("OnPlayerDeath");
    }
}