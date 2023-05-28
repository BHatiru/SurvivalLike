using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class basePickUp : MonoBehaviour
{
    
    [SerializeField] private float _attractionSpeed = 10f;
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Attractor>() != null)
        {
            PickUp(other);
        }
    }
    public void Attract(Vector3 targetPosition)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, _attractionSpeed * Time.deltaTime);
    }
    void Update()
    {
        transform.Rotate(new Vector3(0,0,10) * 10f * Time.deltaTime);
    }
    public virtual void PickUp(Collider other){}
}
