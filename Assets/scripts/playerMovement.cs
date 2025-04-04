using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float velocidad = 5f;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
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
        

    }
}
