using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireZonesCaster : BaseSkill, ICaster
{

    // Start is called before the first frame update
    [SerializeField] private float _spawnRadius = 25f;
    void Start()
    {
        UpdateStats(_SkillData);
    }

    // Update is called once per frame
    void Update()
    {
        cooldownTimer += Time.deltaTime;
        if (cooldownTimer >= cooldown)
        {
            Cast();
            cooldownTimer = 0;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Level++;
            UpdateStats(_SkillData);
        }
    }

    public void Cast()
    {
        Vector3 randomPosition = new Vector3(Random.Range(-_spawnRadius, _spawnRadius), -1, Random.Range(-_spawnRadius, _spawnRadius));
        randomPosition += player.position;

        if (CanSpawn())
        {
            SpawnFireZone(randomPosition);
        }
    }    

    private bool CanSpawn()
    {   
        return (GameObject.FindObjectsOfType<FireZone>().Length < quantity);
    }

    private void SpawnFireZone(Vector3 position)
    {
        FireZone fireZone = Instantiate(VFX_skillObj, position, Quaternion.identity).GetComponent<FireZone>();
        fireZone.SetParameters(radius, damage, duration, speed);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _spawnRadius);
    }
}
