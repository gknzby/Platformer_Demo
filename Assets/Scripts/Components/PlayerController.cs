using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#region UnityEngine Attributes
[HelpURL("https://github.com/gknzby")]
[RequireComponent(typeof(CharacterController2D))]
[RequireComponent(typeof(Animator))]
#endregion
public class PlayerController : MonoBehaviour
{
    public float runSpeed = 100.0f;

    private float hzMove = 0f;
    private CharacterController2D controller;
    private Animator animator;

    private bool jump = false;
    private float jumpCooldown = 0.5f;
    private float jumpTimer = 0.0f;

    private bool crouch = false;

    private void Awake()
    {
        controller = GetComponent<CharacterController2D>();
        controller.Crouching_Event.AddListener(Crouching_Handler);
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        jumpTimer += Time.deltaTime;
        if (Input.GetButtonDown("Jump") && jumpTimer > jumpCooldown)
        {
            jumpTimer = 0.0f;
            animator.SetBool("IsJumping", true);
            jump = true;
        }
        if(Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }

        this.hzMove = Input.GetAxisRaw("Horizontal") * runSpeed;
    }
    private void FixedUpdate()
    {
        this.controller.Move(hzMove * Time.fixedDeltaTime, jump, crouch);
        jump = false;
        crouch = false;
        this.animator.SetFloat("Speed", Mathf.Abs(hzMove));
    }    

    public void OnLanding_Handler()
    {
        if(0.02f < jumpTimer)
            animator.SetBool("IsJumping", false);
    }

    public void Crouching_Handler(bool isCrouching)
    {
        this.crouch = isCrouching;
        animator.SetBool("IsCrouching", isCrouching);
    }
}
