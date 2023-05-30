using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningArc : BaseSkill
{
    [SerializeField] private float damageAmount = 20f;  // Amount of damage to apply
    [SerializeField] private float skillRange = 8f;  // Range of the skill
    public float rotationSpeed = 4.3f;  // Speed of rotation for the raycast


    private const int SPEED_COEFFICIENT = 20;  // Coefficient for the speed of rotation

    //TODO Temporary element needed for development, should be removed later
    [SerializeField] private LineRenderer lineRenderer;  // Reference to the LineRenderer component

    [SerializeField] private GameObject raycastSource;  // Reference to the raycast source object

    [SerializeField] private GameObject _hitEffect;  // Reference to the hit effect object

    private void Start()
    {
        UpdateStats(_SkillData);
       // SetArcPoints();
        VarRename();
        raycastSource.transform.SetParent(transform);
        // Create the raycast source object at the player's position and rotation
        lineRenderer.positionCount = 2;
        lineRenderer.useWorldSpace = true;
    }

    private void VarRename(){
        damageAmount = damage;
        skillRange = radius;
        rotationSpeed = speed;
    }

    private void Update()
    {

        timer += Time.deltaTime;
        if (timer >= 0.07f)
        {
            //UpdateArcPoints();
            timer = 0f;
        }
        Cast();
    }

    private void Cast(){
                //I need to rotate the raycastSource object but independent from the player
        raycastSource.transform.Rotate(Vector3.up, SPEED_COEFFICIENT*rotationSpeed * Time.deltaTime,  Space.Self);
        // Fire a raycast from the raycast source in the forward direction
        Ray ray = new Ray(raycastSource.transform.position, raycastSource.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, skillRange))
        {
            // Check if the raycast hit an enemy
            BaseEnemy enemy = hit.collider.GetComponent<BaseEnemy>();
            if (enemy != null)
            {
                GameObject hitEffect = Instantiate(_hitEffect, hit.point, Quaternion.identity);
                hitEffect.GetComponent<ParticleSystem>().Play();
                Destroy(hitEffect, 0.5f);
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

