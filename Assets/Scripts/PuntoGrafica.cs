using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class PuntoGrafica : MonoBehaviour
{
    private Rigidbody Punto;
    private TrailRenderer punto;
    public int Secuencia;
    private int ConteoTiempo;

    private float MoverTiempo;
    //public Text TextoDeConteo;
    //public Text TextoDeGanador;

    /*
     * esta llamada se realiza la primera vez que se ejecuta la escena o el juego
     */
    void Start()
    {
        Punto = GetComponent<Rigidbody>();
        punto = GetComponent<TrailRenderer>();
        ConteoTiempo = 60;
        //CargarConteo();
        //TextoDeGanador.text = "";
        
        StartCoroutine(Barrido());

    }
    /*
     * se ejecuta cada vez que sucede algo en la escena
     */
    void Update()
    {

    }
    /*
     * se utiliza para realizar algun calculo de "physics" despues de cada "update"
     */
    void FixedUpdate()
    {
        /*
        for(int Inicio = 0;Inicio < ConteoTiempo ; Inicio++)
        {
            
        }
        */
        /*
         * float moverHorizontalmente = Input.GetAxis("Horizontal");
        float moverVerticalmente = Input.GetAxis("Vertical");

        Vector3 Movimiento = new Vector3(moverHorizontalmente, 0.0f, moverVerticalmente);

        RBJugador.AddForce(Movimiento * VelocidadJugador);
        */
    }

    IEnumerator Barrido()
    {
        float VoltajePrueba = 0;
        bool Sentido = false;
        int Ciclo = 50;
        int CicloActual = 0;
        
        while (true)
        {
            if (Secuencia == 0)
            {
                /*
                 * para dos dos segundos por division Inicio = Inicio + 2.5f
                 * para un segundo por division Inicio = Inicio + 5f
                */
                for (float Inicio = 0; Inicio <= 180; Inicio = Inicio + 5f) 
                {
                    
                    if (VoltajePrueba == 0)
                    {
                        Sentido = false;
                    }
                    if (VoltajePrueba <= 50 && Sentido == false)
                    {
                        VoltajePrueba = VoltajePrueba + 5f;
                        if (VoltajePrueba == 50) Sentido = true;
                    }
                    else if (VoltajePrueba <= 50 && Sentido == true)
                    {
                        VoltajePrueba = VoltajePrueba - 5f;
                        if (VoltajePrueba == 50) Sentido = false;
                    }
                    Vector3 PosicionAparicion = new Vector3(Inicio, VoltajePrueba,Punto.position.z);
                    //Punto.AddForce(PosicionAparicion  * VelocidadPunto);
                    if (Inicio == 0 || Inicio == 180) punto.Clear();

                    Punto.MovePosition(PosicionAparicion);

                    yield return new WaitForSeconds(1f);
                }
            }
            else if(Secuencia == 1)
            {
                for(int Inicio = 0;Inicio <= 180;Inicio = Inicio + 5)
                {
                    if (Inicio == 0) punto.Clear();
                    if (CicloActual < Ciclo && Sentido == false)
                    {
                        VoltajePrueba = 50f;
                        CicloActual++;
                        if (CicloActual == 50)
                        {
                            CicloActual = 0;
                            Sentido = true;
                        }
                    }
                    else if (CicloActual < Ciclo && Sentido == true)
                    {
                        VoltajePrueba = 0f;
                        CicloActual++;
                        if (CicloActual == 50)
                        {
                            CicloActual = 0;
                            Sentido = false;
                        }
                    }

                    Vector3 PosicionAparicion = new Vector3(Inicio,VoltajePrueba,Punto.position.z);
                    //Punto.AddForce(PosicionAparicion  * VelocidadPunto);
                    Punto.MovePosition(PosicionAparicion);
                    if (Inicio == 0 || Inicio == 180) punto.Clear();

                    yield return new WaitForSeconds(1f);
                }
            }

        }

    }
}

    























