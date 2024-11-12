using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float Speed;
    public float JumpForce;

    Rigidbody2D rb;
    bool grounded;
    bool dead;

    Animator animator;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public GameObject bulletPrefab;
    void Update()
    {
        if (!dead)
        {
            Movement();
            SetAnimation();
        }
        if(rb.velocity.y < -20)
        {
            Die();
        }
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("jeg klikker med musen");
            GameObject spawnedBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition)- transform.position;
            direction.z = 0;
            direction = direction.normalized * 5;
            spawnedBullet.GetComponent<Rigidbody2D>().velocity = direction;
        }
    }

    void SetAnimation()
    {
        animator.SetFloat("VertSpeed", rb.velocity.y);
        animator.SetFloat("HortSpeed", Mathf.Abs( rb.velocity.x));
        animator.SetBool("Grounded", grounded);
        if(rb.velocity.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        if(rb.velocity.x < 0)
        {
            spriteRenderer.flipX = true;
        }
    }
    int jumpCount = 0;
    void Movement()
    {
        Vector2 newSpeed = new Vector2(Input.GetAxisRaw("Horizontal") * Speed, rb.velocity.y);
        if ( Input.GetButtonDown("Jump") && jumpCount <1)
        {
            if (grounded == false)
            {
                animator.SetTrigger("AirJump");
            }

            grounded = false;
            newSpeed.y = JumpForce;
            jumpCount = jumpCount + 1;
            Debug.Log(jumpCount);
            
        }
        rb.velocity = newSpeed;
    }

    IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void MakeGroundedOnFlat(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y >= 0.5f)
            {
                grounded = true;
                jumpCount = 0;
            }
        }
    }

    void Die()
    {
        if(dead) return;
        dead = true;
        rb.velocity = Vector2.zero;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        animator.SetTrigger("Explode");
        StartCoroutine(ReloadScene());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CollectAble"))
        {
            Destroy(collision.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Deadly"))
        {
            Die();
        }
        
        MakeGroundedOnFlat(collision);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        MakeGroundedOnFlat(collision);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y >= 0.5f)
            {
                return;
            }
        }
        grounded = false;
    }
}
