using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointIncrease : MonoBehaviour
{
    public bool isStart = false;

    private void Start()
    {
        if (isStart)
            this.GetComponent<BoxCollider>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        CheckpointManager CM = other.GetComponentInParent<CheckpointManager>();

        if (other.transform.tag == "Player")
        {
            CM.currentCheckpoint++;
            this.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
