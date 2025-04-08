using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float velocidad = 5f;

    public float fuerzaSalto = 6f;
    public float fuerzaRebote = 4f;
    public float longitudRCast = 0.86f;
    public LayerMask capaPiso;

    private bool enPiso;
    private bool recibeDaniov;
    private bool atacandov;
    private Rigidbody2D rb;


    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!atacandov)
        {
            movimiento();
            salto();
        }
        ataque();
        animaciones();
        
    }
    //mecanica de danio
    public void recibeDanio(Vector2 direccion,int cantDamage)
    {
        if(!recibeDaniov)
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
    public void atacando()
    {
        atacandov = true;
    }
    public void desAtacando()
    {
        atacandov = false;
    }

    public void movimiento()
    {
        // desplazamiento y animacion horizontal
        float h = Input.GetAxis("Horizontal");

        float velocidadX = h * Time.deltaTime * velocidad;

        animator.SetFloat("move", velocidadX * velocidad);

        if (velocidadX < 0)
        {
            transform.localScale = new Vector3(-4, 4, 1);
        }
        if (velocidadX > 0)
        {
            transform.localScale = new Vector3(4, 4, 1);
        }

        Vector3 position = transform.position;
        transform.position = new Vector3(velocidadX + position.x, position.y, position.z);

        //personaje no se puede mover por danio
        if (!recibeDaniov)
            transform.position = new Vector3(velocidadX + position.x, position.y, position.z);
    }

    public void salto()
    {
        // salto y animacion
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, longitudRCast, capaPiso);
        enPiso = hit.collider != null;

        if (enPiso && Input.GetKeyDown(KeyCode.Space) && !recibeDaniov)
        {
            rb.AddForce(new Vector2(0f, fuerzaSalto), ForceMode2D.Impulse);
        }
    }

    public void ataque()
    {
        //ataque
        if (Input.GetKeyDown(KeyCode.Z) && !atacandov && enPiso)
        {
            atacando();
        }
    }

    public void animaciones()
    {
        animator.SetBool("enPiso", enPiso);

        animator.SetBool("recibeDanio", recibeDaniov);

        animator.SetBool("atak", atacandov);
    }

    //figuras imaginarias solo se ven en el editor
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * longitudRCast);
    }
}
