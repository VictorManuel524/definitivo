using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Transform player;
    public float detectionRadius = 5f;
    public float speed = 6f;

    private Rigidbody2D rb;
    private Vector2 movement;
    private bool recibeDaniov;
    private bool moveAnim;
    public float fuerzaRebote = 6f;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        movimiento();
    }
    //usamos la funcion del player 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 direccionDanio = new Vector2(transform.position.x, 0);
            collision.gameObject.GetComponent<playerMovement>().recibeDanio(direccionDanio, 1);
        }
    }
    //dibuja el radio de deteccion del enemigo
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
    private void movimiento()
    {
        //enemigo se mueve dependiendo del jugador
        float distacePlayer = Vector2.Distance(transform.position, player.position);
        if (distacePlayer < detectionRadius)
        {
            Vector2 direccion = (player.position - transform.position).normalized;

            if (direccion.x < 0)
            {
                transform.localScale = new Vector3(-3.5f, 3.5f, 1);
            }
            if (direccion.x > 0)
            {
                transform.localScale = new Vector3(3.5f, 3.5f, 1);
            }

            movement = new Vector2(direccion.x, 0);
            moveAnim = true;
        }
        else
        {
            movement = Vector2.zero;
            moveAnim = false;
        }
        if(!recibeDaniov)
            rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
        animator.SetBool("move", moveAnim);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("espada"))
        {
            Vector2 direccionDanio = new Vector2(collision.gameObject.transform.position.x, 0);
            recibeDanio(direccionDanio, 1);
        }
    }
    public void recibeDanio(Vector2 direccion, int cantDamage)
    {
        if (!recibeDaniov)
        {
            recibeDaniov = true;
            Vector2 rebote = new Vector2(transform.position.x - direccion.x, 0.8f).normalized;
            rb.AddForce(rebote * fuerzaRebote, ForceMode2D.Impulse);
        }
    }

    public void desactivaDanio()
    {
        recibeDaniov = false;
        rb.velocity = Vector2.zero;
    }

}
