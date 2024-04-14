using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class magnit : MonoBehaviour
{
    public GameObject _magnit;
    private ParticleSystemForceField _force;
    private float originalEndRange;

    private void Start()
    {
        _magnit = GameObject.FindGameObjectWithTag("Collectable");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController controller = other.GetComponent<PlayerController>();

        if (controller != null)
        {   
            _force = _magnit.GetComponent<ParticleSystemForceField>();
            originalEndRange = _force.endRange; // Сохраняем изначальное значение

            _force.endRange += 10f;
            Destroy(gameObject);
        }
    }

    
}