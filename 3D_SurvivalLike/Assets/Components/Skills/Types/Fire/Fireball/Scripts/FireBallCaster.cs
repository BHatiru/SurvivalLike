using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallCaster : BaseSkill, ICaster
{
    [SerializeField] private float spreadAngle;
    private GameObject fireBall;
    //properties public get private set
    public float Speed
    {
        get { return speed; }
        private set { speed = value; }
    }
    public float Damage
    {
        get { return damage; }
        private set { damage = value; }
    }

    private BaseEnemy[] enemies;
    EnemyDetector detector;
    private void Start()
    {
        detector = player.GetComponent<EnemyDetector>();
        UpdateStats(_SkillData);
        Damage = damage;
        Speed = speed;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Level++;
            UpdateStats(_SkillData);
        }
        detector.FindEnemies(radius);
        cooldownTimer += Time.deltaTime;
        if (cooldownTimer >= cooldown)
        {
            Cast();
        }
        
    }

    public void Cast()
    {
        BaseEnemy target = detector.GetClosestEnemy();
        if (target == null)
        {
            return;
        }
        Vector3 enemyPos = target.transform.position;
        //Spawns several projectiles based on quantity, rotate towards enemy, spread them in a given angle evenly
        for (int i = 0; i < quantity; i++)
        {
            Quaternion rotation = Quaternion.Euler(0, ((spreadAngle*i)/quantity), 0);
            //add additional rotation to point the fireball towards the enemy
            Quaternion lookRotation = Quaternion.LookRotation(enemyPos - transform.position);
            rotation = rotation * lookRotation;
            //set the vertical rotation to 0 to prevent the fireball from rotating up and down
            rotation = Quaternion.Euler(0, rotation.eulerAngles.y, rotation.eulerAngles.z);
            SpawnFireBall(enemyPos, rotation);
        }

        cooldownTimer = 0f;
    }
    private void SpawnFireBall(Vector3 enemyPos, Quaternion rotation){
        fireBall = Instantiate(VFX_skillObj, transform.position, rotation);
        fireBall.GetComponent<Collider>().enabled = true;
        
    }

}
