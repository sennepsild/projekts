using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Speed;
    public bool WillFall;
    public bool moveLeft;

    Rigidbody2D rb;
    SpriteRenderer rend;
    Vector2 direction = Vector2.right;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        if(moveLeft)
        {
            CHangeDirection();
        }

    }
    void CHangeDirection()
    {
        direction = direction == Vector2.right ? Vector2.left : Vector2.right;
        rend.flipX = !rend.flipX;
    }
    void LookForward()
    {
        Debug.DrawRay(transform.position, direction * transform.localScale.x * 0.7f, Color.red);
        RaycastHit2D[] hitHori = Physics2D.RaycastAll(transform.position, direction, transform.localScale.x * 0.7f);
        foreach (RaycastHit2D hit in hitHori)
        {
            if (hit.collider.gameObject == gameObject) continue;
            if (hit.collider.CompareTag("Player")) continue;
            CHangeDirection();
        }
    }
 
    void LookDown()
    {
        Debug.DrawRay(transform.position, (direction * transform.localScale.x * 0.7f)+ Vector2.down*0.5f, Color.yellow);
        RaycastHit2D[] hitHori = Physics2D.RaycastAll(transform.position, direction +Vector2.down, transform.localScale.x * 0.7f+0.5f);
        int count = 0;
        foreach (RaycastHit2D hit in hitHori)
        {
            if (hit.collider.gameObject == gameObject) continue;
            if (hit.collider.CompareTag("Player")) continue;
            count++;
            
        }

        if (count == 0)
        {
            CHangeDirection();
        }
    }
    void Update()
    {
        Vector2 newSpeed = direction;
        newSpeed *= Speed;
        newSpeed.y = rb.velocity.y;
        rb.velocity = newSpeed;

        LookForward();
        if(!WillFall)
            LookDown();
    }

    
}
