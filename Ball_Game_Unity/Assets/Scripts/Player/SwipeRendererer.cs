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
    [SerializeField]
    private GameObject trail; // trail related



    private InputManager inputManager;

    private Vector2 startPosision;
    private float startTime;
    private Vector2 endPosision;
    private float endTime;

    private Coroutine coroutine;

    void Awake()
    {
        inputManager = InputManager.Instance;
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

        trail.SetActive(true); // trail related
        trail.transform.position = pos; // trail related

        coroutine = StartCoroutine(Trail()); // trail related
    }


    // remove trail
    private IEnumerator Trail() // trail related
    {
        while (true)
        {
            trail.transform.position = inputManager.PrimaryPosition(); // trail related
            yield return null;
        }
    }

    private void SwipeEnd(Vector2 pos, float time)
    {
        trail.SetActive(false); // trail related
        StopCoroutine(coroutine); // trail related

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

        }
        else if (Vector2.Dot(Vector2.right, direction) > directionThreshold)
        {
            //print("swiped right");

        }
        else if (Vector2.Dot(Vector2.down, direction) > directionThreshold)
        {
            //print("swiped down");

        }
        else if (Vector2.Dot(Vector2.left, direction) > directionThreshold)
        {
            //print("swiped left");

        }
    }
}
