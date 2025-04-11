using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemigo2 : MonoBehaviour
{
    public Transform player;
    public float detectionRadius = 5f;
    public float speed = 12.5f;
    private bool recibeDaniov;
    public int vida = 10;

    private Rigidbody2D rb;
    private Vector2 movement;

    private bool muerto;
    private bool moveAnim;
    public float fuerzaRebote = 4f;
    public Animator animator;
    private bool playerVivo;
    // Start is called before the first frame update
    void Start()
    {
        playerVivo = true;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerVivo && !muerto)
        {
            Movimiento();
        }
        animator.SetBool("muerto", muerto);
        animator.SetBool("move", moveAnim);
        animator.SetBool("recibeDano", recibeDaniov);
    }

    private void Movimiento()
    {
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
        if (!recibeDaniov)
            rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
    }

    //usamos la funcion del player 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 direccionDanio = new Vector2(transform.position.x, 0);
            playerMovement playerScript = collision.gameObject.GetComponent<playerMovement>();
            playerScript.recibeDanio(direccionDanio, 1);

            playerVivo = !playerScript.muerto;
            if (!playerVivo)
            {
                moveAnim = false;
            }
        }
    }
    //dibuja el radio de deteccion del enemigo
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("espada"))
        {
            Vector2 direccionDanio = new Vector2(collision.gameObject.transform.position.x, 0);
            RecibeDanio(direccionDanio, 1);
        }
    }
    public void RecibeDanio(Vector2 direccion, int cantDamage)
    {
        if (!recibeDaniov)
        {
            vida -= cantDamage;
            recibeDaniov = true;
            if (vida <= 0)
            {
                muerto = true;
                moveAnim = false;
            }
            else
            {
                Vector2 rebote = new Vector2(transform.position.x - direccion.x, 0.8f).normalized;
                rb.AddForce(rebote * fuerzaRebote, ForceMode2D.Impulse);
            }
        }
    }
    public void DesactivaDanio()
    {
        recibeDaniov = false;
    }

}