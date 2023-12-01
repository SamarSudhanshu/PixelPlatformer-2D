using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolineController : MonoBehaviour {
    private CapsuleCollider2D myCapsuleCollider;
    private Animator myAnimator;

    private void Start() {
        myAnimator = GetComponent<Animator>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();    
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
            myAnimator.SetTrigger("IsJumping");
    }
}
