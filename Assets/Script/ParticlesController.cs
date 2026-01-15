using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesController : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private int particlesCount = 10;

    public void EmitParticles()
    {
        particleSystem.Emit(particlesCount);
    }
}
