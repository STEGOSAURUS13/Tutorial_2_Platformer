using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;

    public Text scoreText;

    public Text winText;

    public Text livesText;

    private int lives;

    private int scoreValue = 0;


    public AudioClip musicClipOne;

    public AudioClip musicClipTwo;

    public AudioSource musicSource;

    Animator anim;


    private bool facingRight = true;


    private bool isOnGround;
    public Transform groundcheck;
    public float checkRadius;
    public LayerMask allGround;



    // Start is called before the first frame update
    void Start()
    {
        
        anim = GetComponent<Animator>();

        rd2d = GetComponent<Rigidbody2D>();
        SetScoreValue();
        lives = 3;
        SetLivesText();
        winText.text = "";

        {
            musicSource.clip = musicClipOne;
            musicSource.Play();
            musicSource.loop = true;

        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));


        isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);


        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }


        if (hozMovement != 0)
        {
            anim.SetInteger("State", 1);
        }

        if (hozMovement == 0)
        {
            anim.SetInteger("State", 0);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            SetScoreValue();
            Destroy(collision.collider.gameObject);

            if (scoreValue == 4)
            {
                lives = 3;
                SetLivesText();
                transform.position = new Vector3(84.0f, -1.8f, 0.0f);
   
            }

        }

        else if (collision.collider.tag == "Enemy")
        {
            lives -= 1;
            SetLivesText();
            Destroy(collision.collider.gameObject);
        }




    }



    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground" && isOnGround)
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }




   

    void SetScoreValue()
    {
        scoreText.text = "Score: " + scoreValue.ToString();
        if (scoreValue >= 8)
        {
            winText.text = "You Win! Game created by: Kirsten Futch";
            musicSource.clip = musicClipTwo;
            musicSource.Play();
            musicSource.loop = false;
        }

    }

    void SetLivesText()
    {
        livesText.text = "Lives: " + lives.ToString();
        if (lives <= 0)
        {
            winText.text = "You Lose! Game created by: Kirsten Futch";
            Destroy(gameObject);
        }
    }


    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }




    void Update()

    {
        if (isOnGround == true)

        {
            if (Input.GetKeyDown(KeyCode.D))

            {

                anim.SetInteger("State", 1);

            }

            if (Input.GetKeyUp(KeyCode.D))

            {

                anim.SetInteger("State", 0);

            }

            if (Input.GetKeyDown(KeyCode.A))

            {

                anim.SetInteger("State", 1);

            }

            if (Input.GetKeyUp(KeyCode.A))

            {

                anim.SetInteger("State", 0);

            }



            if (Input.GetKeyDown(KeyCode.W))

            {

                anim.SetInteger("State", 2);

            }


            if (isOnGround == false)
            {
                anim.SetInteger("State", 2);
            }

        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
}