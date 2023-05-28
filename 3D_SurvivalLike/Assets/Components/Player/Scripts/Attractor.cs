using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    [SerializeField] private float _attractionRadius = 7f;
    private void FixedUpdate()
    {
        Attract();
    }
    private void Attract()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _attractionRadius);
        foreach(Collider collider in colliders)
        {
            if(collider.GetComponent<basePickUp>() != null)
            {
                collider.GetComponent<basePickUp>().Attract(transform.position);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _attractionRadius);
    }
}
