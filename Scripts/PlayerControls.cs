using System.Collections;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public float jumpHieght;


    public Transform feetPos;

    public float checkRadius;
    bool isGrounded = true;

    float velocity;
    public float gravityScale;
    public LayerMask whatIsGround;
    Rigidbody2D rb;
    private bool isJumping;

    private float jumpTimeCounter = 0;
    public float jumpTime = 0;

    public int playerHealth = 3;




    private bool IsFlashing;
    public float FlashTimer;
    private SpriteRenderer ren;
    // bool textAnim;

    public GameObject floatingText;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ren = GetComponent<SpriteRenderer>();
        originalColor = ren.color;
        playerHealth = 3;
    }

    private void Update()
    {
        TouchCon();



        playerConstraints();

        //velocity += Physics2D.gravity.y * gravityScale * Time.deltaTime;

        if (GameManager.instance.PlayerHealth <= 0) { return; }


        if (Input.GetMouseButtonDown(0) && isGrounded)
        {
            jumpTimeCounter = jumpTime;
            rb.linearVelocity = Vector2.up * jumpHieght;
            isJumping = true;
        }

        if (Input.GetKey(KeyCode.Mouse0) && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                rb.linearVelocity = Vector2.up * jumpHieght ;
                jumpTimeCounter -= Time.deltaTime;
            }
            else { isJumping = false; }
        }

        if (Input.GetKeyUp(KeyCode.Mouse0)) { isJumping = false; }


        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

    }

    private void TouchCon()
    {
       
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {


            }
            if (touch.phase == TouchPhase.Ended)
            {

            }
        }
    }

    private void playerConstraints()
    {
        if (transform.position.y < -3.5f)
        {
            transform.position = new Vector2(transform.position.x, -3.5f);

        }
        else if (transform.position.y >= 3.2f)
        {
            transform.position = new Vector2(transform.position.x, 3.5f);
            isJumping = false;
        }
    }

  

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Trash"))
        {
            DamageEffect();
            GameManager.instance.TakeDamage(1);
            ScoreManager.instance.EndGame();
        }

        if (collision.CompareTag("Nut"))
        {
            ScoreManager.instance.AddScore(10);
            StartCoroutine(FloatingText());


        }
    }


    IEnumerator FloatingText()
    {
        float elpased = 0;

        var text = Instantiate(floatingText, transform.position, Quaternion.identity, transform);
        Vector3 startScale = text.transform.localScale;
        text.transform.localPosition = new Vector3(1.8f, .5f, 0f);

        while (elpased < duration)
        {
            text.transform.localScale = Vector3.Lerp(startScale, Vector3.zero, elpased / duration);

            elpased += Time.deltaTime;

            yield return null;
        }

        text.transform.localScale = Vector3.zero;
        //textAnim = false;
        Destroy(text.gameObject);


    }

    private void DamageEffect()
    {
        IsFlashing = true;
        if (IsFlashing)
        {
            StartCoroutine(DamageFlash());
        }
        else if (!IsFlashing) { StopCoroutine(DamageFlash()); }

    }

    Color hitColor = new Color(255, 4, 4, 255);
    Color originalColor;
    private float Interval = .1f;
    private float duration =.2f;

    IEnumerator DamageFlash()
    {

        IsFlashing = false;

        for (int i = 0; i < 2; i++)
        {
            ren.color = hitColor;
            yield return new WaitForSeconds(FlashTimer);
            ren.color = originalColor;
            yield return new WaitForSeconds(Interval);
        }

        //yield return new WaitForSeconds(.1f);
        ren.color = originalColor;

        IsFlashing = false;

    }




}
