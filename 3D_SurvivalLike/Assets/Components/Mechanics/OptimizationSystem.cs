using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptimizationSystem : MonoBehaviour
{
    public static OptimizationSystem Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }else
        {
            Destroy(this.gameObject);
        }
    }
    public bool LimitParticles(){
        return FindObjectsOfType<ParticleSystem>().Length > 100;
    }
}
