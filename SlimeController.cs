using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour {
    [SerializeField] private float myRunSpeed = 10.0f;
    [SerializeField] private float destroyDelayTime = 0.25f;
    private bool slimeIsKnocked;
    private Rigidbody2D myRB;
    private CapsuleCollider2D myCapsuleCollider;
    private BoxCollider2D myBoxCollider;
    private CircleCollider2D myCircleCollider;
    private Animator myAnimator;

        private void Start() {
        myRB = GetComponent<Rigidbody2D>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
        myAnimator = GetComponent<Animator>();
        myBoxCollider = GetComponent<BoxCollider2D>();
        myCircleCollider = GetComponent<CircleCollider2D>();
    }

    private void FixedUpdate() {
        if(slimeIsKnocked)
            return;
        Run();
    }

    private void Run() {
        myRB.velocity = new Vector2 (-myRunSpeed, myRB.velocity.y);
        transform.localScale = new Vector2 (Mathf.Sign(myRunSpeed), 1f);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Platform")))
            myRunSpeed = -myRunSpeed;
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(!myCircleCollider.IsTouchingLayers(LayerMask.GetMask("Platform")))
            myRunSpeed = -myRunSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player")) {
            slimeIsKnocked = true;
            myAnimator.SetTrigger("BeingKnocked");
            Invoke("DestroyMe", destroyDelayTime);
        }
    }

    private void DestroyMe() {
        Destroy(gameObject);
    }
}
