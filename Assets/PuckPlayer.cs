using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class PuckPlayer : MonoBehaviour
{
    private const float YBorderLength = 9;
    private const float XBorderLength = 20;
    private float xspeed = .01f;

    private float yspeed = .03f;
    public float moveSpeed = 10f; // Adjust this to control the speed of movement
    public float moveSpeedX = 10f;
    public float minY = -7f; // Minimum Y position
    public float maxY = 7f; // Maximum Y position

    private int verticalDirection = 1;
    private int horizontalDirection = 1;

    private int rightScore = 0;
    private int leftScore = 0;
    private bool paused = false;
    public AudioClip hitSound; // Sound to play when the ball hits the paddle

    private AudioSource[] audioSources;
    public TMP_Text scoreLeft;

    void Start()
    {
        Time.timeScale = 0;
        audioSources = GetComponents<AudioSource>();
        scoreLeft = GetComponent<TMP_Text>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            paused = !paused;
        }

        Time.timeScale = paused ? 0 : 1;

        if (transform.position.y < -YBorderLength || transform.position.y > YBorderLength)
        {
            verticalDirection *= -1;
        }

        if (transform.position.x < -XBorderLength)
        {
            leftScore += 1;
            Reset();
        }

        if (transform.position.x > XBorderLength)
        {
            rightScore += 1;
            if (scoreLeft)
            {
                scoreLeft.text = rightScore.ToString();
            }

            Reset();
        }

        Vector3 moveDirection = new Vector3(horizontalDirection, verticalDirection, 0f);

        transform.Translate(moveDirection * (25f * Time.deltaTime));
    }

    private void Reset()
    {
        if (audioSources[1])
        {
            audioSources[1].Play();
        }

        transform.position = Vector3.zero;
        Time.timeScale = 0;
        paused = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (audioSources[0] != null)
        {
            audioSources[0].Play();
        }

        horizontalDirection *= -1;
    }
}