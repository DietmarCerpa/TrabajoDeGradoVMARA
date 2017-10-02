using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO.Ports;
using System;

public class AgregarPuertos : MonoBehaviour {


    #region INICIO DEFINICION DE VARIABLES

    public Dropdown puertos;
    public Text Mensaje;
    public Button ConectarPuerto;
    public Text Conect;
    public string mens = "Seleccione el puerto USB";
    string[] ports;
    int Estado = 0 ;
    //byte[] DatoLeido;
    public string Leido = "";
    public int Leido2;
    public char CaracterLeido;
    public string Lectura = "";
    public int Cantidad;
    string Prueba2;
    string[] Buffer2;
    int nivel = 0;
    SerialPort sp = new SerialPort("COM3", 9600, Parity.None, 8, StopBits.One);// Refrence to serialPort 

    #endregion FIN DEFINICION DE VARIABLES

    void Start()
    {
        ports = SerialPort.GetPortNames(); // Fill ports array with COM's Name available
        puertos.options.Clear(); //clear/remove all option item
        sp.ReadTimeout = 1; //de no encontrar nada que leer solo espera 1 ms
        
        LlenarPuertos();
        //puertos.options.Add(new Dropdown.OptionData() { text= "Puertos Disponibles" });
        //StartCoroutine(Espera());
        //StartCoroutine(Prueba());
    }

    void Update()
    {
        if (sp.IsOpen == true)
        {
            try
            {

                int DecimalLeido = sp.ReadChar(); //lectura de dato recibido en forma Decimal

                char LeidoACaracter = Convert.ToChar(Leido2); //conversion del dato de int a chr

                string DecimalACadena = Convert.ToString(Leido2); // conversion del dato de int a str



            }
            catch (Exception Mensaje)
            {
                //Debug.Log(Mensaje);
            }
        }
    }


    #region Inicio Area de Funciones y Procedimientos
    /// <summary>
    /// Esta funcion permite llenar el "DROPDOWN" con los puertos SERIALES (COM) disponibles
    /// </summary>
    private void LlenarPuertos()
    {
        foreach (string c in ports)
        {
            if (c == "COM3")
            {
                puertos.options.Add(new Dropdown.OptionData() { text = "Sin Conexión" });
                sp.Close();
            }
            else puertos.options.Add(new Dropdown.OptionData() { text = c });
        }
        if (sp.IsOpen == true) Debug.Log("Abierto COM 3");
        else Debug.Log("Cerrado COM 3");
    }
    /// <summary>
    /// Permite ejecutar la accion de ABRIR el puerto COM seleccionado en el DROPDOWN
    /// </summary>
    public void Conectar() 
    {
        if (Estado == 0)
        {
            
            try
            {
                sp.Open();
                
                mens = "Puerto " + sp.PortName + " Abierto";
                puertos.enabled = false;
                Conect.text = "Desconectar";
                Estado = 1;
            }
            catch
            {
                mens = "intente conectar nuevamente";
            }
        }
        else if (Estado == 1)
        {
            sp.Close();
            mens = "Puerto " + sp.PortName + " Cerrado";
            puertos.enabled = true;
            Conect.text = "Conectar";
            Estado = 0;
        }
        Mensaje.text = mens;
    }
    /// <summary>
    /// Funcion que permite seleccionar un puerto desde el DROPDOWN 
    /// </summary>
    /// <param name="index"></param>
    public void Dropdown_IndexChange(int index)
    {
        if (ports[index] == "Sin conexión" || ports[index] == "COM3")
        {
            mens = "Elige otro";
            ConectarPuerto.enabled = false;
        }
        else
        {
            ConectarPuerto.enabled = true;
            sp.PortName = ports[index];
        }
        Mensaje.text = mens;
    }
    #endregion Fin de Area de Funciones y Procedimientos

}

