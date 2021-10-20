using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class GameSaveControl : MonoBehaviour
{
    private string saveFileName = "MyGame.dat";
    public GameSave gameSave;

    GM GM;

    void Awake()
    {
        GM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GM>();
    }

    public string SaveFileName
    {
        get
        {
            return saveFileName;
        }
        set
        {
            saveFileName = value;
        }
    }

    public void SaveGame()
    {
        DeleteFile(SaveFileName);
        ConstructSaveGame();

        Debug.Log("Saving file to " + Application.persistentDataPath + "/gamesaves/" + saveFileName);
        FileStream file = null;

        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            file = File.Create(Application.persistentDataPath + "/gamesaves/" + saveFileName);
            bf.Serialize(file, gameSave);
        }
        catch (Exception e)
        {
            if (e != null)
            {
                Debug.Log("Game save failed: " + e);
            }
        }
        finally //whether it has caught an error or not
        {
            if (file != null)
            {
                file.Close();
                Debug.Log("Game save complete");
            }
        }
    }

    public void LoadGame()
    {
        FileStream file = null;
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            file = File.Open(Application.persistentDataPath + "/gamesaves/" + saveFileName, FileMode.Open);
            gameSave = bf.Deserialize(file) as GameSave;
        }
        catch(Exception e)
        {
            if (e != null)
            {
                Debug.Log("Game load failed: " + e);
            }
        }
        finally
        {
            if (file != null)
            {
                file.Close();
                Debug.Log("Game load complete");
            }
        }

        DeconstructLoadGame();
    }


    void DeleteFile(string fileName)
    {
        string strippedName = fileName.Substring(0,
(fileName.Length - 4));  // gets file name less .dat

        Debug.Log("Stripped name = " + strippedName);
        strippedName = strippedName + ".OLD";

        string filePath = (Application.persistentDataPath + "/gamesaves/" + strippedName);
        //delete any .OLD file
        // check if file exists
        try
        {
            if(!File.Exists(filePath))
            {
                Debug.Log("no " + fileName + " file exists");
            }
            else
            {
                Debug.Log(fileName + " file exists, deleting...");

                File.Delete(filePath);
            }

            // now rename our exising  .DAT file to  a .OLD file
            string filePath2 = (Application.persistentDataPath + "/gamesaves/" + fileName);

            File.Move(filePath2, filePath);
        }
        catch (Exception e)
        {
            if (e != null)
            {
                Debug.Log("Game file rename to .old failed: " + e);
            }
        }
    }

    public void ConstructSaveGame()
    {
        gameSave.scene = GM.playerControl.scene;
        gameSave.questNo = UITextControl.questNo;
        gameSave.questPart = UITextControl.questPart;
        gameSave.gender = PlayerPrefs.GetInt("isMale");
        gameSave.playerClass = PlayerPrefs.GetInt("Class");
    }

    public void DeconstructLoadGame()
    {
        PlayerPrefs.SetInt("questNo", gameSave.questNo);
        PlayerPrefs.SetInt("questPart", gameSave.questPart);

        PlayerPrefs.SetInt("isMale", gameSave.gender);
        PlayerPrefs.SetInt("Class", gameSave.playerClass);

        try
        {
            SceneManager.LoadScene(gameSave.scene, LoadSceneMode.Single);
        }
        catch (Exception e)
        {
            Debug.Log("Scene " + gameSave.scene + " load failed: " + e);
        }
    }
}
