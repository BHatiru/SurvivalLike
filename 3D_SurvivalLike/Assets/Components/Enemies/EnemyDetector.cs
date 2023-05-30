using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    public List<BaseEnemy> enemies = new List<BaseEnemy>();
    public float closestDistance = Mathf.Infinity; 
    public BaseEnemy closestEnemy;

    [SerializeField] private float _detectionRadius = 40f;

    private void Update()
    {
        //TODO add detection logic using overlapshere
        FindEnemies();
    }

    private void FindEnemies(){
        Collider[] colliders = Physics.OverlapSphere(transform.position, _detectionRadius);
        
        foreach (Collider col in colliders)
        {
            if (col.GetComponent<BaseEnemy>() != null)
            {
                enemies.Add(col.GetComponent<BaseEnemy>());
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
