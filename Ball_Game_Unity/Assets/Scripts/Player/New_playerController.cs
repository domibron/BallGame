using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UI;
using TMPro;


public class New_playerController : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private float boostSpeed;
    private JoyStick playerInput;
    private Rigidbody rb;

    public Transform orient;
    public Transform VertuialCamera;
    public Slider SpeedSlider;
    public TMP_Text text;


    void Awake()
    {
        playerInput = new JoyStick();
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        playerInput.Enable();
    }

    void OnDisable()
    {
        playerInput.Disable();
    }

    void Update()
    {
        speed = SpeedSlider.value;
        text.text = speed.ToString("F1");

        Vector2 movementInput = playerInput.PlayerMain.Move.ReadValue<Vector2>();
        Vector3 move = new Vector3(movementInput.x, 0f, movementInput.y);

        float eulerRotationY = VertuialCamera.eulerAngles.y;

        if (move != Vector3.zero)
        {
            // orient.rotation = Quaternion.Euler(move);
            // rb.AddForce(orient.forward * 0.1f, ForceMode.Impulse);
            float product;

            product = Vector2.Dot(Vector2.up, movementInput);

            MovePlayer(movementInput, eulerRotationY, product);
        }

        if (playerInput.PlayerMain.Move.ReadValue<Vector2>().magnitude * 10f > 1)
        {
            boostSpeed = playerInput.PlayerMain.Move.ReadValue<Vector2>().magnitude * 5f;
        }
        else
        {
            boostSpeed = 1;

        }
    }

    private void MovePlayer(Vector2 movementInput, float eulerRotationY, float product)
    {
        if (movementInput.x > 0) // right
        {
            if (movementInput.y > 0) // top
            {
                product = 1 - product;
                orient.rotation = Quaternion.Euler(0f, eulerRotationY + (product * 100f), 0f);
                rb.AddForce(orient.forward * speed * boostSpeed, ForceMode.Acceleration);
            }
            else // bottom
            {
                orient.rotation = Quaternion.Euler(0f, eulerRotationY + 90f + (-product * 100f), 0f);
                rb.AddForce(orient.forward * speed * boostSpeed, ForceMode.Acceleration);
            }
        }
        else // left
        {
            if (movementInput.y > 0) // top
            {
                product = 1 - product;
                orient.rotation = Quaternion.Euler(0f, eulerRotationY + (-product * 100f), 0f);
                rb.AddForce(orient.forward * speed * boostSpeed, ForceMode.Acceleration);
            }
            else // bottom
            {
                orient.rotation = Quaternion.Euler(0f, eulerRotationY + -90f + (product * 100f), 0f);
                rb.AddForce(orient.forward * speed * boostSpeed, ForceMode.Acceleration);
            }
        }
    }

    public void FireScaleRay(bool scaleDown)
    {
        RaycastHit hit;
        GameObject item;
        bool hitConfirmed;

        // this sets a bool while getting a hit of a object from a raycast.
        hitConfirmed = Physics.Raycast(transform.position, orient.forward, out hit, 30f);

        Debug.DrawRay(transform.position, orient.forward, Color.red); // shows line of ray.

        // this checks if there was no hit or a object is not scalable and end the function
        if (!hitConfirmed || hit.transform.tag == "NoneScalable" || hit.transform.tag != "Scalable")
            return;

        item = hit.transform.gameObject;

        // depending on the bool set in the function it will secale the objust down or up by 2.
        if (scaleDown)
            item.transform.localScale = new Vector3(item.transform.localScale.x / 1.5f, item.transform.localScale.y / 1.5f, item.transform.localScale.z / 1.5f);
        else
            item.transform.localScale = new Vector3(item.transform.localScale.x * 1.5f, item.transform.localScale.y * 1.5f, item.transform.localScale.z * 1.5f);

    }
}
