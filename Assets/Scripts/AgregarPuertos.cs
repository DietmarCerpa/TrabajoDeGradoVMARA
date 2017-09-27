using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO.Ports;
using System;

public class AgregarPuertos : MonoBehaviour {

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
    
   
    
    // INICIALIZACION
    void Start()
    {
        
        // Fill ports array with COM's Name available
        ports = SerialPort.GetPortNames();
        //clear/remove all option item
        puertos.options.Clear();
        sp.ReadTimeout = 1;
        
        //fill the dropdown menu OptionData with all COM's Name in ports[]
        //puertos.options.Add(new Dropdown.OptionData() { text= "Puertos Disponibles" });
        foreach (string c in ports)
        {
            if (c == "COM3")
            {
                puertos.options.Add(new Dropdown.OptionData() { text = "Sin Conexión" });
                sp.Close();
            }
            else puertos.options.Add(new Dropdown.OptionData() {text =  c });
        }
        if (sp.IsOpen == true) Debug.Log("Abierto COM 3");
        else Debug.Log("Cerrado COM 3");

        //StartCoroutine(Espera());
        //StartCoroutine(Prueba());

    }

    IEnumerator Prueba()
    {
        while (true)
        {
            if (sp.PortName != "COM3" && sp.IsOpen)
            {
                // Debug.Log("entro");
                try
                {
                    //Leido = sp.ReadLine();}
                    Leido2 = sp.ReadChar();
                    //MoveObject(sp.ReadByte());
                    if (Leido != "")
                    {
                        Debug.Log(Leido);
                    }
                    else Debug.Log("No hay");
                }

                catch (System.Exception e)
                {
                    Debug.Log(e);
                    throw;
                }
                yield return new WaitForSeconds(1f);
            }
            else
            {
                Debug.Log("no entro");
                yield return new WaitForSeconds(1f);
            }
        }

    }


    public void Conectar() //ASIGNADO AL BOTON DE CONECTAR
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


    /*
    void Update()
    {

        if (sp != null && sp.IsOpen)
        {
            StartCoroutine(Leerdatospic());
        }
    }
    */
 
    void Update()
    {
        if(sp.IsOpen == true)
        {
            try
            {
                Leido2 = sp.ReadChar();


                //CaracterLeido = Convert.ToChar(Leido2);

                Lectura = Lectura + Convert.ToChar(Leido2);

                Debug.Log("Lectura " + Lectura);
                Debug.Log("Caracter Leido " + Leido2);

                               

                 
            }
            catch (Exception Mensaje)
            {
                //Debug.Log(Mensaje);
            }
           
            
        }


        /*
        if (sp.PortName != "COM3")
        {
            if (sp.IsOpen == true && sp.ReadLine() != null)
            {
                string[] values = sp.ReadLine().Split('$');
                Mensaje.text = values[0];
                foreach (string word in values)
                {
                    Mensaje.text = Mensaje + word;
                }
                
            }
        }*/
         /*
        if (sp.IsOpen)
        {
            try
            {
                //MoveObject(sp.ReadByte());
                if (sp.ReadByte() != 0)
                {
                    print(sp.ReadByte());
                }
            }

            catch (System.Exception e)
            {
                print(e);
                throw;
            }
        }
        */
    }

    IEnumerator Espera()
    {
        if (sp.PortName != "COM3") yield return new WaitForSeconds(0.05f);
    }


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
 
    /*
    private void StartCoroutine(Func<IEnumerator> leerdatospic)
    {
        throw new NotImplementedException();
    }*/


    /*IEnumerator Leerdatospic()
    {
        while (true)
        {
            string[] values = sp.ReadLine().Split('$');
            Mensaje.text = values[0];
            yield return new WaitForSeconds(0.05f);
        }

    }*/
    /*
    IEnumerator Leerdatospic()
    {
        while (true)
        {
            string[] values = sp.ReadLine().Split('$');
            Mensaje.text = values[0];
            yield return new WaitForSeconds(0.05f);
        }

    }*/
}




