using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    [SerializeField]
    GameObject Coin;
    [SerializeField]
    GameObject YCoinpoint;

    [SerializeField, Range(0.01f, 1000)]
    float speed;

    public bool collected = false;

    // Start is called before the first frame update
    void Start()
    {
        // only have one script run these lines in future.
        PlayerPrefs.SetFloat("coins", 0);
        PlayerPrefs.Save();

    }

    // Update is called once per frame
    void Update()
    {
        if (Coin.activeSelf && !collected)
        {
            YCoinpoint.transform.Rotate(0, -speed * Time.deltaTime, 0);
            Coin.transform.Rotate(0, (speed * 2) * Time.deltaTime, (speed * 2) * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponentsInParent<CheckpointManager>();

            collected = true;
            Coin.SetActive(false);
            GetComponent<CapsuleCollider>().enabled = false;

            float y = PlayerPrefs.GetFloat("highscore", 0);
            float x = PlayerPrefs.GetFloat("coins");
            x++;

            if (x > y)
            {
                PlayerPrefs.SetFloat("highscore", x);
                PlayerPrefs.Save();
            }

            PlayerPrefs.SetFloat("coins", x);
            PlayerPrefs.Save();

            print("yes" + x);
        }
    }
}
