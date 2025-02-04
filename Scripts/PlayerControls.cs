using UnityEngine;

public class PlayerControls : MonoBehaviour
{
     public float jumpHeight ;
    bool isHolding;

    private void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
            {
                isHolding = true;   
                
            }if(touch.phase == TouchPhase.Ended)
            {
                isHolding=false;
            }


        }

        if (Input.GetMouseButtonDown(0)) { isHolding = true; }
        if (Input.GetMouseButtonUp(0)) {isHolding = false; }

        float lerpSpeed = 5f;  
        jumpHeight = Mathf.Lerp(jumpHeight, isHolding ? 3.5f : -3.5f, Time.deltaTime * lerpSpeed);

        transform.position = new Vector2(transform.position.x,jumpHeight);



        Debug.Log(jumpHeight);

    }

    private void playerConstraints()
    {
        if(transform.position.y < -3.5f)
        {
            transform.position = new Vector2(transform.position.x, -3.5f);
        }else if (transform.position.y > 3.5f)
        {
            transform.position = new Vector2(transform.position.x, 3.5f);
        }
    }

    private void JumpConstraints(float a)
    {
        if (a < -3.5f)
        {
            a =  -3.5f;
        }
        else if (transform.position.y > 3.5f)
        {
            transform.position = new Vector2(transform.position.x, 3.5f);
        }
    }
}
