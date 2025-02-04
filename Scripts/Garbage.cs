using UnityEngine;

public class Garbage : MonoBehaviour
{
    private float speed = 5;

    void Update()
    {
        float constraint = Random.Range(-3.8f, -4.13f);

        if (transform.position.y > constraint) 
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }

        
    }
}
