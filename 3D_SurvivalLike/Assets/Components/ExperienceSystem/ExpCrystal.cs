using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpCrystal : basePickUp
{
    [SerializeField] public float expAmount = 20;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>

    public override void PickUp(Collider other)
    {
        ExperienceManager.Instance.GainExperience(expAmount);
        Destroy(gameObject);
    }

}
