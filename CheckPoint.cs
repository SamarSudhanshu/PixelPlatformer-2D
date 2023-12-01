using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPoint : MonoBehaviour {
    private int i = 1;
    private Animator myAnimator;
    private int currentSceneIndex;
    private int nextSceneIndex;
    private float delayTime = 2f;

    private void Awake() {
        myAnimator = GetComponent<Animator>();
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        nextSceneIndex = currentSceneIndex + 1;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") && i == 1) {
            myAnimator.SetTrigger("IsOuting");
            i++;
            Invoke("NextLevel", delayTime);
        }
    }

    private void NextLevel() {
        SceneManager.LoadScene(nextSceneIndex);
    }
}
