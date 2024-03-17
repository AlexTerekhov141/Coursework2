using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParicleCollector : MonoBehaviour
{
    private ParticleSystem ps;
    List<ParticleSystem.Particle> _particles = new List<ParticleSystem.Particle>();
    [SerializeField] private float exp;
    private PlayerController _controller;
    private void Start()
    {
        Transform triggerTarget = GameObject.FindGameObjectWithTag("Collectable").transform;
        ps = GetComponent<ParticleSystem>();
        ps.trigger.AddCollider(triggerTarget);
        _controller = FindObjectOfType<PlayerController>();
        
    }

    private void OnParticleTrigger()
    {
        
        int trigerParticles = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, _particles);
        for (int i = 0; i < trigerParticles; i++)
        {
            ParticleSystem.Particle p = _particles[i];
            p.remainingLifetime = 0;
            Debug.Log("We Collected 1 particle");
            _particles[i] = p;
            if (_controller != null)
            {
                _controller.changeExp(exp);
            }
        }
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, _particles);
        Destroy(gameObject);
    }
}
