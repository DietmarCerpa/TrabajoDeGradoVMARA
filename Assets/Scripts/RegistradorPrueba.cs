using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegistradorPrueba : MonoBehaviour
{
    private Rigidbody Punto;
    private TrailRenderer RastroPunto;
    public int Secuencia;
    private int ConteoTiempo;

    private float MoverTiempo, VoltajePrueba;
    bool Sentido, RegistradorON;
    int Ciclo, CicloActual;

    // Use this for initialization
    void Start ()
    {
        Punto = GetComponent<Rigidbody>();
        RastroPunto = GetComponent<TrailRenderer>();
        ConteoTiempo = 60;
        VoltajePrueba = 0;
        Sentido = false;
        RegistradorON = true;
        Ciclo = 50;
        CicloActual = 0;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        while(RegistradorON == true)
        {
            for(float Registrador = 0; Registrador <= 180 ; Registrador = Registrador + 5f)
            {
                if (VoltajePrueba == 0) Sentido = false;
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
                Vector3 PosicionAparicion = new Vector3(Registrador, VoltajePrueba, Punto.position.z);
                //Punto.AddForce(PosicionAparicion  * VelocidadPunto);
                if (Registrador == 0 || Registrador == 180) RastroPunto.Clear();

                Punto.MovePosition(PosicionAparicion);
            }
        }
		
	}

    public void DetenerRegistrador ()
    {
        RegistradorON = false;
        VoltajePrueba = 0;
        Sentido = false;
    }

    public void IniciarRegistrador()
    {
        RegistradorON = true;
    }

    public void PausarRegistrador()
    {
        RegistradorON = false;
    }
    
    public void GuardarImagen()
    {

    }
}
