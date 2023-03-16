using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    float timer = 0.0f;
    float soundInterval = 8f;

    public float speed = 12f;
    public float boostSpeed = 20f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public AudioSource footSteps;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        // Check if space bar is pressed down and adjust speed accordingly
        if (Input.GetKey(KeyCode.Space))
        {
            speed = boostSpeed;
        }
        else
        {
            speed = 12f; // Set speed back to normal if space bar is not pressed down
        }

        controller.Move(move * speed * Time.deltaTime);

        if (move.magnitude > 0f)
        {
            footSteps.enabled = true;

            if (Time.time > timer)
            {
                footSteps.Play();
                timer = Time.time + soundInterval;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                footSteps.Play();
            }
        }
        else
        {
            footSteps.enabled = false;
        }

        // if (Input.GetButtonDown("Jump") && isGrounded)
        // {
        //     velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        // }

        // velocity.y += gravity * Time.deltaTime;
        // controller.Move(velocity * Time.deltaTime);
    }
}
