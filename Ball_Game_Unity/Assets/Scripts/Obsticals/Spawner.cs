using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float minDelay = 1f;
    public float maxDelay = 3f;

    public float deletionTime = 4f;

    public GameObject prefab;

    private float time;
    private float hold = 0f;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time >= hold)
        {
            hold = time + Random.Range(minDelay, maxDelay);
            GameObject go;
            go = Instantiate(prefab, transform.position, Quaternion.identity, transform);
            GameObject.Destroy(go, deletionTime);

        }
    }
}
