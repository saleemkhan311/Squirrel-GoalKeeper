using System;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class GameManager : MonoBehaviour
{
    public int PlayerHealth = 3;
    public Image[] hearts;
    public Sprite heartFull;
    public Sprite heartEmpty;

    public GameObject gameOver;
    public GameObject spawner;

    // menu
    public GameObject mainMenu;
    public GameObject player;
    public GameObject tree;
    public GameObject scoreManager;
    public GameObject heartContainer;
    public GameObject gameManager;
    public GameObject shop  ;

    public Button muteBtn;
    public Sprite mute;
    public Sprite unMute;
    private bool isMute;
    public AudioSource audioSource; // Reference to AudioSource
    public AudioClip ButtonSound;
    public AudioClip DamageSound;


    public static GameManager instance;
    
    private void OnEnable()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            
            return;
        }
        UpdateHealthUI(3);
    }

    public void UpdateHealthUI(int health)
    {
        PlayerHealth = health;
        foreach (Image img in hearts)
        {
            img.sprite = heartEmpty;
        }

        for (int i = 0; i < PlayerHealth; i++)
        {
            hearts[i].sprite = heartFull;
        }
    }

    public void MuteButton()
    {
        isMute = !isMute;

        AudioListener.volume = isMute ? 0f : 1f;
        muteBtn.image.sprite = isMute ? mute : unMute;
    }

    public void GameOver()
    {
        if(PlayerHealth == 0)
        {
            spawner.SetActive(false);
            StopAllCoroutines();
            gameOver.SetActive(true);
            ScoreManager.instance.UpdateHighScore();

        }
    }

    public void Shop()
    {
        audioSource.PlayOneShot(ButtonSound);
        shop.SetActive(true);
        ScoreManager.instance.SetBankUI();
       
    }

    public void TakeDamage(int damage)
    {
        PlayerHealth -= damage;
        UpdateHealthUI(PlayerHealth);

        audioSource.PlayOneShot(DamageSound);
        
        if (PlayerHealth <= 0)
        {
            PlayerHealth = 0;  // Ensure it doesn't go negative
            GameOver();
        }
    }

    public void RestartGame()
    {
        audioSource.PlayOneShot(ButtonSound);
        DestroyGarbage();

        spawner.SetActive(true);
        gameOver.SetActive(false);

        PlayerHealth = 3;
        UpdateHealthUI(PlayerHealth);

        ScoreManager.instance.score = 0;
        ScoreManager.instance.level = 1;

        ScoreManager.instance.UpdateScoreUI();
    }

    private void DestroyGarbage()
    {
        Garbage[] garbage = FindObjectsByType<Garbage>(FindObjectsSortMode.None);
        for (int i = 0; i < garbage.Length; i++)
        {
            Destroy(garbage[i].gameObject);
        }
    }

    public void StopGame()
    {
        audioSource.PlayOneShot(ButtonSound);
        DestroyGarbage();

        player.SetActive(false);
        tree.SetActive(false);
        scoreManager.SetActive(false);
        heartContainer.SetActive(false);
        gameManager.SetActive(false);
        spawner.SetActive(false);
        gameOver.SetActive(false);
        mainMenu.SetActive(true);


    }



}
