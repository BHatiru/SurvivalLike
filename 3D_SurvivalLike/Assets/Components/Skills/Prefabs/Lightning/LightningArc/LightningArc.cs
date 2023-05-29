using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningArc : MonoBehaviour
{
    public float damageAmount = 10f;  // Amount of damage to apply
    public float skillRange = 10f;  // Range of the skill
    public float rotationSpeed = 10f;  // Speed of rotation for the raycast

    public LineRenderer lineRenderer;  // Reference to the LineRenderer component

    public GameObject raycastSource;  // Reference to the raycast source object

    private void Start()
    {
        raycastSource.transform.SetParent(transform);
        // Create the raycast source object at the player's position and rotation
        lineRenderer.positionCount = 2;
        lineRenderer.useWorldSpace = true;
    }
    private void Update()
    {
        //I need to rotate the raycastSource object but independent from the player
        raycastSource.transform.Rotate(Vector3.up, 20*rotationSpeed * Time.deltaTime,  Space.Self);
        // Fire a raycast from the raycast source in the forward direction
        Ray ray = new Ray(raycastSource.transform.position, raycastSource.transform.right);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, skillRange))
        {
            // Check if the raycast hit an enemy
            BaseEnemy enemy = hit.collider.GetComponent<BaseEnemy>();
            if (enemy != null)
            {
                // Apply damage to the enemy
                enemy.TakeDamage(damageAmount);
            }
            lineRenderer.SetPosition(0, ray.origin);
            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            // If the raycast didn't hit anything, set the endpoint to the maximum range
            Vector3 endpoint = ray.origin + ray.direction * skillRange;
            lineRenderer.SetPosition(0, ray.origin);
            lineRenderer.SetPosition(1, endpoint);
        }
    }
}

