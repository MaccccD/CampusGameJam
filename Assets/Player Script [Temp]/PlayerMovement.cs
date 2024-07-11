using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController cC;
    public Transform cam;
    public CinemachineVirtualCamera cinemachineVirtualCam; // this is for the first person camera since the game is a first person shooter

    [SerializeField] public float walkSpeed = 12f; // the actual walk speed
    [SerializeField] public float sprintSpeed = 18f;
    [SerializeField] public float crouchSpeed = 6f;
    [SerializeField] private float smoothTime = 0.1f;
    [SerializeField] private float smoothVelocity;

    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 2f;

    private Vector3 velocity;
    private bool canJump = false;
    private bool canCrouch = true;

    private Vector3 crouchScale = new Vector3(1f, 0.5f, 1f);
    private Vector3 playerScale = new Vector3(1f, 1.5f, 1f);

    private float currentSpeed; // the players current speed while walking

    [SerializeField] private GameObject focusPoint;

    public bool canRotate = true;

    private bool isMovementLocked = false;

    public void LockMovement(bool shouldLock)
    {
        isMovementLocked = shouldLock;
    }

    private void Update()
    {
        if (!enabled || isMovementLocked) return;
        float hz = Input.GetAxisRaw("Horizontal");
        float vt = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(hz, 0f, vt).normalized;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = sprintSpeed;
        }
        else if (Input.GetKey(KeyCode.C) && canCrouch)
        {
            currentSpeed = crouchSpeed;
        }
        else
        {
            currentSpeed = walkSpeed;
        }

        Vector3 moveDire = Vector3.zero;

        if (direction.magnitude >= 0.1f && canRotate)
        {
            float tAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, tAngle, ref smoothVelocity, smoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDire = Quaternion.Euler(0f, tAngle, 0f) * Vector3.forward;
        }

        if (Input.GetKey(KeyCode.C) && canCrouch)
        {
            Crouch(true);
        }
        else if (!Input.GetKey(KeyCode.C) && !canCrouch)
        {
            Crouch(false);
        }

        Vector3 currentVelocity = moveDire * currentSpeed;

        if (Input.GetButtonDown("Jump") && canJump)
        {
            Jump();
        }

        ApplyGravity();
        cC.Move(currentVelocity * Time.deltaTime);
    }

    private void ApplyGravity()
    {
        velocity.y += gravity * Time.deltaTime;

        cC.Move(velocity * Time.deltaTime);

        if (cC.isGrounded)
        {
            velocity.y = 0f; // making the gravitational velocity force zero so the players does not feel the force of gravity
            canJump = true;
        }
    }

    private void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        canJump = false;
    }

    private void Crouch(bool istrue)
    {
        if (istrue)
        {
            transform.localScale = crouchScale;
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
            canCrouch = false;
        }
        else
        {
            transform.localScale = playerScale;
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
            canCrouch = true;
        }
    }

    public void SwitchCameraPriority(CinemachineVirtualCamera otherCamera, bool isDialogueActive)
    {
        if (isDialogueActive)
        {
            cinemachineVirtualCam.Priority = 9;
            otherCamera.Priority = 10;
        }
        else
        {
            cinemachineVirtualCam.Priority = 10;
            otherCamera.Priority = 9;
        }
    }
}
