using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreateCharacterControl : MonoBehaviour
{
    GM GM;

    void Awake()
    {
        GM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GM>();
    }

    public void Back()
    {
        MenuControl.loading = false;
        GM.questLogControl.onMenu = true;

        GM.playerControl.scene = "Menu";
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        Debug.Log("Scene Loaded");
    }

    public void Male()
    {
        GM.playerControl.playerAnimator.SetBool("IsMale", true);
        PlayerPrefs.SetInt("isMale", 1);
        PlayerPrefs.Save();

        GM.questLogControl.onMenu = false;

        MenuControl.loading = false;
        GM.playerControl.scene = "Redcliff";
        SceneManager.LoadScene("Redcliff", LoadSceneMode.Single);
        Debug.Log("Scene Loaded");
    }

    public void Female()
    {
        GM.playerControl.playerAnimator.SetBool("IsMale", false);
        PlayerPrefs.SetInt("isMale", 0);
        PlayerPrefs.Save();

        GM.questLogControl.onMenu = false;

        MenuControl.loading = false;
        GM.playerControl.scene = "Redcliff";
        SceneManager.LoadScene("Redcliff", LoadSceneMode.Single);
        Debug.Log("Scene Loaded");
    }

}
