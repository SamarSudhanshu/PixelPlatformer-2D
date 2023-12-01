using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleArrow : MonoBehaviour {

    private float delayTime = 0.5f;
    private Animator myAnime;    

    private void Awake() {
        myAnime = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        myAnime.SetTrigger("IsHitting");
        Invoke("DestroyGameObject", delayTime);
    }

    private void DestroyGameObject() {
        Destroy(gameObject);
    }
}
