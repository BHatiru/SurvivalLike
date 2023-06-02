using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcyBlizzardCaster : BaseSkill, ICaster
{
    private ParticleSystem _particleSystem;
    [SerializeField] LayerMask _enemyLayer;
    [SerializeField] private float _damageInterval = 1f;

    //radius to effect scale ratio 4.5 : 1     
    protected override void Start()
    {
        
        UpdateStats(_SkillData);
        //if no enemy layer assigned in inspector, get it from the layer mask
        if (_enemyLayer == 0)
        {
            _enemyLayer = LayerMask.GetMask("Enemy");
        }
        base.cooldownDuration = cooldown;
        base.skillDuration = duration;
        _particleSystem = GetComponentInChildren<ParticleSystem>();

        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        if(Input.GetKeyDown(KeyCode.Q)){
            Level++;
            UpdateStats(_SkillData);
            base.cooldownDuration = cooldown;
            base.skillDuration = duration;
        }

    }
    protected override void UpdateActiveState()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            currentState = SkillState.Cooldown;
            timer = cooldownDuration;
            CancelInvoke(nameof(Cast));
        }
    }
    protected override void ActivateSkill()
    {
        _particleSystem.Play();
        float scale = radius / 4.5f;
        _particleSystem.transform.localScale = Vector3.one * scale;
        //periodically cast damage
        InvokeRepeating(nameof(Cast), 0f, _damageInterval);
    }


    public void Cast()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, _enemyLayer);
        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out BaseEnemy enemy) && !collider.isTrigger)
            {
                enemy.TakeDamage(damage);
            }
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
