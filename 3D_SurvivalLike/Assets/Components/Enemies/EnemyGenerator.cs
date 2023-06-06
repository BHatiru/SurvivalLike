using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private float _spawnRate = 5f;
    [SerializeField] private Vector2 _spawnArea = new Vector2(10f, 10f);
    [SerializeField] private int _minEnemies = 5;
    [SerializeField] private int _maxEnemies = 10;
    //[SerializeField] private EnemyData _enemy;
    [SerializeField] private List<EnemyData> _enemies;
    [SerializeField] private Transform _player;
    [SerializeField] private ExpCrystal _expCrystal;
    private float _spawnTimer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        //Set initial enemy stats
        _enemies[0].enemyStats = batEnemyStats;
        _enemies[1].enemyStats = slimeEnemyStats;
        _enemies[2].enemyStats = turtleEnemyStats;
        _expCrystal.expAmount = 20f;
    }

    //The initial enemy stats for each type of enemy
    public EnemyStats batEnemyStats = new(12f, 15f, 1.5f);
    public EnemyStats slimeEnemyStats = new(5f, 25f, 5.5f);
    public EnemyStats turtleEnemyStats = new(3f, 35f, 7f);
    void Update()
    {
        _spawnTimer += Time.deltaTime;
        if(_spawnTimer >= _spawnRate)
        {
            //choose random enemy from list
            SpawnBanchOfEnemies(_enemies[Random.Range(0, _enemies.Count)]);
            _spawnTimer = 0f;
        }
    }

    Vector3 RandomPositionAroundPlayer()
    {
        
        Vector3 randomPos = new Vector3();
        // get random position around player but outside screen area
        float f = Random.value > 0.5f ? 1f : -1f;
        if(Random.value > 0.5f)
        {
            randomPos.x = Random.Range(-_spawnArea.x, _spawnArea.x);
            randomPos.z = f * _spawnArea.y;
        }
        else
        {
            randomPos.z = Random.Range(-_spawnArea.y, _spawnArea.y);
            randomPos.x = f * _spawnArea.x;
        }
        
        
        randomPos.y = 0f;

        randomPos += _player.position;

        return randomPos;    
    }

    public void SpawnEnemy(EnemyData enemyData)
    {
        Vector3 spawnPos = RandomPositionAroundPlayer();
        GameObject enemy = Instantiate(enemyData.enemyPrefab, spawnPos, Quaternion.identity);
        enemy.transform.LookAt(_player);
    }

    public void SpawnBanchOfEnemies(EnemyData enemyData)
    {
        int randomNum = Random.Range(_minEnemies, _maxEnemies);
        
        for (int i = 0; i < randomNum; i++)
        {
            EnemyData enemytospawn = Random.Range(0, 2) == 0 ? enemyData : _enemies[Random.Range(0, _enemies.Count)];
            SpawnEnemy(enemytospawn);
        }
    }

    public void IncreaseDifficulty(){
        _spawnRate -= 0.3f;
        _minEnemies += 3;
        _maxEnemies += 5;
        foreach (EnemyData enemy in _enemies)
        {
            enemy.enemyStats.damage += 0.5f;
            enemy.enemyStats.health += 15f;
            enemy.enemyStats.speed += 0.3f;
            _expCrystal.expAmount *= 1.75f;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_player.position, new Vector3(_spawnArea.x * 2f, 0f, _spawnArea.y * 2f));
    }
}
