using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowSlash : MonoBehaviour
{
    //TODO Serialized for test purpose, remove later
    private float SlashRange;
    [SerializeField] private GameObject _slashHitEffect;

    private ParticleSystem mainEffect;
    private int index;
    private float effectSize; // slash range to effect size ratio (2.25 : 4.5)

    // Start is called before the first frame update
    void Awake()
    {
        mainEffect = GetComponentInChildren<ParticleSystem>();
        
        //UpdateEffect();
    }

    public void UpdateEffect(int _index){
        index = _index;
        var main = mainEffect.main;
        main.startSize = effectSize* (1+index/5f);
    }

    public void InitializeSlash(Transform center, float slashRange){
        //change the start size of the particle effect based on slash range
        
        effectSize = slashRange * 2;
        //UpdateEffect();
        transform.position = center.position + center.forward * slashRange / 2;
        //get parents rotation
        transform.rotation = center.rotation;
    }

    public void ApplyOffset(float offset, float slashRange){
        //apply offset to the slash proportional to the slash range
        transform.position += transform.forward * offset * slashRange/3;
    }

    public void Cast(float damage, float slashRange){
        SlashRange = slashRange;
        
        //Play particle effect
        mainEffect.Play();
        //skill physics and damaging mechanics
        Collider[] colliders = Physics.OverlapSphere(transform.position, slashRange * (1+index/5f));
        foreach (Collider collider in colliders)
        {
            if (collider.GetComponent<BaseEnemy>() != null && collider.isTrigger == false)
            {
                collider.GetComponent<BaseEnemy>().TakeDamage(damage);
                if (OptimizationSystem.Instance.LimitParticles()) return;
                
                GameObject hitEffect = Instantiate(_slashHitEffect, collider.transform.position, Quaternion.identity, collider.transform);
                hitEffect.GetComponent<ParticleSystem>().Play();
                Destroy(hitEffect, hitEffect.GetComponent<ParticleSystem>().main.duration);
                
                
            }
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, SlashRange* (1+index/5f));
    }
}
