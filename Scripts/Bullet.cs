using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 300.0f;
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Project(Vector2 direction)
    {
        rb.AddForce(direction * speed);
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Balloon")
        {
            // SAVE THIS CODE TO ADD PARTICLE LATER Instantiate(bananaParticle, other.transform.position, Quaternion.identity);
            // SAME WITH THIS FOR SOUND             SoundManagerScript.PlaySound("pickup");
            Destroy(other.gameObject);
        }
    }

    }
