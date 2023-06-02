using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireZone : MonoBehaviour
{
    [SerializeField] private float fireZoneRadius; // 4.5 : 1 ratio to scale
    [SerializeField] private float damageInterval = 1f;
    [SerializeField] private float damage = 1f;
    [SerializeField] private float duration = 5f;
    [SerializeField] LayerMask enemyLayer;
    private float timer;
    void Start()
    {
        timer = damageInterval;
        if (enemyLayer == 0)
        {
            enemyLayer = LayerMask.GetMask("Enemy");
        }
        Destroy(gameObject, duration);
    }

    public void SetParameters(float radius, float damage, float duration, float damageInterval){
        this.fireZoneRadius = radius * Random.Range(0.75f, 1.35f);
        this.damage = damage;
        this.duration = duration;
        this.damageInterval = damageInterval;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            
            Collider[] enemies = Physics.OverlapSphere(transform.position, fireZoneRadius, enemyLayer);
            foreach (Collider enemy in enemies)
            {
                if (enemy.GetComponent<BaseEnemy>() != null && !enemy.isTrigger) 
                enemy.GetComponent<BaseEnemy>().TakeDamage(damage);
            }
            timer = damageInterval;
        }
        transform.localScale = Vector3.one * fireZoneRadius / 4.5f;
    }

}
