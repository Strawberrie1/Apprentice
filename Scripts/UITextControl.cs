using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITextControl : MonoBehaviour
{
    GM GM;

    public static int questNo = 0;
    public static int questPart = 0;

    public Text dialog;

    public GameObject nextButton;

    public GameObject battlemasterImage;
    public GameObject baronImage;

    public Dictionary<string, string> questDict = new Dictionary<string, string>();

    bool goToChooseCraft;

    void Awake()
    {

        GM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GM>();

    }

    // Start is called before the first frame update
    void Start()
    {
        GM.playerPreferences.LoadGamePreferences();
        Debug.Log("Loaded quest = " + questNo + questPart);

        GM.textCanvas.SetActive(false);

        FillQuestList();

        goToChooseCraft = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DisplayText(string character)
    {
        //set all images to inactive so we only activate one

        battlemasterImage.SetActive(false);
        baronImage.SetActive(false);






        if (character == "Battlemaster")
        {
            battlemasterImage.SetActive(true);

            if (questNo == 0)
            {
                SetTalking(true);

                questNo = 1;
                questPart = 1;

                if (questDict.ContainsKey(questNo + "P" + questPart))
                {
                    dialog.text = (questDict[questNo + "P" + questPart]);

                    if (!questDict.ContainsKey(questNo + "P" + (questPart + 1)))
                    {
                        nextButton.SetActive(false);
                    }
                    else
                    {
                        nextButton.SetActive(true);
                    }
                }
                else
                {
                    dialog.text = ("Dialog not found");
                }
            }

            else if (questDict.ContainsKey(questNo + "P" + (questPart + 1)))
            {
                SetTalking(true);

                NextText();
            }

            else if (questNo == 1 && !questDict.ContainsKey(questNo + "P" + (questPart + 1)) && GM.textCanvas.activeSelf == false)
            {
                SetTalking(true);
                dialog.text = ("You still need to complete the last quest.");
                nextButton.SetActive(false);
            }
        }

        else if (character == "Baron")
        {
            baronImage.SetActive(true);
            Debug.Log("Quest no/part = " + questNo + "/" + questPart);

            if (questNo == 1 && questPart == 3)
            {
                SetTalking(true);

                questNo = 2;
                questPart = 1;

                if (questDict.ContainsKey(questNo + "P" + questPart))
                {
                    dialog.text = (questDict[questNo + "P" + questPart]);

                    if (!questDict.ContainsKey(questNo + "P" + (questPart + 1)))
                    {
                        nextButton.SetActive(false);
                    }
                    else
                    {
                        nextButton.SetActive(true);
                    }
                }
                else
                {
                    dialog.text = ("Dialog not found");
                }
            }

            else if (questNo == 2 && questPart == 3)
            {
                GM.chooseClassControl.chooseClassCanvas.SetActive(true);
            }

            else if (questDict.ContainsKey(questNo + "P" + (questPart + 1)))
            {
                SetTalking(true);

                if (questNo == 2 && questPart == 2)
                {
                    goToChooseCraft = true;
                }

                NextText();
            }



            else if (questNo == 2 && !questDict.ContainsKey(questNo + "P" + (questPart + 1)) && GM.textCanvas.activeSelf == false)
            {
                SetTalking(true);
                dialog.text = ("You still need to complete the last quest.");
                nextButton.SetActive(false);
            }
        }
    }

    public void NextText()
    {
        questPart += 1;

        if (questNo == 2 && questPart == 4)
        {
            SetTalking(false);
            GM.chooseClassControl.chooseClassCanvas.SetActive(true);
        }


        else if (questDict.ContainsKey(questNo + "P" + questPart))
        {
            dialog.text = (questDict[questNo + "P" + questPart]);

            if (questNo == 2 && questPart == 2)
            {
                goToChooseCraft = true;
            }

            if (!questDict.ContainsKey(questNo + "P" + (questPart + 1)))
            {
                if (goToChooseCraft)
                {
                    nextButton.SetActive(true);
                    goToChooseCraft = false;
                }
                else
                {
                    nextButton.SetActive(false);
                }
            }
            else
            {
                nextButton.SetActive(true);
            }

        }


        else 
        {
            dialog.text = ("Dialog not found");
        }
    }

    public void CloseText()
    {
        SetTalking(false);
    }

    void SetTalking(bool t) 
        
        //true if we want to start talking; false if we want to stop talking

    {
        if (t == true)
        {
            GM.playerControl.playerAnimator.SetBool("Talking", true);
            GM.textCanvas.SetActive(true);
        }
        else if (t == false)
        {
            GM.playerControl.playerAnimator.SetBool("Talking", false);
            GM.textCanvas.SetActive(false);
            GM.questLogControl.UpdateQuests();

        }
    }


    void FillQuestList()
    {
        questDict.Add("1P1", "Hello! My name is Rupert, Battlemaster of this castle. \nBaron Aaron is waiting to see you in his office.");
        questDict.Add("1P2", "You will find the Baron's quarters in the castle keep.");
        questDict.Add("1P3", "Good luck with the choosing ceremony!");

        questDict.Add("2P1", "Greetings, young one. I hope you are ready to receive your apprenticeship.");
        questDict.Add("2P2", "You have a choice of three professions: the Ranger, the Warrior, or the Courier.");
        questDict.Add("2P3", "Choose wisely, you cannot change your decision later...");
    }
}
