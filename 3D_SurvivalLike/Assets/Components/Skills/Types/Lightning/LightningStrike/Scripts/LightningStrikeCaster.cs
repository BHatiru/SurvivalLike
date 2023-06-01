using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningStrikeCaster : BaseSkill, ICaster
{
    [SerializeField] private float _impactRadius;   //radius of the lightning strike hit exploision
    [SerializeField] private float _heightOffset;   //height offset of the lightning strike
    EnemyDetector detector;
    private float distance;
    void Start()
    {
        UpdateStats(_SkillData);
        //create a new enemy detector isntance and set its position to the player position
        detector = gameObject.AddComponent<EnemyDetector>();
        detector.transform.position = transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            UpdateStats(_SkillData);
        }

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        detector.FindEnemies(radius);
        timer += Time.deltaTime;
        if (timer >= cooldown)
        {
            Cast();
            timer = 0f;
        }
    }

    public void Cast()
    {
        //cast several lightning strikes based on quantity with small delay between each strike
        StartCoroutine(ConsequtiveDelayedStrikes(0.2f));
    }

    //method to delay the lightning strike
    public IEnumerator ConsequtiveDelayedStrikes(float delay)
    {
        for (int i = 0; i < quantity; i++)
        {
            BaseEnemy target = detector.GetRandomEnemy();
            if (target == null)
            {
                yield break;
            }
            distance = Vector3.Distance(transform.position, target.transform.position);
            Vector3 enemyPos = target.transform.position;
            //Spawn effect above the enemy
            SpawnLightningStrike(new Vector3(enemyPos.x, enemyPos.y + _heightOffset, enemyPos.z));
            //Damage the target enemy and all enemies in the impact radius
            Collider[] colliders = Physics.OverlapSphere(enemyPos, _impactRadius);
            foreach (Collider col in colliders)
            {
                if (col.GetComponent<BaseEnemy>() != null && col.isTrigger == false)
                {
                    col.GetComponent<BaseEnemy>().TakeDamage(damage, 0.2f);
                }
            }
            yield return new WaitForSeconds(delay);
        }

    }



    //method to spawn the lightning strike and destroy the effect after a certain amount of time
    public void SpawnLightningStrike(Vector3 position)
    {
        GameObject lightningStrike = Instantiate(VFX_skillObj, position, Quaternion.identity);
        Destroy(lightningStrike, duration);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
