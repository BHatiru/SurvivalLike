using System.Collections;
using System;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

[Serializable]
public class EnemyStats{
    //default constructor that accept 3 arguments and assign them to the variables
    public EnemyStats(float speed, float health, float damage){
        this.speed = speed;
        this.health = health;
        this.damage = damage;
    }
    public float speed= 2f;
    public float health=15f;
    public float damage=2f;
}
public class BaseEnemy : MonoBehaviour
{
    private NavMeshAgent _enemy;

    private GameObject _target;
    private PlayerHealth _player;
    private float damageCooldown = 0.5f;  
    private float damageTimer = 0f;
    public EnemyStats stats;     

    [SerializeField] private EnemyData _enemyData;

    [SerializeField] private GameObject _damageText;

    public event System.Action OnDeath;
    
    // Start is called before the first frame update
    void Start()
    {
        _enemy = GetComponent<NavMeshAgent>();
        _target = GameObject.FindGameObjectWithTag("Player");
        InitializeStats();
    }

    void InitializeStats()
    {
        stats.damage = _enemyData.enemyStats.damage;
        stats.health = _enemyData.enemyStats.health;
        stats.speed = _enemyData.enemyStats.speed;
        _enemy.speed = stats.speed;
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
        Damage(damage);
    }

    //Overload for delayed damage
    public void TakeDamage(float damage, float delay)
    {
        StartCoroutine(DamageDelay(damage, delay));
    }

    private void Damage(float damage){
        stats.health -= damage;
        if(AmountOfDamageTexts() < 150){
            DamageIndicator indicator = Instantiate(_damageText, transform.position, Quaternion.identity).GetComponent<DamageIndicator>();
            indicator.SetDamageText(damage);
        }

        if (stats.health <= 0f)
        {
            Die();
        }
    }

    private int AmountOfDamageTexts(){
        return FindObjectsOfType<DamageIndicator>().Length;
    }
    IEnumerator DamageDelay(float damage, float delay)
    {
        yield return new WaitForSeconds(delay);
        Damage(damage);
    }

    public void Die()
    {
        OnDeath?.Invoke();
        //Additional death behaviour

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
            _player = _target.GetComponent<PlayerHealth>();
        }
        _player.TakeDamage(stats.damage);
    }
}
