using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemyPrefabs;
    [SerializeField] private float _spawnRate = 5f;
    [SerializeField] private Vector2 _spawnArea = new Vector2(10f, 10f);
    [SerializeField] private int _maxEnemies = 10;
    [SerializeField] private int _enemyIndex = 0;
    [SerializeField] private Transform _player;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnBanchOfEnemies", 0f, _spawnRate);
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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_player.position, new Vector3(_spawnArea.x * 2f, 0f, _spawnArea.y * 2f));
    }
}
