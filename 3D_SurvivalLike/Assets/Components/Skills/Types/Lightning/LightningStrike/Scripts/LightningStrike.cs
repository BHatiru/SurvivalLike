using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningStrike : BaseSkill
{
    [SerializeField] private float _impactRadius;   //radius of the lightning strike hit exploision
    [SerializeField] private float _heightOffset;   //height offset of the lightning strike
    EnemyDetector detector;
    void Start()
    {
        UpdateStats(_SkillData);
        detector = player.GetComponent<EnemyDetector>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= cooldown)
        {
            Cast();
            timer = 0f;
        }
    }
    
    public void Cast(){
        BaseEnemy[] targets = new BaseEnemy[quantity];
        //cast several lightning strikes based on quantity
        for (int i = 0; i < quantity; i++)
        {
            targets[i] = detector.GetRandomEnemy();
            Vector3 enemyPos = targets[i].transform.position;
            //Spawn effect above the enemy
            SpawnLightningStrike(new Vector3(enemyPos.x, enemyPos.y + _heightOffset, enemyPos.z));
            //Damage the target enemy and all enemies in the impact radius
            Collider[] colliders = Physics.OverlapSphere(enemyPos, _impactRadius);
            foreach (Collider col in colliders)
            {
                if (col.GetComponent<BaseEnemy>() != null && col.isTrigger == false)
                {
                    col.GetComponent<BaseEnemy>().TakeDamage(damage);
                }
            }
        }
        
        if (targets == null)
        {
            return;
        }
        
    }

    //method that finds random enemy in the radius



    //method to spawn the lightning strike and destroy the effect after a certain amount of time
    public void SpawnLightningStrike(Vector3 position)
    {
        GameObject lightningStrike = Instantiate(VFX, position, Quaternion.identity);
        Destroy(lightningStrike, duration);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
