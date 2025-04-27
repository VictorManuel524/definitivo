using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vidas : MonoBehaviour
{
    int vida = 1;
    public AudioClip sonidoVida;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerMovement playerScript = collision.gameObject.GetComponent<playerMovement>();
            playerScript.curar(vida);
            Destroy(this.gameObject);
            AudioManager.Instance.ReproducirSonido(sonidoVida);
        }
    }
}
