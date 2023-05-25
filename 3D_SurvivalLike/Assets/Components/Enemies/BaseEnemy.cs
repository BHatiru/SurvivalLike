using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class BaseEnemy : MonoBehaviour
{
    private NavMeshAgent _enemy;

    private GameObject _target;
    private PlayerScript _player;
    private float damageCooldown = 0.5f;  // Time interval between damage instances
    private float damageTimer = 0f;      // Timer to track elapsed time
    private float speed;
    private float health;
    [SerializeField] private float damage;
    // Start is called before the first frame update
    void Start()
    {
        _enemy = GetComponent<NavMeshAgent>();
        _target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        _enemy.SetDestination(_target.transform.position);
        _enemy.transform.LookAt(new Vector3(_target.transform.position.x, _enemy.transform.position.y, _target.transform.position.z));
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
