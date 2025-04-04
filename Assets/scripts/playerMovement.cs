using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float velocidad = 5f;

    public float fuerzaSalto = 6f;
    public float longitudRCast = 0.86f;
    public LayerMask capaPiso;

    private bool enPiso;
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
        // desplazamiento y animacion horizontal
        float h = Input.GetAxis("Horizontal");
        
        float velocidadX = h * Time.deltaTime * velocidad;

        animator.SetFloat("move", velocidadX*velocidad);

        if (velocidadX < 0)
        {
            transform.localScale = new Vector3(-4, 4, 1);
        }
        if (velocidadX > 0)
        {
            transform.localScale = new Vector3(4, 4, 1);
        }

        Vector3 position = transform.position;
        transform.position = new Vector3(velocidadX + position.x, position.y,position.z);


        // salto y animacion
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, longitudRCast, capaPiso);
        enPiso = hit.collider != null;

        if (enPiso && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector2(0f, fuerzaSalto), ForceMode2D.Impulse);
        }
        animator.SetBool("enPiso", enPiso);
        
    }
    //figuras imaginarias solo se ven en el editor
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * longitudRCast);
    }
}
