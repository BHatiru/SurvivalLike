using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningArc : MonoBehaviour
{
    private const int SPEED_COEFFICIENT = 20;  // Coefficient for the speed of rotation
    [SerializeField] private float rayOffset = 0.5f; 
    [SerializeField] private Transform _arcEndPoint;

    //TODO Temporary element needed for development, should be removed later
    [SerializeField] private LineRenderer lineRenderer;  // Reference to the LineRenderer component
    [SerializeField] private GameObject _hitEffect;  // Reference to the hit effect object

    private void Start()
    {     
        // Create the raycast source object at the player's position and rotation
        lineRenderer.positionCount = 2;
        lineRenderer.useWorldSpace = true;
    }

    public void Cast(float rotationSpeed, float radius, float damage){
        radius += rayOffset;
        transform.Rotate(Vector3.up, SPEED_COEFFICIENT*rotationSpeed * Time.deltaTime,  Space.Self);
        // Fire a raycast from the raycast source in the forward direction
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, radius))
        {
            // Check if the raycast hit an enemy
            BaseEnemy enemy = hit.collider.GetComponent<BaseEnemy>();
            if (enemy != null)
            {
                GameObject hitEffect = Instantiate(_hitEffect, hit.point, Quaternion.identity);
                hitEffect.GetComponent<ParticleSystem>().Play();
                Destroy(hitEffect, 0.5f);
                // Apply damage to the enemy
                enemy.TakeDamage(damage);
            }
            //TODO: Anything below is for development purposes, should be removed later
            lineRenderer.SetPosition(0, ray.origin);
            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            // If the raycast didn't hit anything, set the endpoint to the maximum range
            Vector3 endpoint = ray.origin + ray.direction * radius;
            lineRenderer.SetPosition(0, ray.origin);
            lineRenderer.SetPosition(1, endpoint);
        }
    }

    public void SetArcRadius(float radius)
    {
        // set the end point of the arc to the radius, dont change the y position
        _arcEndPoint.localPosition = Vector3.forward * radius*0.5f; 
        _arcEndPoint.localPosition = new Vector3(_arcEndPoint.localPosition.x, 0, _arcEndPoint.localPosition.z);
    }
    public void SetArcRotation(Quaternion rotation)
    {
        // set the rotation of the arc to the rotation of the player
        transform.rotation = rotation;
    }

    public void DestroyArc()
    {
        Destroy(gameObject);
    }

}

