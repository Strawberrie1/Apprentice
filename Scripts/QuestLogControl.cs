using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestLogControl : MonoBehaviour
{
    public GameObject tutorialBox;
    public Text tutorialText;

    public Dictionary<int, string> tutorialDict = new Dictionary<int, string>();
    public static int tutorialSection;

    public GameObject questBox;
    public Text questText;
    public Dictionary<int, string> questTextDict = new Dictionary<int, string>();

    public bool onMenu;


    // Start is called before the first frame update
    void Start()
    {
        FillTutorialList();
        FillQuestTextList();
        UpdateQuests();
        TutorialNext();
    }

    public void TutorialNext()
    {
        if (tutorialSection == 0)
        {
            tutorialBox.SetActive(false);
            UpdateQuests();
        }
        else if (tutorialSection == 1 && !onMenu)
        {
            Debug.Log("Beginning tutorial, section = " + tutorialSection);

            questBox.SetActive(false);
            tutorialBox.SetActive(true);

            tutorialText.text = tutorialDict[tutorialSection];
            tutorialSection += 1;

        }
        else if (tutorialSection >= 2 && tutorialSection <= 3)
        {
            Debug.Log("Beginning tutorial, section = " + tutorialSection);
            tutorialText.text = tutorialDict[tutorialSection];
            tutorialSection += 1;
        }
        else if (tutorialSection == 4)
        {
            Debug.Log("Beginning tutorial, section = " + tutorialSection);
            tutorialText.text = tutorialDict[tutorialSection];
            tutorialSection += 1;

        }
        else if (tutorialSection == 5)
        {
            Debug.Log("Ending tutorial, section = " + tutorialSection);
            tutorialSection = 0;
            tutorialBox.SetActive(false);
            UITextControl.questNo = 0;

            UpdateQuests();
        }
        else
        {
            tutorialText.text = "Tutorial broken. Please restart game.";
        }

    }

    public void UpdateQuests()
    {
        Debug.Log("Updating quests... Quest no = " + UITextControl.questNo);
        if (UITextControl.questNo == -1)
        {
            questBox.SetActive(false);
        }

        else if (UITextControl.questNo >= 0)
        {
            questBox.SetActive(true);
            questText.text = questTextDict[UITextControl.questNo];
        }
    }

    void FillTutorialList()
    {
        tutorialDict.Add(1, "Welcome, adventurer, to the peaceful fief of Redcliff.");
        tutorialDict.Add(2, "Use WASD to move, shift to sprint, and left click to interact with objects and characters.");
        tutorialDict.Add(3, "You have just turned 15 and are looking for an apprenticeship.");
        tutorialDict.Add(4, "Head north to the castle and speak to the Baron to begin your journey...");
    }

    void FillQuestTextList()
    {
        questTextDict.Add(0, "Visit the castle to the north and find someone to direct you to the Baron.");
        questTextDict.Add(1, "Go into the keep and talk to the Baron.");
        questTextDict.Add(2, "Go to your new home and meet with your craftmaster.");
    }
}
