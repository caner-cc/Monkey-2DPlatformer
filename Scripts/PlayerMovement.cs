using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;    
    public TextMeshProUGUI scoreField;
    public GameObject finalMessage;
    public GameObject bananaParticle;
    public GameObject jumpParticle;
    public AudioClip bananaSoundfx;
    public Bullet bulletPrefab;
    float dirX;
    float dirY;
    float JumpVelocity = 9;
    int score = 0;
    bool onWall = false;
    bool onWallVertical = false;
    bool isJumping = false;
    Vector2 currectDirection;

    // Start is called before the first frame update
    private void Start()
    {
        score = 0;
        scoreField.text = score.ToString();
        rb = GetComponent<Rigidbody2D>();
        finalMessage.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {

        if (onWall == true)
        {
            dirY = Input.GetAxisRaw("Vertical");
            rb.velocity = new Vector2(rb.velocity.x, dirY * 4f);
            dirX = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(dirX * 3f, rb.velocity.y);
        }
        else if (onWallVertical == true)
        {
            rb.gravityScale = 0;
            dirY = Input.GetAxisRaw("Vertical");
            rb.velocity = new Vector2(rb.velocity.x, dirY * 4f);
            dirX = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(dirX * 3f, rb.velocity.y);

        }
        else
        {
            onWall = false;
            onWallVertical = false;

            dirX = Input.GetAxisRaw("Horizontal");              //-> Pressing the "a" or "d" key gives a value of -1 or 1
            rb.velocity = new Vector2(dirX * 4f, rb.velocity.y);
        }
        //FLIP CHARACTER CODE HERE 
        Vector3 characterScale = transform.localScale;
        if (dirX < 0)
        {
            characterScale.x = -1.3f;            
        }
        if (dirX > 0)
        {
            characterScale.x = 1.3f;
        }
        transform.localScale = characterScale;
        //


        if (Input.GetButtonDown("Jump") && !isJumping && !onWall && !onWallVertical)
        {
            Debug.Log("jump");
            rb.velocity = new Vector2(rb.velocity.x, JumpVelocity);
            SoundManagerScript.PlaySound("jump");
            Instantiate(jumpParticle, rb.transform.position, Quaternion.identity);
            isJumping = true;

        }
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("you tried to fire a bullet");
            Shoot();
        }

    }
    private void Shoot()
    {
        Bullet bullet = Instantiate(bulletPrefab, this.transform.position, this.transform.rotation);
        currectDirection = new Vector2(rb.velocity.x, 0);
        
        //Code for direction of bullet
        if (rb.velocity.x < 0)
        {
            currectDirection = new Vector2(-1, 0);
            bullet.Project(currectDirection);
        }
        else if (rb.velocity.x > 0)
        {
            currectDirection = new Vector2(1, 0);
            bullet.Project(currectDirection);
        }
        else
        {
            bullet.Project(transform.right);
        }

    }
    private void FixedUpdate()
    {

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "ClimbWall")
        {
            onWall = false;
        }
        if (other.gameObject.tag == "ClimbWallVertical")
        {
            rb.gravityScale = 2;
            onWallVertical = false;
        }



    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "PickUp")
        {
            score = score + 1;
            scoreField.text = "Bananas: " + score.ToString();
            Instantiate(bananaParticle, other.transform.position, Quaternion.identity);
            SoundManagerScript.PlaySound("pickup");
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "ClimbWallVertical")
        {
            onWallVertical = true;
            Debug.Log("You are on the the Horizontal Wall");
            isJumping = false;
            //when it interacts with the wallclimb tag it should set the movement options to only vertical
            //after you exit the tag it should return to horizontal
        }

        if (other.gameObject.tag == "ClimbWall")
        {
            onWall = true;
            Debug.Log("You are on the the Wall");
            isJumping = false;


            //when it interacts with the wallclimb tag it should set the movement options to only vertical
            //after you exit the tag it should return to horizontal
        }
        if (other.gameObject.tag == "Finish")
        {
            SceneManager.LoadScene("StartScreen");
        }
        /*if (score == 30)
        {
            finalMessage.SetActive(true);
            //Simple way to pause the game
            Time.timeScale = 0;
        }*/
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Debug.Log("You are on the ground");
            isJumping = false;
            //when it interacts with the wallclimb tag it should set the movement options to only vertical
            //after you exit the tag it should return to horizontal
        }

    } 

    
}
