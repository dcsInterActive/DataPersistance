using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // for SceneManager
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuManager : MonoBehaviour
{
    public GameObject button;
    public GameObject inputField;

    public Text text;

    public Text HighScore;
    public Text LastScore;

    public void Start()
    {
        //Instance = this;
        //StartCoroutine(SetupMenuScreen());
        if (GameManager.Instance != null)
        {
            button.SetActive(false);
            if (GameManager.Instance.LastPlayer != "") LastScore.text = GameManager.Instance.LastPlayer + " : " + GameManager.Instance.LastGameScore;
            if (GameManager.Instance.HighPlayer != "") HighScore.text = GameManager.Instance.HighPlayer + " : " + GameManager.Instance.HighScore;
            inputField.SetActive(true);
        }
        else
        {
            Debug.Log("GameManager Instance is null");
            Quit();
        }
    }

    public void Quit()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.Exit();
        }
        else 
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void NameUpdate()
    {
        if(text.text != "")
        {
            GameManager.Instance.CurrentPlayer = text.text;
            button.SetActive(true);
        }   
    }
}
