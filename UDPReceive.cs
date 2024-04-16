using UnityEngine;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class UDPReceive : MonoBehaviour
{
    // Thread and client variables
    Thread receiveThread;
    UdpClient client;

    // Port number for UDP communication
    public int port = 2411;

    // Boolean variables for controlling receiving and printing behavior
    public bool startRecieving = true;
    public bool printToConsole = false;

    // Variable to store received data
    public string data;

    // Start method called when the script is initialized
    public void Start()
    {
        // Start a new thread for receiving data
        receiveThread = new Thread(
            new ThreadStart(ReceiveData));
        receiveThread.IsBackground = true;
        receiveThread.Start();
    }

    // Method for receiving data on a separate thread
    private void ReceiveData()
    {
        // Initialize a UDP client to listen on the specified port
        client = new UdpClient(port);

        // Continuously listen for data while startRecieving is true
        while (startRecieving)
        {
            try
            {
                // Receive data from any IP address
                IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
                byte[] dataByte = client.Receive(ref anyIP);
                // Convert received byte array to string using UTF-8 encoding
                data = Encoding.UTF8.GetString(dataByte);

                // Print received data to the console if printToConsole is true
                if (printToConsole) { print(data); }
            }
            catch (Exception err)
            {
                // Print any exceptions that occur during receiving
                print(err.ToString());
            }
        }
    }
}

