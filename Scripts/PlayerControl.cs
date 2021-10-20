using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class PlayerControl : MonoBehaviour
{
    public Rigidbody2D rigidbody2d;

    //Movement
    float horizontal;
    float vertical;
    public float speed = 4.0f;
    public Vector2 position;

    //Animation
    public Animator playerAnimator;

    GM GM;
    
    public string scene = "Redcliff";

    bool isDipRunning = false;

    void Awake()
    {
        GM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GM>();
        //Debug.Log("found GM = " + GM);
    }

    void Start()
    {
        Screen.SetResolution(1920, 1080, true);

        rigidbody2d = GetComponent<Rigidbody2D>();

        //Animation
        playerAnimator = GetComponent<Animator>();
        playerAnimator.SetFloat("Speed", 0);

        //Interaction
        playerAnimator.SetBool("Talking", false);

        GM.playerPreferences.LoadGamePreferences();

    }

    void Update()
    {
        //Movement
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (Input.GetMouseButtonDown(0) && GM.menuControl.paused == false)
        {
            CheckClick();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!GM.menuControl.paused)
            {
                GM.menuControl.menuCanvas.SetActive(true);
            }
            else if (GM.menuControl.paused)
            {
                GM.menuControl.menuCanvas.SetActive(false);
            }
            GM.menuControl.paused = !GM.menuControl.paused;
        }
    }

    void FixedUpdate()
    {

        if (MenuControl.loading == false && PlayerPrefs.GetFloat("nextScenePosX") == 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                speed = 8.0f;
            }
            else
            {
                speed = 4.0f;
            }
            
            //Movement
            position = rigidbody2d.position;
            position.x = position.x + (horizontal * speed * Time.deltaTime);
            position.y = position.y + (vertical * speed * Time.deltaTime);
            rigidbody2d.MovePosition(position);

            //Movement animation
            playerAnimator.SetFloat("SpeedX", horizontal);
            playerAnimator.SetFloat("SpeedY", vertical);

            if (!Mathf.Approximately(horizontal, 0.0f) || !Mathf.Approximately(vertical, 0.0f))
            {
                playerAnimator.SetFloat("Speed", 1);
            }
            else
            {
                playerAnimator.SetFloat("Speed", 0);

                if (!isDipRunning)
                {
                    StartCoroutine(RandomPlayerDip());
                }
            }
        }
        else if (PlayerPrefs.GetFloat("nextScenePosX") != 0)
        {
            position.x = PlayerPrefs.GetFloat("nextScenePosX");
            position.y = PlayerPrefs.GetFloat("nextScenePosY");
            //Debug.Log("Retrieved pos = " + PlayerPrefs.GetFloat("nextScenePosX") + " " + PlayerPrefs.GetFloat("nextScenePosY"));

            rigidbody2d.MovePosition(position);

            PlayerPrefs.SetFloat("nextScenePosX", 0f);
            PlayerPrefs.Save();
        }
        else if (MenuControl.loading == true)
        {
            MenuControl.loading = false;
            rigidbody2d.MovePosition(position);

        }



        
    }

    

    void CheckClick()
    {
        Collider2D collider = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        if (collider != null)

        {
            Debug.Log("Collider = " + collider);

            Debug.Log("Text canvas (player control) =" + GM.textCanvas);
            Debug.Log("text canvas active: " + GM.textCanvas.activeSelf);

            if (!GM.textCanvas.activeSelf)                                      //********//
            {
                if (collider.name == "CastleGateExt")
                {
                    ChangeScene("RedcliffCourtyard", 1f, -8.5f);
                }
                else if (collider.name == "CastleGateInt")
                {
                    ChangeScene("Redcliff", 1f, 16.5f);
                }

                else if (collider.name == "KeepDoorExt")
                {
                    ChangeScene("RedcliffKeep", 1f, -11f);
                }
                else if (collider.name == "KeepDoorInt")
                {
                    ChangeScene("RedcliffCourtyard", 1f, -6f);
                }

                else if (collider.name == "ClearingArrowExt")
                {
                    ChangeScene("RangersClearing", 19.5f, -7f);
                }
                else if (collider.name == "ClearingArrowInt")
                {
                    ChangeScene("Redcliff", -17f, -10f);
                }

                else if (collider.name == "RangerHutDoorExt")
                {
                    ChangeScene("RangerHutInterior", 7f, -13.5f);
                }
                else if (collider.name == "RangerHutDoorInt")
                {
                    ChangeScene("RangersClearing", 7f, -2f);
                }

                else if (collider.name == "Battlemaster")
                {
                    GM.textControl.DisplayText("Battlemaster");
                }
                else if (collider.name == "Baron")
                {
                    GM.textControl.DisplayText("Baron");
                }
            }
        }
        
    }  

    void ChangeScene(string scene, float posX, float posY)
    {
        PlayerPrefs.SetFloat("nextScenePosX", posX);
        PlayerPrefs.SetFloat("nextScenePosY", posY);
        PlayerPrefs.Save();

        GM.playerPreferences.SaveScenePreferences();
        //Debug.Log("Saved pos = " + PlayerPrefs.GetFloat("nextScenePosX") + " " + PlayerPrefs.GetFloat("nextScenePosY"));

        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

    IEnumerator RandomPlayerDip()
    {
        isDipRunning = true;
        //Debug.Log("Running playerDip");

        int waitTime = UnityEngine.Random.Range(1, 4);

        yield return new WaitForSeconds(waitTime);
        playerAnimator.SetBool("IdleDip", true);

        yield return new WaitForSeconds(0.5f);
        playerAnimator.SetBool("IdleDip", false);

        yield return new WaitForSeconds(1);

        isDipRunning = false;

    }
}
