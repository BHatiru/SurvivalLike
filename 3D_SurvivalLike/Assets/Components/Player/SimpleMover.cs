using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMover : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f; // Adjust this value to control the player's movement speed

    private Vector3 movement;

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        movement = new Vector3(moveHorizontal, 0f, moveVertical);
        if (movement.magnitude > 1f) movement.Normalize();
        movement *= movementSpeed * Time.deltaTime;
        transform.Translate(movement);
    }

}
