using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruits : MonoBehaviour {
    [SerializeField] private float destroyDelayTime = 0.5f;
    private CircleCollider2D myCircleCollider;
    private Animator myAnimator;

    private void Start() {
        myCircleCollider = GetComponent<CircleCollider2D>();
        myAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(myCircleCollider.IsTouchingLayers(LayerMask.GetMask("Player"))) {
            myAnimator.SetTrigger("IsCollected");
            Invoke("DestroyTheGameObject", destroyDelayTime);
        }
    }

    private void DestroyTheGameObject() {
        Destroy(gameObject);
    }
}
