using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public int Puntos { get { return puntos; } }
    private int puntos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SumarPuntos(int puntosSumar)
    {
        puntos += puntosSumar;
    }
}
