using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MainManager : MonoBehaviour
{
    public static MainManager instance;
    public Color teamColor;

    private void Awake()
    {
        //If the instance is just setted in another main manager
        if (instance != null)
        {
            Destroy(gameObject);//Destroy this main manager because exists another game manager
            return;//Returns the function
        }
        instance = this;//set this as instance of main manager
        DontDestroyOnLoad(gameObject);//Set don't destroy on load this game object

        LoadColor();//Load the color from the saving file, if exists
    }

    [System.Serializable]
    class SaveData
    {
        public Color teamColor;
    }

    public void SaveColor()
    {
        SaveData data = new SaveData();
        data.teamColor = teamColor;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadColor()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            teamColor = data.teamColor;
        }
    }
}
