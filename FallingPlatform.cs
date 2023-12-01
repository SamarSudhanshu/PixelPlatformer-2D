using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour {
    [SerializeField] private float fallDelay = 1f;
    [SerializeField] private float destroyDelay = 1f;
    private Rigidbody2D myRB;
    private Animator myAnimator;
    private BoxCollider2D myBoxCollider;

    private void Awake() {
        myRB = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBoxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
            Invoke("Fall",fallDelay);
    }

    private void Fall() {
        myRB.isKinematic = false;
        myAnimator.SetTrigger("IsFalling");
        Invoke("DestroyGameObject", destroyDelay);
    }

    private void DestroyGameObject() {
        Destroy(gameObject);
    }
}
