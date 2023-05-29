using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationAnime : MonoBehaviour
{
    public Transform player;  // Reference to the player's Transform
    public float orbitSpeed = 5f;  // Speed of rotation
    public float radius = 10f;  // Radius of the circular path

    private void Update()
    {
        // Calculate the position of the object on the circular path
        Vector3 pos = new Vector3(Mathf.Cos(Time.time * orbitSpeed), 0, Mathf.Sin(Time.time * orbitSpeed)) * radius;
        // Set the object's position
        transform.position = pos + player.position;

    }

}
