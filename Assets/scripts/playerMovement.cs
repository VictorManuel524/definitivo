using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float velocidad = 5f;
    public Transform body;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        //girar el personaje
        float direccion = body.localScale.x;
        if (h < 0)
        {
            direccion = Mathf.Abs(body.localScale.x) * -1;
        }
        else if (h > 0)
        {
            direccion = Mathf.Abs(body.localScale.x);
        }

        float velocidadX = h * Time.deltaTime * velocidad;
        Vector3 position = transform.position;
        transform.position = new Vector3(velocidadX + position.x, position.y,position.z);
        

    }
}
