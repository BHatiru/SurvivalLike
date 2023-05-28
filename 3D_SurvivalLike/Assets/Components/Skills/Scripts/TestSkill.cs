using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSkill : BaseSkill
{
    [SerializeField] private SkillData _SkillData;
    [SerializeField] private float slashRange;
    [SerializeField] private GameObject slashHitEffect;

    private ParticleSystem mainEffect;
    private GameObject skillHolder;
    // Start is called before the first frame update
    void Start()
    {
        UpdateStats(_SkillData);
        mainEffect = GetComponentInChildren<ParticleSystem>();
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= cooldown)
        {
            Cast();
        }
    }

    void UpdateLevel(){
        Level++;
        UpdateStats(_SkillData);
        // custom changes
    }

    private void Cast(){
        Debug.Log("Cast");
        //Play particle effect
        mainEffect.Play();
        //skill physics and damaging mechanics
        Collider[] colliders = Physics.OverlapSphere(transform.position, slashRange);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.tag == "Enemy")
            {
                collider.GetComponent<BaseEnemy>().TakeDamage(damage);
                GameObject hitEffect = Instantiate(slashHitEffect, collider.transform.position, Quaternion.identity);
                hitEffect.GetComponent<ParticleSystem>().Play();
                Destroy(hitEffect, hitEffect.GetComponent<ParticleSystem>().main.duration);
            }
        }
        timer = 0f;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, slashRange);
    }
}
