using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    private static LevelManager _instance;
    private static PlatformManager _platformManagerInstance;

    public static LevelManager Instance { get { return _instance; } }
    public static PlatformManager PlatformManager { get { return _platformManagerInstance; } }
    [SerializeField] Text scoreText;
    [SerializeField] Text highScoreText;

    public GameObject player;
    [SerializeField]
    private Image GameOverImage;
    [SerializeField]
    private Image RetryImage;
    [SerializeField]
    private Image MenuImage;

    [SerializeField]
    private Image LevelCompleteImage;

    private bool LoseConditionTriggered = false;
    private bool WinConditionTriggered = false;
    [SerializeField]
    private int playerScore = 0;
    private int playerhighScore = 0;
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    void Start()
    {
        GameOverImage.enabled = false;
        RetryImage.enabled = false;
        MenuImage.enabled = false;
        playerhighScore = PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_highscore");
        highScoreText.text = playerhighScore.ToString();
        scoreText.text = playerScore.ToString();
    }
    public void IncrementPlayerScore()
    {
        playerScore++;
        scoreText.text = playerScore.ToString();
        if (playerhighScore < playerScore)
        {
            highScoreText.text = playerScore.ToString();
        }
    }
    public int GetPlayerScore()
    {
        return playerScore;
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void TriggerLoseCodition()
    {
        if (!LoseConditionTriggered && !WinConditionTriggered)
        {
            Debug.Log("Lose Condition Triggered");
            LoseConditionTriggered = true;
            GameOverImage.enabled = true;
            RetryImage.enabled = true;
            MenuImage.enabled = true;
            SetHighScore();
        }
    }
    public void TriggerWinCondition()
    {
        if(!WinConditionTriggered && !LoseConditionTriggered)
        {
            Debug.Log("Win Condition Triggered");
            WinConditionTriggered = true;
        }
    }
    public void Retry()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
    public void Menu()
    {

        SceneManager.LoadScene("MainMenu");
    }
    private void SetHighScore()
    {
        int highscore = PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_highscore");
        if(playerScore> highscore )
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_highscore", playerScore);
        Debug.Log(PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_highscore"));
    }
}
