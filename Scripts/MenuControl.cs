using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    GM GM;

    public bool paused;
    public bool startWithMenuUp;
    public GameObject menu;
    public GameObject menuCanvas;

    public static bool loading = false;

    void Awake()
    {
        GM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GM>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (startWithMenuUp == true)
        {
            paused = true;
            GM.questLogControl.onMenu = true;
        }
        else
        {
            paused = false;
        }

        if (paused)
        {
            menuCanvas.SetActive(true);
        }
        else
        {
            menuCanvas.SetActive(false);
        }
    }

    public void Resume()
    {
        menuCanvas.SetActive(false);
        paused = false;
    }

    public void New()
    {
        PlayerPrefs.SetFloat("nextScenePosX", 0f);
        PlayerPrefs.SetFloat("nextScenePosY", 0f);

        PlayerPrefs.SetInt("questNo", -1);
        PlayerPrefs.SetInt("questPart", 0);

        PlayerPrefs.Save();

        loading = false;

        GM.questLogControl.onMenu = true;
        QuestLogControl.tutorialSection = 1;

        PlayerPrefs.SetInt("Class", 0);
        GM.playerControl.playerAnimator.SetInteger("Class", 0);

        GM.playerControl.scene = "Redcliff";
        SceneManager.LoadScene("MakeCharacterMenu", LoadSceneMode.Single);
        Debug.Log("Scene Loaded");
    }

    public void Load()
    {
        loading = true;
        GM.gameSaveControl.LoadGame();
    }

    public void Save()
    {
        GM.playerPreferences.SaveGamePreferences();
        GM.gameSaveControl.SaveGame();
    }

    public void Quit()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
}
