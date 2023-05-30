using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningArc : BaseSkill
{
    [SerializeField] private float damageAmount = 20f;  // Amount of damage to apply
    [SerializeField] private float skillRange = 8f;  // Range of the skill
    [SerializeField] private float rotationSpeed = 4.3f;  // Speed of rotation for the raycast

    private const int SPEED_COEFFICIENT = 20;  // Coefficient for the speed of rotation
    private const float RAYCAST_OFFSET = 0.5f;  // Offset for the raycast source object
    
    /*
    4.297f is the speed of rotation for the raycast that is appoximately equal tothe rotation of arc visual effect rotation of -1,5f 
    */

    //TODO Temporary element needed for development, should be removed later
    public LineRenderer lineRenderer;  // Reference to the LineRenderer component

    public GameObject raycastSource;  // Reference to the raycast source object

    [SerializeField] private GameObject[] _arcPoints;  // Array of points for the arc

    private void Start()
    {
        UpdateStats(_SkillData);
        SetArcPoints();
        VarRename();
        raycastSource.transform.SetParent(transform);
        // Create the raycast source object at the player's position and rotation
        lineRenderer.positionCount = 2;
        lineRenderer.useWorldSpace = true;
    }

    private void SetArcPoints()
    {
        _arcPoints[0].GetComponent<RotationAnime>().orbitSpeed = -1.5f;
        _arcPoints[0].GetComponent<RotationAnime>().radius = 0.5f;

        _arcPoints[3].GetComponent<RotationAnime>().orbitSpeed = -1.5f;
        _arcPoints[3].GetComponent<RotationAnime>().radius = radius;
    }

    private void UpdateArcPoints()
    {
        float rval1 = Random.Range(1.45f, 1.5f);
        float rval2 = Random.Range(-1.45f, -1.5f);
        _arcPoints[1].GetComponent<RotationAnime>().orbitSpeed = rval1;
        _arcPoints[2].GetComponent<RotationAnime>().orbitSpeed = rval2;
    }
    private void VarRename(){
        damageAmount = damage;
        skillRange = radius+RAYCAST_OFFSET;
        rotationSpeed = speed;
    }

    private void Update()
    {

        timer += Time.deltaTime;
        if (timer >= 0.07f)
        {
            UpdateArcPoints();
            timer = 0f;
        }
        //I need to rotate the raycastSource object but independent from the player
        raycastSource.transform.Rotate(Vector3.up, SPEED_COEFFICIENT*rotationSpeed * Time.deltaTime,  Space.Self);
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

