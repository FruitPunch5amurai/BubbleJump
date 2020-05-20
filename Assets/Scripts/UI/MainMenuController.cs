using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    private int levelIndex = 0;
    [SerializeField]
    private Image TapToPlayText;
    [SerializeField]
    private Transform[] levelsObjs = new Transform[1];
    [SerializeField]
    private GameObject mainMenu;

    public void Start()
    {

    }

    public void Update()
    {
        if (Input.touchCount == 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began && TapToPlayText.enabled)
            {
                TapToPlayText.enabled = false;
                EnableMainMenu();
            }
        }
    }

    public void EnableMainMenu()
    {
        Debug.Log("EnableMainMenu()");
        mainMenu.SetActive(true);
    }

    public void LoadLevel()
    {
        Debug.Log("LoadLevel()");
        SceneManager.LoadScene(levelsObjs[levelIndex].name);
    }
    public void NextLevel()
    {
        Debug.Log("NextLevel()");
    }
    public void PreviousLevel()
    {
        Debug.Log("PreviousLevel()");
    }
}
