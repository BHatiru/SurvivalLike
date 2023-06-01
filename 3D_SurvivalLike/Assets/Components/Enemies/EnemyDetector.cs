using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    [SerializeField] private List<BaseEnemy> enemies = new List<BaseEnemy>();
    [SerializeField] private float closestDistance;
    [SerializeField] private BaseEnemy closestEnemy;

    public void FindEnemies(float radius)
    {
        closestDistance = Mathf.Infinity;
        enemies.Clear();
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

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
    //method to get the closest enemy
    public BaseEnemy GetClosestEnemy()
    {
        if (closestEnemy == null)
        {
            return null;
        }
        return closestEnemy;
    }

    //method to get random enemy, used for the lightning strike
    public BaseEnemy GetRandomEnemy()
    {
        if (enemies.Count == 0)
        {
            return null;
        }
        return enemies[Random.Range(0, enemies.Count)];
    }
    //method to get all enemies
    public List<BaseEnemy> GetEnemies()
    {
        return enemies;
    }

    public float GetClosestDistance()
    {
        return closestDistance;
    }

}
