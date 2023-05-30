using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : BaseSkill
{
    private GameObject fireBall;

    private BaseEnemy[] enemies;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        UpdateStats(_SkillData);
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
        //TODO Spawn projectile around player in a point closest to the target
        fireBall = Instantiate(VFX, transform.position, Quaternion.identity);
        //ProjectileMovement();
    }

    //TODO projectile movement logic
    private void ProjectileMovement(Transform target)
    {
        //rotate to look at the player
        fireBall.transform.LookAt(target.position);
        fireBall.transform.position = Vector3.MoveTowards(fireBall.transform.position, target.position, speed * Time.deltaTime);
    }
    //TODO Add projectile collision logic
}
