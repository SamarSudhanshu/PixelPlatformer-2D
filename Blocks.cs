using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : MonoBehaviour {

    [SerializeField] private float destroyDelayTime = 1f;
    private Animator myAnimator;

    private void Awake() {
        myAnimator = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        myAnimator.SetTrigger("IsHitting");
        Invoke("DestroyGameObject", destroyDelayTime);
    }

    private void DestroyGameObject() {
        Destroy(gameObject);
    }
}
