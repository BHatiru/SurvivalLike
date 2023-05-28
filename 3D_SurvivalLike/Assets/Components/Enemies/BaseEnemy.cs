using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class BaseEnemy : MonoBehaviour
{
    private NavMeshAgent _enemy;

    private GameObject _target;
    private PlayerScript _player;
    private float damageCooldown = 0.5f;  
    private float damageTimer = 0f;      
    [SerializeField] private float speed;
    [SerializeField] private float health=15f;
    [SerializeField] private float damage=2f;
    
    // Start is called before the first frame update
    void Start()
    {
        _enemy = GetComponent<NavMeshAgent>();
        _target = GameObject.FindGameObjectWithTag("Player");
    }

    void InitializeStats()
    {
        //TODO create scribtable objects to store enemy stats
        _enemy.speed = speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        _enemy.SetDestination(_target.transform.position);
        _enemy.transform.LookAt(new Vector3(_target.transform.position.x, _enemy.transform.position.y, _target.transform.position.z));
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0f)
        {
            Die();
        }
    }

    public void Die()
    {
        //Additional death behaviour
        Debug.Log("Enemy died");
        //destroy object
        Destroy(gameObject);
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == _target)
        {
            damageTimer += Time.deltaTime;

            if (damageTimer >= damageCooldown)
            {
                damageTimer = 0f;
                CollisionDamage();
            }
        }
    }

    private void CollisionDamage()
    {
        if (_player == null)
        {
            _player = _target.GetComponent<PlayerScript>();
        }
        _player.TakeDamage(damage);
    }
}
