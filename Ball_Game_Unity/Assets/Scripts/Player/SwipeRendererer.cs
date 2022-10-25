using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeRendererer : MonoBehaviour
{
    [SerializeField]
    private float minimumDistance = .2f;
    [SerializeField]
    private float maximumTime = 1f;
    [SerializeField, Range(0f, 1f)]
    private float directionThreshold = .9f;



    private InputManager inputManager;

    private Rigidbody rb;

    private Vector2 startPosision;
    private float startTime;
    private Vector2 endPosision;
    private float endTime;

    private Coroutine coroutine;

    void Awake()
    {
        inputManager = InputManager.Instance;
        rb = GetComponent<Rigidbody>();
    }





    void OnEnable()
    {
        inputManager.OnStartPrimaryTouch += SwipeStart;
        inputManager.OnEndPrimaryTouch += SwipeEnd;
    }

    void OnDisable()
    {
        inputManager.OnStartPrimaryTouch -= SwipeStart;
        inputManager.OnEndPrimaryTouch -= SwipeEnd;
    }

    private void SwipeStart(Vector2 pos, float time)
    {
        startPosision = pos;
        startTime = time;
    }

    private void SwipeEnd(Vector2 pos, float time)
    {

        endPosision = pos;
        endTime = time;
        DetectSwipe();
    }



    private void DetectSwipe()
    {
        if (Vector3.Distance(startPosision, endPosision) >= minimumDistance &&
            (endTime - startTime) <= maximumTime)
        {
            Debug.Log("swiped!");
            Debug.DrawLine(startPosision, endPosision, Color.red);
            Vector3 direction = endPosision - startPosision;
            Vector2 direction2D = new Vector2(direction.x, direction.y).normalized;
            SwipeDirection(direction2D);
        }
    }

    private void SwipeDirection(Vector2 direction)
    {
        if (Vector2.Dot(Vector2.up, direction) > directionThreshold)
        {
            //print("swiped up");
            rb.AddForce(Vector3.forward, ForceMode.Impulse);
        }
        else if (Vector2.Dot(Vector2.right, direction) > directionThreshold)
        {
            //print("swiped right");

        }
        else if (Vector2.Dot(Vector2.down, direction) > directionThreshold)
        {
            //print("swiped down");
            rb.AddForce(Vector3.back, ForceMode.Impulse);
        }
        else if (Vector2.Dot(Vector2.left, direction) > directionThreshold)
        {
            //print("swiped left");

        }
    }
}
