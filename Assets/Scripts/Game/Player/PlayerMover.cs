using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidbody;
    [Space]
    [SerializeField] private float moveForce = 3.5f;
    [SerializeField] private float horizontalFriction = .6f;
    [Space]
    [SerializeField] private float moveInAirForce = .1f;
    [SerializeField] private float jumpForce = 8;
    private bool isGrounded = false;
    public bool IsGrounded { set { isGrounded = value; } }
    void Update()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
            rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
    }
    void FixedUpdate()
    {
        if (isGrounded)
            MoveGrounded();
        else
            MoveInAir();
    }

    private void MoveInAir()
    {
        float horizontalForce = Input.GetAxis("Horizontal");
        Vector3 moveVector = Vector3.right * horizontalForce;

        rigidbody.AddForce(moveVector * moveInAirForce, ForceMode.VelocityChange);

    }

    private void MoveGrounded()
    {
        float horizontalForce = Input.GetAxis("Horizontal");
        Vector3 moveVector = Vector3.right * horizontalForce;

        rigidbody.AddForce(moveVector * moveForce, ForceMode.VelocityChange);

        Vector3 frictionVector = Vector3.zero;
        frictionVector.x = rigidbody.velocity.x * -horizontalFriction;
        rigidbody.AddForce(frictionVector, ForceMode.VelocityChange);
    }
}
