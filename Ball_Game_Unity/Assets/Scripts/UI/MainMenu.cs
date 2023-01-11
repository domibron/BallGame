using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private TMP_Text highscore;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        highscore.text = "coins " + PlayerPrefs.GetFloat("highscore", 0) + " / 10";
    }

    public void LoadLevelInt(int levelIndex)
    {
        SceneManager.LoadSceneAsync(levelIndex, LoadSceneMode.Single);
    }

    public void ClearSave()
    {
        PlayerPrefs.SetFloat("highscore", 0);
        PlayerPrefs.Save();
    }
}
