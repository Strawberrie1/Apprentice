using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameSave
{
    public string scene;
    public float characterPosX;
    public float characterPosY;

    public int questNo;
    public int questPart;

    public int gender; // 0 = female; 1 = male
    public int playerClass; // 0 = ranger, 1 = courier, 2 = warrior

    //public int health;
}
