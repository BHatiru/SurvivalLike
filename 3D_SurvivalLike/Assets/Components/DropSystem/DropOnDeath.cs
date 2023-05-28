using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropOnDeath : MonoBehaviour
{
    [SerializeField] private GameObject[] _dropList;
    [SerializeField] private float _expCrystalsDropChance = 0.2f;
    [SerializeField] private float _healthCrystalsDropChance = 0.01f;

    private void Start()
    {
        GetComponent<BaseEnemy>().OnDeath += Drop;
    }

    private void Drop()
    {
        
        float expRoll = Random.Range(0f, 1f);
        if (expRoll <= _expCrystalsDropChance)
        {
            Instantiate(_dropList[0], transform.position, Quaternion.identity);
        }
        //TODO: Add health crystal drop
        float healthRoll = Random.Range(0f, 1f);
        if (healthRoll <= _healthCrystalsDropChance)
        {
            Instantiate(_dropList[1], transform.position+Vector3.back, Quaternion.identity);
        }
    }


}
