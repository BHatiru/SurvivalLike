using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    [SerializeField] private List<BaseEnemy> enemies = new List<BaseEnemy>();
    [SerializeField] private float closestDistance;
    [SerializeField] private BaseEnemy closestEnemy;

    [SerializeField] private float _detectionRadius = 40f;

    private void FixedUpdate()
    {
        //TODO add detection logic using overlapshere
        FindEnemies();
    }

    private void FindEnemies()
    {
        closestDistance = Mathf.Infinity;
        enemies.Clear();
        Collider[] colliders = Physics.OverlapSphere(transform.position, _detectionRadius);

        foreach (Collider col in colliders)
        {
            //check if the collider is an enemy and not the trigger collider
            if (col.GetComponent<BaseEnemy>() != null && col.isTrigger == false)
            {
                enemies.Add(col.GetComponent<BaseEnemy>());
                
                if (enemies.Count > 0)
                {
                    float distance = Vector3.Distance(transform.position, col.transform.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestEnemy = col.GetComponent<BaseEnemy>();
                    }
                    
                }
            }
        }
    }

    public BaseEnemy GetEnemy()
    {
        if (closestEnemy == null)
        {
            return null;
        }
        return closestEnemy;
    }

    public List<BaseEnemy> GetEnemies()
    {
        return enemies;
    }

    public float GetClosestDistance()
    {
        return closestDistance;
    }

}
