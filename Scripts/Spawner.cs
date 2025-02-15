using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject Garbage;
    public GameObject Nut;

    private new SpriteRenderer renderer;

    [SerializeField] private float GarbageSpawnRate;
    [SerializeField] private float NutSpawnRate;

    public Sprite[] sprites;

    private float randomPos;

    private Coroutine spawnCoroutine;

    void OnEnable()
    {
        // Start the coroutine and store its reference
        spawnCoroutine = StartCoroutine(SpawnObjects());
    }

    void OnDisable()
    {
        // Stop the coroutine if it's running
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }


    IEnumerator SpawnObjects()
    {
        while (true)
        {
            if (ScoreManager.instance == null)
            {
                yield return null; 
                continue;
            }

            if (GameManager.instance == null)
            {
                yield return null; // Wait until the next frame
                continue; // Try again in the next loop
            }

            if (GameManager.instance.PlayerHealth <= 0) {yield return null; }

            randomPos = Random.Range(-1.0f, 3.0f);

            float waitTime = Mathf.Max(0.3f, 1.0f / (1 + ScoreManager.instance.level * 0.1f));
            yield return new WaitForSeconds(waitTime);

            if (!gameObject.activeInHierarchy)  // Exit if object is destroyed
                yield break;

           
            float spawnChance = Random.value; // Generates a number between 0 and 1

            if (spawnChance < 0.70f)  // 60% chance to spawn garbage
            {
                spawnGarabge();
            }
            else  // 30% chance to spawn a nut
            {
                SpawnNuts();
            }
        }
    }


    IEnumerator Coroutine()
    {
        
        while (true)
        {
            randomPos = Random.Range(-1.0f, 3.0f);
            float waitTime = Mathf.Max(0.3f, 1.2f - (ScoreManager.instance.level * GarbageSpawnRate ));
            yield return new WaitForSeconds( waitTime);
            spawnGarabge();
            
        }
    }

    private void spawnGarabge()
    {
        randomPos = Random.Range(-1.0f, 3.0f);
        GameObject gameObject = Instantiate(Garbage,new Vector3(transform.position.x,randomPos , transform.position.z),Quaternion.identity);

        renderer= gameObject.GetComponent<SpriteRenderer>();

        renderer.sprite = sprites[Random.Range(0, 9)];

       
    }

    private void SpawnNuts()
    {
            randomPos = Random.Range(-0.9f, 2.9f);
            Instantiate(Nut, new Vector3(transform.position.x, randomPos, transform.position.z), Quaternion.identity);
    }
}
