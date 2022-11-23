using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-1)] // this makes sure the checkpoint's data is set by running before anything else
public class CheckPointId : MonoBehaviour
{
    public int ID; // id for the checkpoint
    public GameObject _ThisObject;

    void Awake()
    {
        ID = GenerateID();
        _ThisObject = gameObject;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            CheckpointManager CM = other.GetComponentInParent<CheckpointManager>();

            CM.SetCurrentCheckpointID(ID);
        }
    }

    private int GenerateID()
    {
        string name = gameObject.name;
        string[] items = name.Split(' ');

        if (items.Length > 2)
        {
            // remember lists and arrays start at 0.

            // this just seperates the (4) for example into a array of 1 is (, 2 is 4, and 3 is ).
            string holder;
            char[] charArrayHolder;
            holder = items[2];
            charArrayHolder = holder.ToCharArray();

            // this makes a string but leaves out the brakets only leaving the numbers.
            holder = "";
            for (int i = 1; i < charArrayHolder.Length - 1; i++)
            {
                holder += charArrayHolder[i].ToString();
            }
            // ID is then passed.
            return int.Parse(holder);


        }
        else
            return 0; // base number.
    }
}
