using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    [SerializeField]
    private float speed = 50f;
    [SerializeField, Range(0.1f, 0.9f)]
    private float swingRange = 0.45f;

    private bool swingingRight = true;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        print($"{swingingRight} - {transform.localRotation.z} - {swingRange}");

        if (transform.localRotation.z >= swingRange)
        {
            swingingRight = false;
        }
        else if (transform.localRotation.z <= -swingRange)
        {
            swingingRight = true;
        }

        if (swingingRight)
        {
            transform.Rotate(0f, 0f, speed * Time.deltaTime, Space.Self);
        }
        else if (!swingingRight)
        {
            transform.Rotate(0f, 0f, -speed * Time.deltaTime, Space.Self);
        }
    }
}
