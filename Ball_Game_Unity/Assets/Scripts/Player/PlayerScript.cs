using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    private float minimumDistance = .2f;
    [SerializeField]
    private float maximumTime = 1f;
    [SerializeField, Range(0f, 1f)]
    private float directionThreshold = .9f;
    [SerializeField]
    private float rotationAmmount;

    public Transform VertuialCamera;
    public Transform Orientation;
    public Transform MainCamera;
    public GameObject PlayerModel;


    private InputManager inputManager;

    private Rigidbody rb;

    private Vector2 startPosision;
    private float startTime;
    private Vector2 endPosision;
    private float endTime;

    private float yRotation;

    private Coroutine coroutine;

    private float endY;
    private float startY;

    private float time;


    void Awake()
    {
        inputManager = InputManager.Instance;
        rb = GetComponent<Rigidbody>();
    }

    void Update() // UPDATE
    {
        if (PlayerModel.transform.rotation.y == Orientation.rotation.y)
            time = 0;
        else
            time += Time.deltaTime * 1f;

        PlayerModel.transform.rotation = Quaternion.Euler(PlayerModel.transform.rotation.x, Mathf.Lerp(PlayerModel.transform.rotation.y, Orientation.rotation.y, time), PlayerModel.transform.rotation.z);
    }
    
    private void rotatePlayer(float start, float end, float speed = 1)
    {
        
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
        if (Vector3.Distance(startPosision, endPosision) >= minimumDistance && (endTime - startTime) <= maximumTime)
        {
            Vector3 direction = endPosision - startPosision;
            Vector2 direction2D = new Vector2(direction.x, direction.y).normalized;
            SwipeDirection(direction2D);
        }
    }

    private void SwipeDirection(Vector2 direction)
    {
        //print(direction);
        float percentOfSwipe = Vector2.Dot(Vector2.up, direction);
        float rotationY = 180 * VertuialCamera.rotation.y; // try local with main camera

        if (Vector2.Dot(Vector2.up, direction) > directionThreshold)
        {
            Orientation.transform.rotation = Quaternion.Euler(Orientation.rotation.x, rotationY, Orientation.rotation.z);
            rb.AddForce(Orientation.forward, ForceMode.Impulse);
        }
        else if (direction.x > 0) // right
        {

            print("right " + percentOfSwipe);

            if (direction.y > 0) // can use the whole 90 to -90 / 1 to -1
            {
                // top right 90 to 0
                percentOfSwipe = 1 - percentOfSwipe; // inverts the percent so is 0 to 90

                float ninetyPercent = 90 * percentOfSwipe;

                float ammountToRotate = rotationY + ninetyPercent;

                Orientation.transform.rotation = Quaternion.Euler(Orientation.rotation.x, ammountToRotate, Orientation.rotation.z);
                rb.AddForce(Orientation.forward, ForceMode.Impulse);

            }
            else
            {
                // bottom right 90 to 180

                float ninetyPercent = 90 * -percentOfSwipe; // look at this -Var might not work

                float ammountToRotate = (rotationY + 90) + ninetyPercent;

                Orientation.transform.rotation = Quaternion.Euler(Orientation.rotation.x, ammountToRotate, Orientation.rotation.z);
                rb.AddForce(Orientation.forward, ForceMode.Impulse);

            }

        }
        else // left
        {
            print("Left " + percentOfSwipe);

            if (direction.y > 0)
            {
                // top left, -90 to 0

                percentOfSwipe = 1 - percentOfSwipe; // inverts the percent so is 0 to -90

                float ninetyPercent = 90 * -percentOfSwipe; // look at this -Var might not work

                float ammountToRotate = rotationY + ninetyPercent;

                Orientation.transform.rotation = Quaternion.Euler(Orientation.rotation.x, ammountToRotate, Orientation.rotation.z);
                rb.AddForce(Orientation.forward, ForceMode.Impulse);


            }
            else
            {

                // bottom left -90 to -180
                float ninetyPercent = 90 * percentOfSwipe;

                float ammountToRotate = (rotationY - 90) + ninetyPercent;

                Orientation.transform.rotation = Quaternion.Euler(Orientation.rotation.x, ammountToRotate, Orientation.rotation.z);
                rb.AddForce(Orientation.forward, ForceMode.Impulse);

            }
        }












        //print(Vector2.Dot(Vector2.up, direction));

        //if (Vector2.Dot(Vector2.up, direction) > directionThreshold)
        //{
        //	//print("swiped up");
        //	rb.AddForce(Orientation.forward, ForceMode.Impulse);
        //}
        //else if (Vector2.Dot(Vector2.right, direction) > directionThreshold)
        //{
        //	//print("swiped right");
        //	yRotation += rotationAmmount;
        //	//transform.rotation = Quaternion.Euler(transform.rotation.x + rotationAmmount, transform.rotation.y, transform.rotation.z);
        //	Orientation.transform.rotation = Quaternion.Euler(Orientation.rotation.x, yRotation, Orientation.rotation.z);

        //}
        //else if (Vector2.Dot(Vector2.down, direction) > directionThreshold)
        //{
        //	//print("swiped down");
        //	rb.AddForce(-Orientation.forward, ForceMode.Impulse);
        //}
        //else if (Vector2.Dot(Vector2.left, direction) > directionThreshold)
        //{
        //	//print("swiped left");
        //	yRotation -= rotationAmmount;
        //	//transform.rotation = Quaternion.Euler(transform.rotation.x - rotationAmmount, transform.rotation.y, transform.rotation.z);
        //	Orientation.transform.rotation = Quaternion.Euler(Orientation.rotation.x, yRotation, Orientation.rotation.z);
        //}
    }
}
