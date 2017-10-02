using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO.Ports;
using System;

public class AgregarPuertos : MonoBehaviour {


    #region INICIO DEFINICION DE VARIABLES (REVISAR CUALES DEBEN ESTAR Y CUALES NO)

    public Dropdown puertos;
    public Text Mensaje, Conect;
    public Button ConectarPuerto;

    string[] ports, Buffer2;
    public int Estado = 0, Leido2, Cantidad, nivel = 0;
    public string mens = "Seleccione el puerto USB" , Leido = "" , Lectura = "", Prueba2;
    public char CaracterLeido;

    SerialPort sp = new SerialPort("COM3", 9600, Parity.None, 8, StopBits.One);// Refrence to serialPort 

    #endregion FIN DEFINICION DE VARIABLES

    #region INICIO VARIABLES DE PRUEBA

    int DecimalLeido, CuantosON = 0, CuantosOFF = 0;
    bool EnTrama = false;
    char DecimalACaracter;
    string DecimalACadena;
    //char InicioTrama = 'O', Indice1 = 'F', Indice2 = 'N', FinTrama = '$';

    string InicioTrama = "O", Indice1 = "F", Indice2 = "N", FinTrama = "$";

    string[] CadenaDeDatos = new string[100];
    int IndiceCadenaDeDatos = 0;


    #endregion FIN VARIABLES DE PRUEBA

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
                DecimalLeido = sp.ReadChar(); //lectura de dato recibido en forma Decimal

                DecimalACadena = Convert.ToString(Convert.ToChar(DecimalLeido)); // conversion del dato de int a str

                if (DecimalACadena == "O")
                {
                    EnTrama = true;
                }
                else if (DecimalACadena == "$")
                {
                    EnTrama = false;
                }

                if (EnTrama == true)
                {
                    if(DecimalACadena != "O")
                    {
                        CadenaDeDatos[IndiceCadenaDeDatos] = DecimalACadena;
                        IndiceCadenaDeDatos++;
                    }
                    /*
                    if (Indice1 == DecimalACadena)
                    {
                        CuantosOFF++;
                        Debug.Log(CuantosOFF);
                    }
                    else if (Indice2 == DecimalACadena)
                    {
                        CuantosON++;
                        Debug.Log(CuantosON);
                    }
                    */
                }
                else if (EnTrama == false)
                {
                    Debug.Log("Final trama");

                    AnalizarTrama(CadenaDeDatos);

                    CadenaDeDatos.Initialize();
                    
                    IndiceCadenaDeDatos = 0;
                    
                }
                



            }
            catch (Exception Mensaje)
            {
                Debug.Log(Mensaje);
            }
        }
    }


    #region Inicio Area de Funciones y Procedimientos
    /// <summary>
    /// En esta funcion estan las acciones correspondientes a la verificacion de los datos recibidos 
    /// por enlace SERIAL a traves del Puerto COMX
    /// </summary>
    private void AnalizarTrama(string[] CadenaDeDatosRecibidos )
    {
        if (CadenaDeDatosRecibidos[0] == "F")
        {
            CuantosOFF++;
            Debug.Log(CuantosOFF);
            if (CadenaDeDatosRecibidos[1] == "F")
            {
                Debug.Log("TRAMA OFF RECIBIDA:  " + CuantosOFF + "  VECES");
            }
        }
        else if (CadenaDeDatosRecibidos[0] == "N")
        {
            CuantosON++;
            Debug.Log("TRAMA ON RECIBIDA:  " + CuantosON + "  VECES");
        }
    } 


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

