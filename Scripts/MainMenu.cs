using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject player;
    public GameObject spawner;
    public GameObject tree;
    public GameObject scoreManager;
    public GameObject heartContainer;
    public GameObject gameManager;
    public GameObject records;


    public RectTransform imageTransform; // Assign your UI Image (RectTransform)
    public GameObject ripple;
    private Image imageComponent; // Assign the Image component for fading effect
    public Vector2 targetSize = new Vector2(300f, 300f); // Target size
    public float duration = 1f; // Time to reach target size
    public float fadeDuration = 1f; // Time to fade out
    public float waitTime = 2f; // Wait time before restarting

    private Vector2 originalSize;
    private Color originalColor;

    private Coroutine coroutine;

    void Start()
    {
        imageComponent =ripple.GetComponent<Image>();
        originalSize = imageTransform.sizeDelta; // Store original size
        originalColor = imageComponent.color; // Store original color
        
    }

    private void OnEnable()
    {
        coroutine = StartCoroutine(ScaleAndFadeLoop());
    }
    private void OnDisable()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
    }

    int health;
    public void StartGame()
    {
        
        player.SetActive(true);
        tree.SetActive(true);
        scoreManager.SetActive(true);   
        heartContainer.SetActive(true);
        gameManager.SetActive(true);

        if (GameManager.instance == null)
        {
            Debug.LogError("GameManager instance is NULL!");
            return;
        }
        health = GameManager.instance.PlayerHealth;



        GameManager.instance.RestartGame();
        
        mainMenu.SetActive(false);
    }

    public void ShowRecords()
    {
        records.SetActive(true);
        ScoreManager.instance.UpdateRecords();
    }

    public void HideRecords()
    {
        records.SetActive(false );
    }

    IEnumerator ScaleAndFadeLoop()
    {
        while (true) // Repeat indefinitely
        {
            // Scale up
            yield return StartCoroutine(ScaleImage(originalSize, targetSize, duration));

            // Fade out
            yield return StartCoroutine(FadeImage(1f, 0f, fadeDuration));

            // Reset size and fade in immediately
            imageTransform.sizeDelta = originalSize;
            imageComponent.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);

            // Wait before restarting
            yield return new WaitForSeconds(waitTime);
        }
    }

    IEnumerator ScaleImage(Vector2 startSize, Vector2 endSize, float time)
    {
        float elapsed = 0f;

        while (elapsed < time)
        {
            elapsed += Time.deltaTime;
            imageTransform.sizeDelta = Vector2.Lerp(startSize, endSize, elapsed / time);
            yield return null;
        }

        imageTransform.sizeDelta = endSize; // Ensure final size is exac
    }

    IEnumerator FadeImage(float startAlpha, float endAlpha, float time)
    {
        float elapsed = 0f;
        Color color = imageComponent.color;

        while (elapsed < time)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, endAlpha, elapsed / time);
            imageComponent.color = color;
            yield return null;
        }

        imageComponent.color = new Color(color.r, color.g, color.b, endAlpha); // Ensure final transparency
    }
}
