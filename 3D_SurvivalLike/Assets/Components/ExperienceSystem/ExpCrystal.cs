using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpCrystal : MonoBehaviour
{
    [SerializeField] private int expAmount = 20;
    [SerializeField] private float _attractionSpeed = 10f;
    void Start()
    {
        
    }

    public void Attract(Vector3 targetPosition)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, _attractionSpeed * Time.deltaTime);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerLevel>() != null)
        {
            PickUp();
        }
    }
    //NOTE: This method is called from the PlayerController.cs script
    public void PickUp()
    {
        ExperienceManager.Instance.GainExperience(expAmount);
        Destroy(this.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0,0,10) * 10f * Time.deltaTime);
    }
}
