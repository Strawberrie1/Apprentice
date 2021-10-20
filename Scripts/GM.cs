using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour
{
    public PlayerControl playerControl;
    public MenuControl menuControl;
    public UITextControl textControl;
    public GameSaveControl gameSaveControl;
    public PlayerPreferences playerPreferences;
    public CreateCharacterControl createCharacterControl;
    public QuestLogControl questLogControl;
    public ChooseClassControl chooseClassControl;

    public GameObject textCanvas;
    
    
    // Start is called before the first frame update
    void Awake()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
            Debug.Log("GM playerControl = " + playerControl);
        }
        if (GameObject.FindGameObjectWithTag("GameManager") != null)
        {
            gameSaveControl = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameSaveControl>();
            playerPreferences = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerPreferences>();
        }
        if (GameObject.FindGameObjectWithTag("Menu") != null)
        {
            menuControl = GameObject.FindGameObjectWithTag("Menu").GetComponent<MenuControl>();
        }
        if (GameObject.FindGameObjectWithTag("TextCanvas") != null)
        {
            textControl = GameObject.FindGameObjectWithTag("TextCanvas").GetComponent<UITextControl>();
            textCanvas = GameObject.FindGameObjectWithTag("TextCanvas");
            //textCanvas.SetActive(false);
        }
        if (GameObject.FindGameObjectWithTag("CreateCharacterMenu") != null)
        {
            createCharacterControl = GameObject.FindGameObjectWithTag("CreateCharacterMenu").GetComponent<CreateCharacterControl>();
        }
        if (GameObject.FindGameObjectWithTag("QuestLogCanvas") != null)
        {
            questLogControl = GameObject.FindGameObjectWithTag("QuestLogCanvas").GetComponent<QuestLogControl>();
        }
        if (GameObject.FindGameObjectWithTag("ChooseClassCanvas") != null)
        {
            chooseClassControl = GameObject.FindGameObjectWithTag("ChooseClassCanvas").GetComponent<ChooseClassControl>();
        }
    }

}
