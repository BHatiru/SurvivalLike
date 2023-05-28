using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartCrystal : basePickUp
{
    [SerializeField] private int healAmount = 50;

    public override void PickUp(Collider other)
    {
        other.GetComponent<PlayerHealth>().Heal(healAmount);
        Destroy(gameObject);
    }
}
