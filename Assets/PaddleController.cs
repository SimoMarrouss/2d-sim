using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public float speed = 5f;
    public KeyCode _upKeyCode = KeyCode.W;
    public KeyCode _downKeyCode = KeyCode.S;

    void Start()
    {
    }

    void Update()
    {
        float verticalInput = 0f;

        if (Input.GetKey(_upKeyCode))
            verticalInput = 1f;
        else if (Input.GetKey(_downKeyCode))
            verticalInput = -1f;

        // Calculate the desired position
        Vector3 moveDirection = new Vector3(0f, verticalInput, 0f) * (speed * Time.deltaTime);
        Vector3 desiredPosition = transform.position + moveDirection;

        // Clamp the desired position within the defined range
        desiredPosition.y = Mathf.Clamp(desiredPosition.y, -7, 7);

        // Move the object
        transform.position = desiredPosition;
    }
}