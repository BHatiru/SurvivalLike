using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class BaseEnemy : MonoBehaviour
{
    private NavMeshAgent _enemy;
    private Transform _target;
    // Start is called before the first frame update
    void Start()
    {
        _enemy = GetComponent<NavMeshAgent>();
        _target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        _enemy.SetDestination(_target.position);
        _enemy.transform.LookAt(new Vector3(_target.position.x, _enemy.transform.position.y, _target.position.z));
    }
}
