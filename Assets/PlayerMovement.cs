using System.Collections;
using System.Collections.Generic;
using System;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 moveInput;
    public float walkSpeed = 10;
    public float sprintSpeed = 15;
    public float turnRate = 5;
    private float move;
    public float jumpSpeed = 300;
    public Rigidbody rb;

    private bool jump;
    public bool grounded;

    public Transform Cam;
    private Vector3 moveVector;
    public bool isFishing;
    

    void Update()
    {
        moveInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        jump = (jump || Input.GetButtonDown("Jump")) && grounded;
    }

    private void FixedUpdate()
    {
        if (!isFishing)
        {
            movePlayer();
            turnPlayer();
            jumpPlayer();
        }
    }

    private void movePlayer()
    {
        if (Input.GetKey("left shift"))
        {
            move = sprintSpeed;
        }
        else
        {
            move = walkSpeed;
        }
        moveVector = Cam.transform.right * moveInput.x * move + Cam.transform.forward * moveInput.z * move;
        moveVector.y = 0f;
        rb.linearVelocity = new Vector3(moveVector.x, rb.linearVelocity.y, moveVector.z);
    }

    private void turnPlayer()
    {
        if (moveVector.magnitude != 0f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveVector), 0.2f);
        }
    }

    private void jumpPlayer()
    {
        if (jump)
        {
            rb.AddForce(new Vector3(0, jumpSpeed, 0), ForceMode.Impulse);
            jump = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            grounded = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            grounded = false;
        }
    }
}