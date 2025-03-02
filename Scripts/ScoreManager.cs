using System;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [NonSerialized]public int score = 0;
    [NonSerialized]public int highScore = 0;
    [NonSerialized]public int piggyBank = 0;
    public Text PiggyText;
    private int currency;

    private int recordHighScore = 0;
    private int recordLowScore = 0;
    private int recordMidScore = 0;
    public int level = 1;
    public static ScoreManager instance;
    public Text scoreText;
    public Text NewScoreText;
    public Text HighScoreText;
    public Text RecordHighScoreText;
    public Text RecordLowScoreText;
    public Text RecordMidScoreText;

    public AudioSource audioSource; // Reference to AudioSource
    public AudioClip ScoreSound; // Assign in Inspector

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        score = 0;
        level = 1;
        UpdateScoreUI();
        highScore = PlayerPrefs.GetInt("highScore", 0);
        //PlayerPrefs.SetInt("highScore",recordHighScore);
        SetBankUI();
    }

    public void SetBankUI()
    {
        piggyBank = PlayerPrefs.GetInt("piggyBank", 0);
        PiggyText.text = piggyBank.ToString();
    }

    public bool Purchase(int amount)
    {
        if (piggyBank >= amount)
        {
            Debug.Log($"Before: {piggyBank}");
            piggyBank -= amount;

            Debug.Log($"After: {piggyBank}");


            PlayerPrefs.SetInt("piggyBank", piggyBank);
            PlayerPrefs.Save();

            SetBankUI();
            return true;
        }
        else
        {
            Debug.Log("You Dont have Enough Money");
            return false;
        }
    }

    private void OnEnable()
    {
        score = 0;
        level = 1;
        UpdateScoreUI();
    }

    public void AddScore(int points)
    {
        score += points;
        currency += points;

        
        if (score > PlayerPrefs.GetInt("highScore", 0))
        {
            PlayerPrefs.SetInt("highScore", score);
            highScore = PlayerPrefs.GetInt("highScore", 0);
            PlayerPrefs.Save();
        }
        if (score >= level * 50)
        {
            level++;
        }
        UpdateScoreUI();

        audioSource.PlayOneShot(ScoreSound);
       

    }

    public void EndGame()
    {
        piggyBank = PlayerPrefs.GetInt("PiggyBank", 0);
        piggyBank += currency;
        PlayerPrefs.SetInt("piggyBank", piggyBank);
        PlayerPrefs.Save();
    }

   


    public void UpdateScoreUI()
    {
        scoreText.text = $"Score: {score}";
    }
    


    public void UpdateHighScore()
    {
        NewScoreText.text = $"Your Score is {score}";
        HighScoreText.text = $"Best Score {highScore}";
    }

    private void SetRecords()
    {
        recordHighScore = PlayerPrefs.GetInt("recordHighScore",0);
        recordMidScore = PlayerPrefs.GetInt("recordMidScore",0);
        recordLowScore = PlayerPrefs.GetInt("recordLowScore" , 0) ; 

        if(score > recordHighScore )
        {
            recordLowScore = recordMidScore;
            recordMidScore = recordHighScore;
            recordHighScore = score;

        } else if(score > recordMidScore )
        {
            recordLowScore = recordMidScore;
            recordMidScore = score;
        }
        else if(score > recordLowScore)
        {
            recordLowScore = score; 
        }

        PlayerPrefs.SetInt("recordHighScore", recordHighScore);
        PlayerPrefs.SetInt("recordMidScore", recordMidScore);
        PlayerPrefs.SetInt("recordLowScore", recordLowScore);
        PlayerPrefs.Save();

    }

    public void UpdateRecords()
    {
        SetRecords();

        RecordHighScoreText.text = recordHighScore.ToString();  
        RecordMidScoreText.text = recordMidScore.ToString();
        RecordLowScoreText.text = recordLowScore.ToString();
    }

    
}
