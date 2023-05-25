using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemyPrefabs;
    [SerializeField] private float _spawnRate = 5f;
    [SerializeField] private float _spawnRadius = 15f;
    [SerializeField] private int _maxEnemies = 10;
    [SerializeField] private int _enemyIndex = 0;
    private Transform _player;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        InvokeRepeating("SpawnBanchOfEnemies", 0f, _spawnRate);
    }

    Vector3 RandomPositionAroundPlayer()
    {
        Vector3 randomPos = Random.insideUnitSphere * _spawnRadius;
        randomPos += _player.position;

        return new Vector3(randomPos.x, 0f, randomPos.z);    
    }

    public void SpawnEnemy(int enemyIndex)
    {
        Vector3 spawnPos = RandomPositionAroundPlayer();
        GameObject enemy = Instantiate(_enemyPrefabs[enemyIndex], spawnPos, Quaternion.identity);
        enemy.transform.LookAt(_player);
    }

    public void SpawnBanchOfEnemies()
    {
        int randomNum = Random.Range(5, _maxEnemies);
        for (int i = 0; i < randomNum; i++)
        {
            SpawnEnemy(_enemyIndex);
        }
    }

    public void GenerateWave(){
        
    }
}
