using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int HighScore;
    public int LastGameScore;

    public string HighPlayer;
    public string LastPlayer;
    public string CurrentPlayer;

    public void Awake()
    {
        if (Instance != null)
        { 
            Destroy(gameObject);
            return;
        }
        Instance = this;
        LoadData();
        DontDestroyOnLoad(gameObject);
    }

    public void Exit()
    {
        SaveData();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    [System.Serializable]
    class SaveHighScore
    {
        public float Version = 1.2f;
        public int HighScore;
        public string HighPlayer;
        public int LastGameScore;
        public string LastPlayer;
    }

    public void SaveData()
    {
        SaveHighScore data = new SaveHighScore();
        data.HighScore = HighScore;
        data.HighPlayer = HighPlayer;
        data.LastGameScore = LastGameScore;
        data.LastPlayer = LastPlayer;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveHighScore data = JsonUtility.FromJson<SaveHighScore>(json);
            if(data.Version == 1.2f)
            {
                HighScore = data.HighScore;
                HighPlayer = data.HighPlayer;
                LastGameScore = data.LastGameScore;
                LastPlayer = data.LastPlayer;
            }
            else if(data.Version == 1.1f)
            {
                HighScore = data.HighScore;
                HighPlayer = data.HighPlayer;
                LastPlayer = data.LastPlayer;
            }
            else
            {
                HighScore = data.HighScore;
                HighPlayer = data.HighPlayer;
            }

        }
    }
}
