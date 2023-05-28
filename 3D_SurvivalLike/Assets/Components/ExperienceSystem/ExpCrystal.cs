using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpCrystal : basePickUp
{
    [SerializeField] private int expAmount = 20;

    void Start()
    {
        
    }

    public override void PickUp(Collider other)
    {
        ExperienceManager.Instance.GainExperience(expAmount);
        Destroy(gameObject);
    }

}
