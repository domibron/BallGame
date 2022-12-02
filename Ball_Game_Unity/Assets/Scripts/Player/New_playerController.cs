using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

public class New_playerController : MonoBehaviour
{

    private JoyStick playerInput;
    private Rigidbody rb;

    public Transform orient;
    public Transform VertuialCamera;


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
        Vector2 movementInput = playerInput.PlayerMain.Look.ReadValue<Vector2>();
        Vector3 move = new Vector3(movementInput.x, 0f, movementInput.y);

        float eulerRotationY = VertuialCamera.eulerAngles.y;

        if (move != Vector3.zero)
        {
            // orient.rotation = Quaternion.Euler(move);
            // rb.AddForce(orient.forward * 0.1f, ForceMode.Impulse);
            float product;

            product = Vector2.Dot(Vector2.up, movementInput);

            if (movementInput.x > 0) // right
            {
                if (movementInput.y > 0) // top
                {
                    product = 1 - product;
                    orient.rotation = Quaternion.Euler(0f, eulerRotationY + (product * 100f), 0f);
                    rb.AddForce(orient.forward * 1f * 1f, ForceMode.Acceleration);
                }
                else // bottom
                {
                    orient.rotation = Quaternion.Euler(0f, eulerRotationY + 90f + (-product * 100f), 0f);
                    rb.AddForce(orient.forward * 1f * 1f, ForceMode.Acceleration);
                }
            }
            else // left
            {
                if (movementInput.y > 0) // top
                {
                    product = 1 - product;
                    orient.rotation = Quaternion.Euler(0f, eulerRotationY + (-product * 100f), 0f);
                    rb.AddForce(orient.forward * 1f * 1f, ForceMode.Acceleration);
                }
                else // bottom
                {
                    orient.rotation = Quaternion.Euler(0f, eulerRotationY + -90f + (product * 100f), 0f);
                    rb.AddForce(orient.forward * 1f * 1f, ForceMode.Acceleration);
                }
            }
        }
    }
}
