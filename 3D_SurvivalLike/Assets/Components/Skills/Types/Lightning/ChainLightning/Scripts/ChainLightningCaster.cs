using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainLightningCaster : BaseSkill, ICaster
{
    [SerializeField] private int maxChainCount = 3; // Maximum number of chain targets // TODO: Make this a skill upgrade
    [SerializeField] private float chainRadius = 5f; // Radius within which enemies can be chained
    [SerializeField] private float chainDelay = 0.5f; // Delay between chain lightning strikes
    [SerializeField] private LayerMask enemyLayer; // Layer mask for the enemies
    private List<BaseEnemy> previouslyTargeted; // List of previously targeted enemies

    private void Start() {
        UpdateStats(_SkillData);
    }
    private void Update() {
        cooldownTimer += Time.deltaTime;
        if (cooldownTimer >= cooldown){
            Cast();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Level++;
            UpdateStats(_SkillData);
        }
    }
    
    public void Cast()
    {
        ActivateChainLightning(quantity);
        cooldownTimer = 0F;
    }
    public void ActivateChainLightning(int strikeCount)
    {
        previouslyTargeted = new List<BaseEnemy>();

        StartCoroutine(ChainLightningRoutine(strikeCount));
    }

    private IEnumerator ChainLightningRoutine(int strikeCount)
    {
        for (int i = 0; i < strikeCount; i++)
        {
            // Start the chain lightning from the player's position
            ChainLightning(transform.position, 0);

            yield return new WaitForSeconds(chainDelay);
        }


        previouslyTargeted = null;
    }

    private void ChainLightning(Vector3 chainPoint, int chainCount)
    {
        // Find all enemies within the chain radius

        Collider[] colliders = Physics.OverlapSphere(chainPoint, chainRadius, enemyLayer);
        List<BaseEnemy> potentialTargets = new List<BaseEnemy>();

        foreach (Collider collider in colliders)
        {
            BaseEnemy enemy = collider.GetComponent<BaseEnemy>();
            if (enemy != null && !previouslyTargeted.Contains(enemy) && !collider.isTrigger)
            {
                potentialTargets.Add(enemy);
            }
        }

        // Select the target enemy
        BaseEnemy targetEnemy = null;

        if (potentialTargets.Count > 0)
        {
            if (chainCount == 0)
            {
                // For the first chain, select a random enemy from potential targets
                int randomIndex = Random.Range(0, potentialTargets.Count);
                targetEnemy = potentialTargets[randomIndex];
            }
            else
            {
                // Sort potentialTargets based on distance from chainPoint
                potentialTargets.Sort((a, b) =>
                    Vector3.Distance(chainPoint, a.transform.position).CompareTo(
                    Vector3.Distance(chainPoint, b.transform.position)));

                targetEnemy = potentialTargets[0]; // Select the closest enemy from potential targets
            }
        }

        // If a target enemy is found, chain to it
        if (targetEnemy != null)
        {
            // Apply damage to the enemy
            targetEnemy.TakeDamage(damage);
            // Spawn the lightning chain effect and rotate it towards the enemy, not changing the vertical rotation
            Quaternion rotation = Quaternion.LookRotation(targetEnemy.transform.position - chainPoint);
            rotation.eulerAngles = new Vector3(0f, rotation.eulerAngles.y, rotation.eulerAngles.z);
            SpawnLightningChain(rotation, targetEnemy.transform.position, chainPoint);

            // Add the enemy to the previously targeted list
            previouslyTargeted.Add(targetEnemy);


            // Continue the chain if chainCount is less than maxChainCount
            if (chainCount < maxChainCount)
            {
                // Recursively chain to the target enemy
                ChainLightning(targetEnemy.transform.position+Vector3.up*0.5f, chainCount + 1);
            }
        }
    }

    private void SpawnLightningChain(Quaternion rotation, Vector3 targetPosition, Vector3 chainPoint)
    {
        //Instantiate the lightning chain effect
        GameObject chain = Instantiate(VFX_skillObj, chainPoint, rotation);
        chain.GetComponent<ChainLightningSegment>().endScaleZ = Vector3.Distance(transform.position, targetPosition);
        //Destroy the effect after 1 second
        Destroy(chain, 1f);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chainRadius);
    }

    public void ActivateSkillCaster()
    {
        gameObject.SetActive(true);
    }

    
    public void UpdateCasterLevel()
    {
        Level++;
        Debug.Log("Skill" +skillName +  "level updated to " + Level);
        UpdateStats(_SkillData);
    }
}

