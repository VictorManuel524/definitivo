using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dano : MonoBehaviour
{
    private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 direccionDanio = new Vector2(transform.position.x, 0);
            playerMovement playerScript = collision.gameObject.GetComponent<playerMovement>();
            playerScript.recibeDanio(direccionDanio, 1);
        }
    }
}
