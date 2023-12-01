using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour {

    [SerializeField] private float myVerticalSpeed;
    [SerializeField] private float myHorizontalSpeed;
    [SerializeField] private float yAxisUpperLimit;
    [SerializeField] private float yAxisLowerLimit;
    [SerializeField] private float xAxisUpperLimit;
    [SerializeField] private float xAxisLowerLimit;
    [SerializeField] private bool yAxisDirectionChange = true;
    [SerializeField] private bool xAxisDirectionChange = true;

    private void Update() {
        Move();
    }

    private void Move() {
        transform.Translate(myHorizontalSpeed * Time.deltaTime, myVerticalSpeed * Time.deltaTime, 0);
        if (transform.position.y > yAxisUpperLimit && yAxisDirectionChange) {
            myVerticalSpeed = -myVerticalSpeed;
            yAxisDirectionChange = !yAxisDirectionChange;
        }
        if (transform.position.y < yAxisLowerLimit && !yAxisDirectionChange) {
            myVerticalSpeed = -myVerticalSpeed;
            yAxisDirectionChange = !yAxisDirectionChange;
        }
        if (transform.position.x > xAxisUpperLimit && xAxisDirectionChange ) {
            myHorizontalSpeed = -myHorizontalSpeed;
            xAxisDirectionChange = !xAxisDirectionChange;
        }
        if (transform.position.x < xAxisLowerLimit && !xAxisDirectionChange) {
            myHorizontalSpeed = -myHorizontalSpeed;
            xAxisDirectionChange = !xAxisDirectionChange;
        }
    }
}
