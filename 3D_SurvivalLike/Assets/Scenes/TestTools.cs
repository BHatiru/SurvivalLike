using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTools : MonoBehaviour
{
    [SerializeField] private float _speedModifier = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //accelerate game speed 2 times when 'r' key is pressed
        if (Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale *=_speedModifier; 
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Time.timeScale /= _speedModifier;
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            Time.timeScale = 1f;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale = 0f;
        }

    }
}
