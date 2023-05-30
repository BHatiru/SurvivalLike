using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FireBallProjectile : MonoBehaviour
{
    [SerializeField] private GameObject _hitPrefab;
    [SerializeField] private float _explosionRadius = 1f;
    private Rigidbody rb;
    private FireBallCaster _caster;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody> ();
        //find caster script  
        _caster = FindObjectOfType<FireBallCaster>();
        Destroy(gameObject, 10f);
    }

    private void FixedUpdate()
    {
        
        MoveProjectile();
    }
    private void MoveProjectile()
    {
        if (_caster.Speed != 0 && rb != null)
			rb.position += (transform.forward) * (_caster.Speed * Time.deltaTime);  
    }

    //colision logic
    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<BaseEnemy>(out BaseEnemy enemy) && other.isTrigger == false)
        {
            // Debug.Log("Enemy hit");
            
            // Debug.Log("Tried to damage enemy");
            // Debug.Log("Colided object: " + enemy);
            enemy.TakeDamage(_caster.Damage);
            
            Vector3 pos = enemy.transform.position;
            if (_hitPrefab != null)
            {
                GameObject hitEffect = Instantiate(_hitPrefab, pos, Quaternion.identity);
                hitEffect.GetComponent<ParticleSystem>().Play();
                Destroy(hitEffect, 1f);
            }
            //deal damage to enemy hit and in small area around it
            // Debug.Log("Explosion effect spawned");
            Collider[] colliders = Physics.OverlapSphere(pos, _explosionRadius);
            foreach(Collider collider in colliders)
            {
                if (collider.GetComponent<BaseEnemy>() != null && collider.isTrigger == false && collider.GetComponent<BaseEnemy>() != enemy)
                {
                    collider.GetComponent<BaseEnemy>().TakeDamage(_caster.Damage/1.5f);
                }
            }

            // Debug.Log("Explosion damage dealt");
            Destroy(gameObject);
        }
    }
    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _explosionRadius);
    }
}
