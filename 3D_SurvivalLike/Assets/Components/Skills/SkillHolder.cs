using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillHolder : MonoBehaviour
{
    private Transform player;  // Reference to the player's Transform
    void Start()
    {
        player = FindAnyObjectByType<PlayerControls>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position;
    }
}
