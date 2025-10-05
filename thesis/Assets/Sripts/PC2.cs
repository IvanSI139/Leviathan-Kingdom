using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class PC2 : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Movement")]
    [SerializeField] private float walkSpeed = 1;
    [SerializeField] private float jumpForce = 20;
    [SerializeField] private AudioClip footsteps;

    [Header("JumpBuffer")]
    private int JumpBufferCounter =0;
    [SerializeField] private int JumpBufferFrames;

    [Header("CoyotyTime")]
    private float coyotyTimeCounter = 0;
    [SerializeField] private float coyotyTime;

    [Header("GroundCheck")]
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private float grounCheckY = 0.2f;
    [SerializeField] private float grounCheckx = 0.2f;
    [SerializeField] private LayerMask Ground;

    [Header("Wall Collison")]
    [SerializeField] private float wallDampingIncrease = 2f;
    private float originalLinearDamping;
    private bool wall = false;


    private int Direction;

    private float xAxis;
    Animator anim;

    public static PC2 Instance;

    [Header("Slide")]
    [SerializeField] private CapsuleCollider targetCollider;
    [SerializeField] private float disableTime = 2f;
    [SerializeField] private float pushForceX = 5f;
    private bool slide = false;

    PlayerStateList pState;
    void Start()
    {

        pState = GetComponent <PlayerStateList>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        Instance = this;
        originalLinearDamping = rb.linearDamping;
    }

    void Update()
    {
        if (!slide)
        {
            GetInputs();
            UpdateJumpVariable();
            Move();

            Flip();
            Jump();

        }

        Grounded();
        if (Input.GetKeyDown(KeyCode.LeftShift) && Grounded() && !slide)
        {
            StartCoroutine(Slide());
        }


    }

    void GetInputs()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        //Debug.Log("xAxis: " + xAxis);
    }


    private void Move()
    {

        rb.linearVelocity = new Vector3(walkSpeed * xAxis, rb.linearVelocity.y, rb.linearVelocity.z);

        Debug.Log("walk rb velocity: " + rb.linearVelocity);

        anim.SetBool("walking", rb.linearVelocity.x != 0 && Grounded());

    }

    private IEnumerator Slide()
    {
        slide = true;
        anim.SetTrigger("slide");
        Debug.Log("Slide");
        
        rb.linearVelocity = new Vector3(pushForceX * Direction, rb.linearVelocity.y, rb.linearVelocity.z);
        Debug.Log("Slide rb velocity: " + rb.linearVelocity);

           
        yield return new WaitForSeconds(disableTime);

        slide = false;

    }

    public bool Grounded()
    {
        if (Physics.Raycast(groundCheckPoint.position, Vector2.down, grounCheckY, Ground)
            || Physics.Raycast(groundCheckPoint.position + new Vector3(grounCheckx, 0, 0), Vector2.down, grounCheckY, Ground)
            || Physics.Raycast(groundCheckPoint.position + new Vector3(-grounCheckx, 0, 0), Vector2.down, grounCheckY, Ground))
        {
            anim.SetBool("grpunded", true);
            anim.SetBool("juming", false);
            Debug.Log("grouded: true");

            return true;
            
        }
        else if (wall)
        {
            anim.SetBool("juming", false);
            Debug.Log("grouded: true");

            return true;


        }
        else
        {
            anim.SetBool("grpunded", false);
            Debug.Log("grouded: false");

            return false;
        }

    }

    void Jump()
    {

        if (Input.GetButtonDown("Jump") && rb.linearVelocity.y < 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            pState.jumping = false;
        }

        if (!pState.jumping)
        {
            if (JumpBufferCounter > 0 && coyotyTimeCounter > 0)
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce);
                anim.SetBool("juming", true);

                pState.jumping = true;
                coyotyTimeCounter = 0;
            }
        }

        if (Input.GetButtonUp("Jump") && rb.linearVelocity.y > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, y: 0);
        }
 
    }

    void UpdateJumpVariable()
    {
        if (Grounded())
        {
            pState.jumping = false;
            coyotyTimeCounter = coyotyTime;
        }
        else
        {
            coyotyTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump"))
        {
            JumpBufferCounter = JumpBufferFrames;

        }
        else
        {
            JumpBufferCounter--;
        }
    }

    void Flip()
    {
        if (xAxis < 0)
        {
            transform.eulerAngles = new Vector3(0, 260, 0);
            Direction = 1;
        }
        else if (xAxis > 0)
        {
            transform.eulerAngles = new Vector3(0, 90, 0);
            Direction = -1;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("wall"))
        {
            rb.linearDamping = originalLinearDamping + wallDampingIncrease;
            anim.SetBool("wall", true);
            wall = true;

        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("wall"))
        {
            rb.linearDamping = originalLinearDamping;
            anim.SetBool("wall", false);
            wall = false;
        }
    }


}
