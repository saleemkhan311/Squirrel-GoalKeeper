using System;
using UnityEngine;
using UnityEngine.UIElements;

public class Garbage : MonoBehaviour
{

   [NonSerialized] public float moveSpeed = 5;
    private float leftEdge;
    private float targetRot;
    
    private float rotateSpeed;
    private float constraint;
    private float SmoothRot;




    private void Awake()
    {
        leftEdge = Camera.main.ScreenToWorldPoint(Vector2.zero).x - 1;

        rotateSpeed = UnityEngine.Random.Range(30f, 90f);
        //moveSpeed = Random.Range(1f, 5f);
        constraint = UnityEngine.Random.Range(-3.8f, -4.13f);



    }

    
    void Update()
    {
        if (GameManager.instance.PlayerHealth == 0) {  return ; }

        targetRot += Time.deltaTime * rotateSpeed  *(1 + ScoreManager.instance.level * 0.1f);

        if (transform.position.y > constraint) 
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime * (1 + ScoreManager.instance.level * 0.1f);
        }

        SmoothRot = Mathf.Lerp(SmoothRot,targetRot,Time.deltaTime * rotateSpeed);

        transform.rotation = Quaternion.Euler(0,0,SmoothRot);
       

        if (transform.position.x < leftEdge)
        {
            Destroy(gameObject);
        }

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject) ;
        }
    }


}
