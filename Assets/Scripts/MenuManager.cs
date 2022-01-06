using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.IO;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    public string theUserName;
    public string nextUserName;
    public int highScore;

    public InputField username;

    private void Awake()
    {
        if (Instance != null)
        {
            ////Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void StartGame()
    {
        Debug.Log("Press start game button");
        LoadHighScoreData();
        Debug.Log("Loaded user name was " + MenuManager.Instance.theUserName);
        MenuManager.Instance.nextUserName = username.text.ToString(); 
        Debug.Log("Menu Manager is " + MenuManager.Instance.theUserName + " MenuManager.Instance.nextUserName " + MenuManager.Instance.nextUserName);
        SceneManager.LoadScene(1);

    }
    public void QuitGame()
    {
#if UNITY_EDITOR 
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }


    [System.Serializable]
    class SaveData
    {
        public string username;
        public int highscore;
    }

    public void SaveHighScoreData()
    {
        SaveData data = new SaveData
        {
            username = MenuManager.Instance.theUserName,
            highscore = MainManager.highScore
        };

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadHighScoreData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            MenuManager.Instance.theUserName = data.username;
            MainManager.highScore = data.highscore;
        }
    }
}
