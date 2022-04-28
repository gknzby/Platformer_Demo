using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

#region UnityEngine Attributes
[HelpURL("https://github.com/gknzby")]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
#endregion
public class CharacterController2D : MonoBehaviour
{
    #region Common
    private Rigidbody2D rb2D;
    #endregion

    #region Jump&Landing
    [Header("Jump&Landing")]
    [Space]
    [SerializeField] private Transform groundCheck_Transform;
    [SerializeField] private LayerMask groundCheck_Layers;
    [SerializeField] private float groundCheck_Distance;
    [SerializeField] private float jumpForce = 700f;
    [SerializeField] private float jumpForgiveTime = 0f;
    private float jumpForgiveTimer = 0f;
    private bool onGround = false;
    #endregion

    #region Horizontal Movement
    [Header("Horizontal Movement")]
    [Space]
    [SerializeField] [Range(0, 1)] private float moveSmooth;
    private Vector3 vel = Vector3.zero;
    private bool lookLeft = false;
    #endregion

    #region Crouching
    [Header("Crouching")]
    [Space]
    [SerializeField] private Transform ceilingCheck_Transform;
    [SerializeField] private LayerMask ceilingCheck_Layers;
    [SerializeField] private float ceilingCheck_Distance;
    [SerializeField] private Collider2D crouchDisableCollider;
    [SerializeField] [Range(0, 1)] private float crouchSpeed;
    private bool isCrouch = false;
    #endregion

    #region Events
    [Header("Events")]
    [Space]
    public UnityEvent OnLanding_Event;
    [HideInInspector] public UnityEvent<bool> Crouching_Event;
    #endregion

    void Reset()
    {
        //Rigidbody
        this.rb2D = this.GetComponent<Rigidbody2D>();

        //Jump&Landing
        this.groundCheck_Layers = LayerMask.GetMask("Default");
        this.groundCheck_Distance = 0.25f;
        this.jumpForce = 600f;
        this.jumpForgiveTime = 0.3f;

        //Horizontal Move
        this.moveSmooth = 0.05f;

        //Crouching
        this.ceilingCheck_Layers = LayerMask.GetMask("Default");
        this.ceilingCheck_Distance = 0.19f;
        this.crouchSpeed = 0.8f;
    }

    private void Awake()
    {
        this.rb2D = this.GetComponent<Rigidbody2D>();

        if (this.OnLanding_Event == null)
            OnLanding_Event = new UnityEvent();
        if (this.Crouching_Event == null)
            Crouching_Event = new UnityEvent<bool>();

        lookLeft = this.GetComponent<SpriteRenderer>().flipX;
    }

    private void FixedUpdate()
    {
        jumpForgiveTimer += Time.deltaTime;
        onGround = CheckGround(onGround);
    }



    public void Move(float hzMove, bool jump, bool crouch)
    {
        if (!jump)
        {
            if (crouch && onGround)
            {
                if (!isCrouch)
                {
                    isCrouch = true;
                }
                else if (!CheckCeiling())
                {
                    isCrouch = false;
                }
                Crouching_Event.Invoke(isCrouch);
                crouchDisableCollider.enabled = !isCrouch;
            }
        }
        else if (isCrouch)
        {
            if (CheckCeiling())
            {
                jump = false;
            }
            else
            {
                isCrouch = false;
                Crouching_Event.Invoke(isCrouch);
                crouchDisableCollider.enabled = !isCrouch;
            }
        }

        hzMove *= (isCrouch ? crouchSpeed : 1f);
        hzMove *= (onGround ? 1f : 0.75f);
        Vector3 targetVel = new Vector2(hzMove * 10.0f, this.rb2D.velocity.y);
        this.rb2D.velocity = Vector3.SmoothDamp(this.rb2D.velocity, targetVel, ref this.vel, this.moveSmooth);

        if (hzMove < 0 && !lookLeft)
        {
            Flip();
        }
        else if (hzMove > 0 && lookLeft)
        {
            Flip();
        }

        if ((onGround || jumpForgiveTimer < jumpForgiveTime) && jump)
        {
            this.onGround = false;
            this.rb2D.AddForce(new Vector2(0f, this.jumpForce));
        }
    }

    public void Flip()
    {
        lookLeft = !lookLeft;
        this.GetComponent<SpriteRenderer>().flipX = lookLeft;
    }

    private bool CheckGround(bool onGround)
    {
        RaycastHit2D hit = Physics2D.Raycast(this.groundCheck_Transform.position, -Vector2.up, this.groundCheck_Distance, this.groundCheck_Layers);

        if (hit.collider != null)
        {
            //Debug.DrawRay(this.groundCheck_Transform.position, -Vector2.up * hit.distance, Color.red);
            if (hit.transform != this.transform)
            {
                //Debug.Log("Hit");
                if (!onGround)
                    OnLanding_Event.Invoke();
                return true;
            }
        }
        if (onGround) jumpForgiveTimer = 0;
        return false;
    }

    private bool CheckCeiling()
    {
        RaycastHit2D hit = Physics2D.Raycast(this.ceilingCheck_Transform.position, Vector2.up, this.ceilingCheck_Distance, this.ceilingCheck_Layers);

        if (hit.collider != null)
        {
            //Debug.DrawRay(this.ceilingCheck_Transform.position, Vector2.up * hit.distance, Color.red);
            //Debug.Log("CMON");
            if (hit.transform != this.transform)
            {
                //Debug.Log(hit.transform.name);
                return true;
            }
        }
        return false;
    }
}
