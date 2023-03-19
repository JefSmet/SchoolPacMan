/* Testprog serial port from Arduino threaded
 * Wim Van Weyenberg
 * 18/09/2018
 * In Start() wordt de comport geopend (stel juiste naam in van de Arduino Port!) en wordt ook de thread gestart om data te ontvangen en te zenden
 * Ontvangen en zenden gebeurt in dezelfde thread
 * Ontvangen moet via aparte thread omdat we sp.ReadTimeout = 20 ms lang moeten wachten om te weten of er iets ontvangen is.
 * Als er data ontvangen is wordt deze in update getoond via Debug.Log
 * Om dat te zenden gebruiken we hier de A en U toets op het keyboard
 * You have to set the File -> Build Settings -> Player Settings -> API Compatibility Level to .NET2.0 (NOT the subset).
 */


using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System.Threading;
using System;
using QuestMan.Observer;
using Unity.VisualScripting.Antlr3.Runtime;

public enum ArduinoLight
{
    GreenLight,
    RedLight
}

public class SerialCommThreaded : MonoBehaviour
{
    public static event Action onButtonPressed;
    public static event Action<int> onPotentioValueChanged;
    [SerializeField] float blinkInterval = 0.05f;

    public SerialPort sp = new SerialPort("COM3", 9600, Parity.None, 8, StopBits.One);
    private bool blnPortcanopen = false; //if portcanopen is true the selected comport is open

    //statics to communicate with the serial com thread
    static private string string_in; //read line from serial port
    //static private int databyte_in; //read databyte from serial port
    static private bool databyteRead = false; //becomes true if there is indeed a character received
    static private int databyte_out; //index in txChars array of possible characters to send
    static private bool databyteWrite = false; //to let the serial com thread know there is a byte to send
    //txChars contains the characters to send: we have to use the index
    private char[] txChars = { 'O', 'I', 'U', 'A'}; 

    //threadrelated
    private bool stopSerialThread = false; //to stop the thread
    private Thread readWriteSerialThread; //threadvariabele    

    private int potDiv10 = -1;

    void Start()
    {        
        OpenConnection(); //init COMPort
                          //define thread and start it
        readWriteSerialThread = new Thread(SerialThread);
        readWriteSerialThread.Start(); //start thread
    }

    void Update()
    {        
        if (databyteRead) //if a databyte is received
        {
            int potValue;            
            if (string_in.Equals("B"))
            {
                onButtonPressed?.Invoke();
            }
            else if (int.TryParse(string_in, out potValue))
            {
                if (IsPotDiv10Changed(potValue))
                {
                    onPotentioValueChanged?.Invoke(potValue);
                }                
            }
            databyteRead = false; //to see if a next databyte is received
        }    
    }

    bool IsPotDiv10Changed(int value)
    {
        int curr = value / 10;
        if (potDiv10 != curr)
        {
            potDiv10 = curr;
            return true;
        }
        return false;
    }

    public void SetLight(ArduinoLight light, bool on)
    {
        if (on)
        {
            if (light == ArduinoLight.GreenLight)
            {
                databyte_out = 1; //index in txChars
            }
            else
            {
                databyte_out = 3; //index in txChars
            }
            
        }
        else
        {
            if (light == ArduinoLight.GreenLight)
            {
                databyte_out = 0; //index in txChars
            }
            else
            {
                databyte_out = 2; //index in txChars
            }
        }
        databyteWrite = true;
    }

    public void Blink(ArduinoLight light)
    {
        StartCoroutine(blink(light));
    }

    IEnumerator blink(ArduinoLight light)
    {
        SetLight(light, false);
        SetLight(light, true);
        yield return new WaitForSeconds(blinkInterval);
        SetLight(light, false);
    }
    void SerialThread() //separate thread is needed because we need to wait sp.ReadTimeout = 20 ms to see if a byte is received
    {
        while (!stopSerialThread) //close thread on exit program
        {
            if (blnPortcanopen)
            {
                if (databyteWrite)
                {   // txChars = { 'O', 'I', 'U', 'A'}; 
                    if (databyte_out == 0)
                    {
                        sp.Write(txChars, 0, 1); //tx 'O'
                    }
                    if (databyte_out == 1)
                    {
                        sp.Write(txChars, 1, 1); //tx 'I'
                    }
                    if (databyte_out == 2)
                    {
                        sp.Write(txChars, 2, 1); //tx 'U'
                    }
                    if (databyte_out == 3)
                    {
                        sp.Write(txChars, 3, 1); //tx 'A'
                    }
                    databyteWrite = false; //to be able to send again
                }
                try //trying something to receive takes 20 ms = sp.ReadTimeout
                {
                    string_in = sp.ReadLine();
                    databyteRead = true;                                                                         
                }
                catch (Exception e)
                {
                    databyteRead = false;
                    Debug.Log("Arduino-error: "+e.Message);
                }
            }
        }
    }


    //Function connecting to Arduino
    public void OpenConnection()
    {
        if (sp != null)
        {
            if (sp.IsOpen)
            {
                string message = "Port is already open!";
                Debug.Log(message);
            }
            else
            {
                try
                {
                    sp.Open();  // opens the connection
                    blnPortcanopen = true;
                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                    blnPortcanopen = false;
                }
                if (blnPortcanopen)
                {
                    sp.ReadTimeout = 20;  // sets the timeout value before reporting error
                    Debug.Log("Port Opened!");
                }
            }
        }
        else
        {
            Debug.Log("Port == null");
        }
    }


    void OnApplicationQuit() //proper afsluiten van de thread
    {
        if (sp != null) sp.Close();
        stopSerialThread = true;
        readWriteSerialThread.Abort();
    }
}
