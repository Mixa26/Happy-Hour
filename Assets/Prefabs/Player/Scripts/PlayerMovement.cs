using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;

    //check if player is on ground
    public bool IsGrounded;
    
    private Vector3 jumpForce;

    private bool collidingWithJumpWall;
    private bool jumpOnceJumpWall;

    //used for checking if player is on ground with ray casts
    private float distanceToGround;

    [HideInInspector]
    public bool enemyHit;
    public bool foodAte;
    [Space]

    [HideInInspector]
    public bool endLevel;

    [Header("Audio")]
    //Audio
    public AudioSource jumpAudio;
    public AudioClip jumpAudioClip;
    public AudioSource dieAudio;
    public AudioClip dieAudioClip;
    public AudioSource oohAudio;
    public AudioClip oohAudioClip;
    public AudioSource eatAudio;
    public AudioClip eatAudioClip;

    //Animator
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        IsGrounded = true;
        jumpForce = new Vector3(0, 8, 0);
        distanceToGround = GetComponent<Collider>().bounds.extents.y;
        collidingWithJumpWall = false;
        jumpOnceJumpWall = false;

        enemyHit = false;
        foodAte = false;

        //Audio
        jumpAudio = gameObject.AddComponent<AudioSource>();
        jumpAudio.clip = jumpAudioClip;
        dieAudio = gameObject.AddComponent<AudioSource>();
        dieAudio.clip = dieAudioClip;
        oohAudio = gameObject.AddComponent<AudioSource>();
        oohAudio.clip = oohAudioClip;
        eatAudio = gameObject.AddComponent<AudioSource>();
        eatAudio.clip = eatAudioClip;

        //Animator
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (collidingWithJumpWall)
        {
            if (jumpOnceJumpWall)
            {
                // player can jump if he is touching the jump wall
                IsGrounded = true;
            }
            else
            {
                IsGrounded = false;
            }
        }

        //movement
        if (IsGrounded && Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (collidingWithJumpWall)
            {
                jumpOnceJumpWall = false;
            }
            //physics
            rb.AddForce(jumpForce, ForceMode.Impulse);

            //if player on the ground check
            IsGrounded = false;

            //audio effect
            jumpAudio.Play();
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //movement update
            Vector3 playerPosition = transform.position;
            playerPosition.x -= 5.0f * Time.deltaTime;
            transform.position = playerPosition;

            //look left update
            Quaternion playerRotation = transform.rotation;
            playerRotation.x = 0;
            playerRotation.y = 180;
            playerRotation.z = 0;
            transform.rotation = playerRotation;

            //player animation bools update
            if (IsGrounded)
            {
                animator.SetBool("isJump", false);
                animator.SetBool("isWalking", true);
                animator.SetBool("isStanding", false);
            }

        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            //movement update
            Vector3 playerPosition = transform.position;
            playerPosition.x += 5.0f * Time.deltaTime;
            transform.position = playerPosition;

            //look left update
            Quaternion playerRotation = transform.rotation;
            playerRotation.x = 0;
            playerRotation.y = 0;
            playerRotation.z = 0;
            transform.rotation = playerRotation;

            //player animation bools update
            if (IsGrounded)
            {
                animator.SetBool("isJump", false);
                animator.SetBool("isWalking", true);
                animator.SetBool("isStanding", false);
            }

        }
        else
        {
            if (IsGrounded)
            {
                //player idle animation bool update
                animator.SetBool("isJump", false);
                animator.SetBool("isWalking", false);
                animator.SetBool("isStanding", true);
            }
        }

        if (!IsGrounded)
        {
                //player in jump animation bool update
                animator.SetBool("isJump", true);
                animator.SetBool("isWalking", false);
                animator.SetBool("isStanding", false);            
        }

        
    }

    private void OnCollisionEnter(Collision collision)
    {
        //check for if the player is touching the jump wall
        if (collision.gameObject.CompareTag("JumpWall"))
        {
            collidingWithJumpWall = true;
            jumpOnceJumpWall = true;
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemyHit = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("JumpWall"))
        {
            collidingWithJumpWall = false;
            jumpOnceJumpWall = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            foodAte = true;
            eatAudio.Play();
            Destroy(other.gameObject);
        }

        if (other.gameObject.name.Equals("EndLevel"))
        {
            endLevel = true;
        }

        if (other.gameObject.name.Equals("OpenDoor"))
        {
            GameObject.Find("DoorV2").GetComponent<Animator>().SetBool("OpenDoor", true);
        }
    }

    private void FixedUpdate()
    {
        if (!collidingWithJumpWall)
        {
            if (Physics.Raycast(transform.position, -Vector3.up, distanceToGround + 0.56f))
            {
                IsGrounded = true;
            }
            else
            {
                IsGrounded = false;
            }
        }      
    }
}
