using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerPreferences : MonoBehaviour
{
    GM GM;

    // Start is called before the first frame update
    void Awake()
    {
        
        GM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GM>();
        //Debug.Log("Player preferences GM = " + GM);
    }

    public void SaveGamePreferences()
    {
        PlayerPrefs.SetFloat("playerPosX", GM.playerControl.position.x);
        PlayerPrefs.SetFloat("playerPosY", GM.playerControl.position.y);

        
        PlayerPrefs.SetInt("questNo", UITextControl.questNo);
        PlayerPrefs.SetInt("questPart", UITextControl.questPart);

        Debug.Log("Saved quest = " + UITextControl.questNo + UITextControl.questPart);

        if (GM.playerControl.playerAnimator.GetBool("IsMale") == true)
        {
            PlayerPrefs.SetInt("isMale", 1);
        }
        else if (GM.playerControl.playerAnimator.GetBool("IsMale") == false)
        {
            PlayerPrefs.SetInt("isMale", 0);
        }
        Debug.Log("gender saved (male = 1, female = 0) = " + PlayerPrefs.GetInt("isMale"));

        PlayerPrefs.SetInt("Class", GM.playerControl.playerAnimator.GetInteger("Class"));

        PlayerPrefs.Save();
    }

    public void SaveScenePreferences()
    {
        PlayerPrefs.SetInt("questNo", UITextControl.questNo);
        PlayerPrefs.SetInt("questPart", UITextControl.questPart);

        if (GM.playerControl.playerAnimator.GetBool("IsMale") == true)
        {
            PlayerPrefs.SetInt("isMale", 1);
        }
        else if (GM.playerControl.playerAnimator.GetBool("IsMale") == false)
        {
            PlayerPrefs.SetInt("isMale", 0);
        }

        PlayerPrefs.SetInt("Class", GM.playerControl.playerAnimator.GetInteger("Class"));

        PlayerPrefs.Save();
    }

    public void LoadGamePreferences()
    {

        if (PlayerPrefs.HasKey("playerPosX"))

        {
            //Debug.Log("test position = " + PlayerPrefs.GetFloat("playerPosX"));
        }
        else
        {
            Debug.Log("test position failed");
        }
        
        try
        {
            Debug.Log("PlayerPreferences playerControl = " + GM.playerControl);
            GM.playerControl.position.x = PlayerPrefs.GetFloat("playerPosX");
            GM.playerControl.position.y = PlayerPrefs.GetFloat("playerPosY");
            //Debug.Log("Saved position = " + PlayerPrefs.GetFloat("playerPosX") + PlayerPrefs.GetFloat("playerPosY"));
        }
        catch (Exception e)
        {
            Debug.Log("Player position failed to load: " + e);
        }

        try
        {
            UITextControl.questNo = PlayerPrefs.GetInt("questNo");
            UITextControl.questPart = PlayerPrefs.GetInt("questPart");
        }
        catch (Exception e)
        {
            Debug.Log("Quests failed to load: " + e);
        }

        try
        {

            if (PlayerPrefs.GetInt("isMale") == 1)
            {
                GM.playerControl.playerAnimator.SetBool("IsMale", true);
            }
            else if (PlayerPrefs.GetInt("isMale") == 0)
            {
                GM.playerControl.playerAnimator.SetBool("IsMale", false);
            }


            Debug.Log("gender loaded (male = true, female = false) = " + GM.playerControl.playerAnimator.GetBool("IsMale"));
        }
        catch (Exception e)
        {
            Debug.Log("Gender failed to load: " + e);
        }

        try
        {
            GM.playerControl.playerAnimator.SetInteger("Class", PlayerPrefs.GetInt("Class"));
        }
        catch (Exception e)
        {
            Debug.Log("Class failed to load: " + e);
        }
    }

    public void LoadScenePreferences()
    {
        UITextControl.questNo = PlayerPrefs.GetInt("questNo");
        UITextControl.questPart = PlayerPrefs.GetInt("questPart");

        if (PlayerPrefs.GetInt("isMale") == 1)
        {
            GM.playerControl.playerAnimator.SetBool("IsMale", true);
        }
        else if (PlayerPrefs.GetInt("isMale") == 0)
        {
            GM.playerControl.playerAnimator.SetBool("IsMale", false);
        }

        try
        {
            GM.playerControl.playerAnimator.SetInteger("Class", PlayerPrefs.GetInt("Class"));
        }
        catch (Exception e)
        {
            Debug.Log("Class failed to load: " + e);
        }
    }
}
