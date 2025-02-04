using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject Garbage;

    private new SpriteRenderer renderer;

    public float spawnSpeed;

    public Sprite[] sprites;

    private void Start()
    {
        StartCoroutine(Coroutine());
    }
    IEnumerator Coroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnSpeed);
            spawnGarabge();
        }
    }

    private void spawnGarabge()
    {
        GameObject gameObject = Instantiate(Garbage,new Vector3(transform.position.x, Random.Range(-1.0f, 3.0f), transform.position.z),Quaternion.identity);

        renderer= gameObject.GetComponent<SpriteRenderer>();

        renderer.sprite = sprites[Random.Range(0, 9)];

        if(renderer.sprite.name =="nut")
        {
            Debug.Log(renderer.sprite.name);
        }

    }
}
