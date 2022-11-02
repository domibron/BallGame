using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private bool swingingRight = true;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        print(transform.eulerAngles.z);

        if (transform.eulerAngles.z >= 45f)
        {
            swingingRight = false;
        }
        else if (transform.eulerAngles.z <= 315f)
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
