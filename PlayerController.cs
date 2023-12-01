using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    [SerializeField] private float myRunSpeed = 10.0f;
    [SerializeField] private float myJumpForce = 10.0f;
    [SerializeField] private float myTrampolineJumpForce = 10.0f;
    [SerializeField] private float myWallJumpTimer = 10.0f;
    private Vector2 moveInput;
    private int numberOfJumps;
    private bool playerHaveJumpedFromWall;
    private bool playerIsKnocked;
    private Rigidbody2D myRB;
    private CapsuleCollider2D myCapsuleCollider;
    private BoxCollider2D myBoxCollider;
    private Animator myAnimator;
    private AudioSource myAudioSource;

    private void Awake() {
        myRB = GetComponent<Rigidbody2D>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
        myBoxCollider = GetComponent<BoxCollider2D>();
        myAnimator = GetComponent<Animator>();
        myAudioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate() {
        if(playerIsKnocked)
            return;
        Run();
    }

    private void Update() {
        if(playerIsKnocked)
            return;
        JumpAnimations();
        CheckPlayerIsKnockedOut();
        WallSlide();
        TrampolineJump();
        JumpOnKnockingEnemies();
        JumpOnBubbleArrow();
    }

    private void OnMove(InputValue value) {
        moveInput = value.Get<Vector2>();
    }

    private void OnJump(InputValue value) {
        if(playerIsKnocked)
            return;
        // Player Jumping Wall
        if(!myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Platform")) 
            && myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Platform"))) {
            playerHaveJumpedFromWall = true;
            myRB.velocity = new Vector2 (myRB.velocity.x, myJumpForce);
            Invoke("PlayerHaveJumpedWall", myWallJumpTimer);
        }
        // Player Jumping In General
        if(myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Platform")) 
            || myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Moving Platform"))) {
            numberOfJumps = 1;
            myRB.velocity = new Vector2 (myRB.velocity.x, myJumpForce);
        }
        else if(numberOfJumps == 1) {
            numberOfJumps = 0;
            myRB.velocity = new Vector2 (myRB.velocity.x, myJumpForce);
            myAnimator.SetTrigger("IsDoubleJumping");
        }
    }

    private void Run() {
        bool playerIsRunning;
        myRB.velocity = new Vector2 (moveInput.x * myRunSpeed, myRB.velocity.y);
        playerIsRunning = Mathf.Abs(myRB.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("IsRunning", playerIsRunning);
        if(playerIsRunning)
            transform.localScale = new Vector2 (Mathf.Sign(myRB.velocity.x), 1);
    }

    private void JumpAnimations() {
        bool playerIsJumping = myRB.velocity.y > 0.5f;
        bool playerIsFalling = myRB.velocity.y < -0.5f;
        if(myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Moving Platform"))) {
            playerIsFalling = false;
            playerIsJumping = false;
        }
        myAnimator.SetBool("IsJumping", playerIsJumping);
        myAnimator.SetBool("IsFalling", playerIsFalling);
    }

    private void CheckPlayerIsKnockedOut() {
        if(myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Trap")) 
            || myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Trap")) 
            || myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Enemies"))) {
                playerIsKnocked = true;
                myRB.velocity = new Vector2 (0f, myJumpForce);
                myAnimator.SetTrigger("IsKnocked");
        }
        else
            playerIsKnocked = false;
    }

    private void WallSlide() {
        bool playerIsWallSliding;
        if(!myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Platform")) 
            && myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Platform")) 
            && !playerHaveJumpedFromWall) {
                numberOfJumps = 0;
                playerIsWallSliding = true;
                myRB.velocity = new Vector2 (myRB.velocity.x, 0f);
                myAnimator.SetBool("IsWallSliding", playerIsWallSliding);
        }
        else {
            playerIsWallSliding = false;
            myAnimator.SetBool("IsWallSliding", playerIsWallSliding);
        }
    }

    private void TrampolineJump() {
        if(myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Trampoline")))
            myRB.velocity = new Vector2 (myRB.velocity.x, myTrampolineJumpForce);
    }
    private void PlayerHaveJumpedWall() {
        playerHaveJumpedFromWall = false;
    }

    private void JumpOnKnockingEnemies() {
        if(myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Enemies")))
            myRB.velocity = new Vector2 (0f, myJumpForce);
    }

    private void JumpOnBubbleArrow() {
        if(myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Bubble Arrow")))
            myRB.velocity = new Vector2 (0f, myJumpForce);
    }
}
