using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Necesario para TextMeshPro

public class activar : MonoBehaviour
{
    private TextMeshProUGUI gameOverText; // Referencia al TextMeshPro
    private playerMovement playerScript;
    public bool juegoPausado = false;

    void Start()
    {
        // Buscar el TextMeshPro con tag "win"
        gameOverText = GameObject.FindGameObjectWithTag("win").GetComponent<TextMeshProUGUI>();

        if (gameOverText == null)
        {
            Debug.LogError("No se encontró TextMeshPro con tag 'win'");
        }
        else
        {
            gameOverText.gameObject.SetActive(false); // Ocultar al inicio
        }

        // Obtener referencia al jugador
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<playerMovement>();
    }

    void Update()
    {
        // Verificar si el jugador ha muerto
        if (playerScript != null && playerScript.muerto && !juegoPausado)
        {
            MostrarGameOver();
        }
    }

    void MostrarGameOver()
    {
        if (gameOverText != null)
        {
            gameOverText.text = "GAME OVER"; // Asegurar el texto
            gameOverText.gameObject.SetActive(true);
            Time.timeScale = 0;
            juegoPausado = true;
        }
    }

    public void Reanudar()
    {
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(false);
            Time.timeScale = 1;
            juegoPausado = false;
        }
    }
}