using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    // this is where the two points will be referenced.
    public Transform PointA;
    public Transform PointB;

    public float speed;
    public float delayBeforeMoving;

    private bool goToPointB;

    void Update()
    {
        StartCoroutine(CheckIfPlatformIsAtPoint());
    }

    void FixedUpdate()
    {
        MovePlatform();
    }

    private IEnumerator CheckIfPlatformIsAtPoint()
    {
        // checks to see if the platform is at target A position.
        // if it reaches the point it will pause then change direction.
        if (transform.position == PointA.position)
        {
            yield return new WaitForSeconds(delayBeforeMoving);
            goToPointB = true;
        }

        if (transform.position == PointB.position)
        {
            yield return new WaitForSeconds(delayBeforeMoving);
            goToPointB = false;
        }
    }

    private void MovePlatform()
    {
        // checks the bool to see where the platform should go to.
        if (goToPointB)
            transform.position = Vector3.MoveTowards(transform.position, PointB.position, speed * Time.deltaTime);
        else
            transform.position = Vector3.MoveTowards(transform.position, PointA.position, speed * Time.deltaTime);
    }
}
