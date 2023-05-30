using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallCaster : BaseSkill
{
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
        player = GameObject.FindGameObjectWithTag("Player").transform;
        detector = player.GetComponent<EnemyDetector>();
        UpdateStats(_SkillData);
        Damage = damage;
        Speed = speed;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= cooldown)
        {
            Cast();
        }
        
    }

    private void Cast()
    {
        //Spawns projectile and rotate towards enemy
        fireBall = Instantiate(VFX, transform.position, Quaternion.identity);
        fireBall.GetComponent<Collider>().enabled = true;
        var enemyPos = detector.GetEnemy().transform.position;
        //rotate towards enemy, only horizontally
        fireBall.transform.LookAt(new Vector3(enemyPos.x, fireBall.transform.position.y, enemyPos.z));
        BaseEnemy target = detector.GetEnemy();
        timer = 0f;
    }

}
