using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseClassControl : MonoBehaviour
{
    GM GM;

    public GameObject chooseClassCanvas;

    void Awake()
    {
        GM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GM>();
    }

    void Start()
    {
        chooseClassCanvas.SetActive(false);
    }

    public void ChooseRanger()
    {
        GM.playerControl.playerAnimator.SetInteger("Class", 1);         // 1 = ranger, 2 = courier, 3 = warrior
        PlayerPrefs.SetInt("Class", 1);
        PlayerPrefs.Save();

        chooseClassCanvas.SetActive(false);
    }

    public void ChooseCourier()
    {
        GM.playerControl.playerAnimator.SetInteger("Class", 2);
        PlayerPrefs.SetInt("Class", 2);
        PlayerPrefs.Save();

        chooseClassCanvas.SetActive(false);
    }

    public void ChooseWarrior()
    {
        GM.playerControl.playerAnimator.SetInteger("Class", 3);
        PlayerPrefs.SetInt("Class", 3);
        PlayerPrefs.Save();

        chooseClassCanvas.SetActive(false);
    }
}
