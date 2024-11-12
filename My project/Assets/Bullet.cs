using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Jeg kollidere med noget!");
        if (collision.CompareTag("Deadly"))
        {
            Destroy(collision.gameObject);
        }
      
    }
}
