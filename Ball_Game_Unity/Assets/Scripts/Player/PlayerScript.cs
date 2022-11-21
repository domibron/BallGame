using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    private float minimumDistance = .2f;
    [SerializeField]
    private float maximumTime = 1f;
    [SerializeField, Range(0f, 1f)]
    private float directionThreshold = .9f;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float swipeNurf;

    public Transform VertuialCamera;
    public Transform Orientation;
    public Transform MainCamera;
    public GameObject PlayerModel;

    public Slider SpeedSlider;
    public TMP_Text text;

    private InputManager inputManager;

    private Rigidbody rb;

    private Vector2 startPosision;
    private Vector2 endPosision;
    private float startTime;
    private float endTime;

    private float yRotation;

    private Coroutine coroutine;

    private float endY;
    private float startY;

    private float rotateTo;
    private float oldRotation;
    private float PlayerModelY;

    private float time;

    private float swipeDistance;


    void Awake()
    {
        inputManager = InputManager.Instance;
        rb = GetComponent<Rigidbody>();

        //print(PlayerModel.transform.rotation.y);

        SpeedSlider.value = speed;
    }

    void Update() // UPDATE what exactly?
    {
        // scrap the rotation
        // scrap the rotation
        // no

        speed = SpeedSlider.value;
        text.text = speed.ToString();

        time += Time.deltaTime * 50f;
        PlayerModelY = Mathf.Lerp(oldRotation, rotateTo, time);

        PlayerModel.transform.rotation = Quaternion.Euler(PlayerModel.transform.rotation.x, PlayerModelY, PlayerModel.transform.rotation.z);

        //RaycastHit hit;
        //if (Physics.Raycast(transform.position, Orientation.forward, out hit, 30f))
        //{
            
        //    Debug.DrawLine(transform.position, hit.point, Color.cyan, 5f);
        //}
    }

    public void FireScaleRay(bool scaleDown)
    {
        RaycastHit hit;
        GameObject item;
        bool hitConfirmed;

        // this sets a bool while getting a hit of a object from a raycast.
        hitConfirmed = Physics.Raycast(transform.position, Orientation.forward, out hit, 30f);

        Debug.DrawRay(transform.position, Orientation.forward, Color.red); // shows line of ray.

        // this checks if there was no hit or a object is not scalable and end the function
        if (!hitConfirmed || hit.transform.tag == "NoneScalable")
            return;

        item = hit.transform.gameObject;

        // depending on the bool set in the function it will secale the objust down or up by 2.
        if (scaleDown)
            item.transform.localScale = new Vector3(item.transform.localScale.x / 2f, item.transform.localScale.y / 2f, item.transform.localScale.z / 2f);
        else
            item.transform.localScale = new Vector3(item.transform.localScale.x * 2f, item.transform.localScale.y * 2f, item.transform.localScale.z * 2f);

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
            swipeDistance = Vector3.Distance(endPosision, startPosision); // gets the distance of the two points.
            swipeDistance = swipeDistance < 1f ? 1f : swipeDistance; // make sure the value is never below 1.
            swipeDistance = swipeDistance / swipeNurf; // loweres the value.
        }
    }

    private void SwipeDirection(Vector2 direction)
    {
        //print(direction);
        float percentOfSwipe = Vector2.Dot(Vector2.up, direction);
        float rotationY = 180 * VertuialCamera.rotation.y; //  | try local with main camera | it works fine
        float eulerRotationY = VertuialCamera.eulerAngles.y;

        if (Vector2.Dot(Vector2.up, direction) > directionThreshold)
        {
            //Orientation.transform.rotation = Quaternion.Euler(Orientation.rotation.x, rotationY, Orientation.rotation.z);
            Orientation.rotation = Quaternion.Euler(0f, eulerRotationY, 0f);
            rb.AddForce(Orientation.forward * speed * swipeDistance, ForceMode.Impulse);

            // rotate model - REMOVE
            //oldRotation = 180 * PlayerModel.transform.rotation.y;
            //time = 0;
            //rotateTo = rotationY;

            
        }
        else if (Vector2.Dot(Vector2.down, direction) > directionThreshold)
        {
            Orientation.rotation = Quaternion.Euler(0f, eulerRotationY + 180f, 0f);
            rb.AddForce(Orientation.forward * speed * swipeDistance, ForceMode.Impulse);
        }
        else if (direction.x > 0) // right
        {

            //print("right " + percentOfSwipe);

            if (direction.y > 0) // can use the whole 90 to -90 / 1 to -1
            {
                // top right 90 to 0
                percentOfSwipe = 1 - percentOfSwipe; // inverts the percent so is 0 to 90

                //float ninetyPercent = 90 * percentOfSwipe;
                //float ammountToRotate = rotationY + ninetyPercent;

                //Orientation.transform.rotation = Quaternion.Euler(Orientation.rotation.x, ammountToRotate, Orientation.rotation.z);

                Orientation.rotation = Quaternion.Euler(0f, eulerRotationY + (percentOfSwipe * 100f), 0f);

                rb.AddForce(Orientation.forward * speed * swipeDistance, ForceMode.Impulse);

                // rotate model - REMOVE
                //oldRotation = 180 * PlayerModel.transform.rotation.y;
                //time = 0;
                //rotateTo = ammountToRotate;
                
                //print(oldRotation + " -old | ammo- " + ammountToRotate);
            }
            else
            {
                // bottom right 90 to 180

                //float ninetyPercent = 90 * -percentOfSwipe; // look at this -Var might not work

                //float ammountToRotate = (rotationY + 90) + ninetyPercent;

                //Orientation.transform.rotation = Quaternion.Euler(Orientation.rotation.x, ammountToRotate, Orientation.rotation.z);

                Orientation.rotation = Quaternion.Euler(0f, eulerRotationY + 90f +(-percentOfSwipe * 100f), 0f);

                rb.AddForce(Orientation.forward * speed * swipeDistance, ForceMode.Impulse);

                // rotate model - REMOVE
                //oldRotation = 180 * PlayerModel.transform.rotation.y;
                //time = 0;
                //rotateTo = ammountToRotate;

                //print(oldRotation + " -old | ammo- " + ammountToRotate);
            }
        }
        else // left
        {
            //print("Left " + percentOfSwipe);

            if (direction.y > 0)
            {
                // top left, -90 to 0

                percentOfSwipe = 1 - percentOfSwipe; // inverts the percent so is 0 to -90

                //float ninetyPercent = 90 * -percentOfSwipe; // look at this -Var might not work

                //float ammountToRotate = rotationY + ninetyPercent;

                //Orientation.transform.rotation = Quaternion.Euler(Orientation.rotation.x, ammountToRotate, Orientation.rotation.z);

                Orientation.rotation = Quaternion.Euler(0f, eulerRotationY + (-percentOfSwipe * 100f), 0f);

                rb.AddForce(Orientation.forward * speed * swipeDistance, ForceMode.Impulse);

                // rotate model - REMOVE
                //oldRotation = 180 * PlayerModel.transform.rotation.y;
                //time = 0;
                //rotateTo = ammountToRotate;

                //print(oldRotation + " -old | ammo- " + ammountToRotate);
            }
            else
            {

                // bottom left -90 to -180
                //float ninetyPercent = 90 * percentOfSwipe;

                //float ammountToRotate = (rotationY - 90) + ninetyPercent;

                //Orientation.transform.rotation = Quaternion.Euler(Orientation.rotation.x, ammountToRotate, Orientation.rotation.z);

                Orientation.rotation = Quaternion.Euler(0f, eulerRotationY + -90f + (percentOfSwipe * 100f), 0f);

                rb.AddForce(Orientation.forward * speed * swipeDistance, ForceMode.Impulse);

                // rotate model - REMOVE
                //oldRotation = 180 * PlayerModel.transform.rotation.y;
                //time = 0;
                //PlayerModelY = ammountToRotate;

                //print(oldRotation + " -old | ammo- " + ammountToRotate);
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

    public void LoadLevlInt(int levelIndex)
    {
        SceneManager.LoadSceneAsync(levelIndex, LoadSceneMode.Single);
    }
}
