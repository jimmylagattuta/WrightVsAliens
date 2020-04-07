﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathFx;
    [SerializeField] Transform parent;
    // Start is called before the first frame update
    void Start()
    {
        Collider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = false;
        // AddNonTriggerBoxCollider();

    }

    // private void AddNonTriggerBoxCollider()
    // {
    //     // Collider boxCollider = gameObject.AddComponent<BoxCollider>();
    //     Collider boxCollider = gameObject.AddComponent<BoxCollider>();
    //     boxCollider.isTrigger = false;
    // }

    void OnParticleCollision(GameObject other)
    {
        GameObject fx = Instantiate(deathFx, transform.position, Quaternion.identity);
        fx.transform.parent = parent;
        // print("Particles collided with enemy" + gameObject.name);
        Destroy(gameObject);
    }
}
