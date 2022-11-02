using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Windmill : MonoBehaviour
{
    [SerializeField]
    private bool rotateY;
    [SerializeField]
    private bool rotateX;
    [SerializeField]
    private bool rotateZ;

    [SerializeField]
    private float speed;


    void Start()
    {
        
    }

    void Update()
    {
        if (rotateY)
        {
            transform.Rotate(0, speed * Time.deltaTime, 0);
        }
        else if (rotateX)
        {
            transform.Rotate(speed * Time.deltaTime, 0, 0);
        }
        else if (rotateZ)
        {
            transform.Rotate(0, 0, speed * Time.deltaTime);
        }
    }
}
