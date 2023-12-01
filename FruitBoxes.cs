using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitBoxes : MonoBehaviour {
    [SerializeField] private float destroyGameObjectDelayTime = 1f;
    [SerializeField] private int numOfTimesBoxNeedToBeHitten;
    private int numberOfFruits = 0;
    [SerializeField] private GameObject[] fruits;
    [SerializeField] private Transform fruitSpawnPoint;
    private int numOfTimesBoxIsHitten;
    private Animator myAnimator;
    
    private void Awake() {
        myAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        myAnimator.SetTrigger("IsHitting");
        numOfTimesBoxIsHitten++;
        if(numOfTimesBoxIsHitten == numOfTimesBoxNeedToBeHitten) {
            Invoke("DestroyGameObject", destroyGameObjectDelayTime);
            return;
        }
        Instantiate(fruits[numberOfFruits], fruitSpawnPoint);
        numberOfFruits++;
    }

    private void DestroyGameObject() {
        Destroy(gameObject);
    }
}
