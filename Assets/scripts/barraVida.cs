using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class barraVida : MonoBehaviour
{
    //acceder al amount
    public UnityEngine.UI.Image rellenoVida;
    //el personaje tiene las vidas
    private playerMovement playerController;
    private float vidaMaxima;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("player").GetComponent<playerMovement>();
        vidaMaxima = playerController.vida;
    }

    // Update is called once per frame
    void Update()
    {
        //conectar con la barra (fill)
        rellenoVida.fillAmount = playerController.vida / vidaMaxima;
    }
}
