using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour {
    private int currentSceneIndex;
    private int i = 1;
    private Animator myAnimator;
    private int nextSceneIndex;
    private float delayTime = 2f;

    private void Awake() {
        myAnimator = GetComponent<Animator>();
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        nextSceneIndex = currentSceneIndex + 1;
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
    public void Play() {
        SceneManager.LoadScene("Level_0");
    }
    
    public void ExitGame() {
        Application.Quit();
    }

    public void RestartLevel() {
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void Back() {
        SceneManager.LoadScene(0);
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
