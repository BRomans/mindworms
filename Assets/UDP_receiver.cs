using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class UDP_receiver : MonoBehaviour
{
    private UdpClient udpClient;
    public int listenPort;
    private Thread _listenThread;
    private bool _listenRunning = false;

    public byte[] returnBytes;

    public Queue<byte[]> _receiveQueue = new Queue<byte[]>();
    private System.Object _receiveQueueLock = new System.Object();

    // Start is called before the first frame update
    void Start()
    {
        _listenThread = new Thread(DataListener);
        _listenThread.Start();
    }

    // Update is called once per frame
    void Update()
    {
        lock (_receiveQueueLock)
        {
            while (_receiveQueue.Count > 0)
            {
                // read the data by the main thread
                /*byte[]*/ returnBytes = _receiveQueue.Dequeue();
                String tempString = Encoding.ASCII.GetString(returnBytes, 0, returnBytes.Length);
                String[] splitString = tempString.Split(' ');
                Debug.Log(splitString[0]);
                Debug.Log(splitString[1]);
                splitString[1] = splitString[1].Replace('.', ',');
                Debug.Log(Double.Parse(splitString[1], System.Globalization.NumberStyles.Float));
                //Debug.Log(float.Parse(splitString[1].Substring(0,splitString[1].Length - 5)));
            }
        }
    }

    private void DataListener()
    {
        udpClient = new UdpClient(listenPort);

        _listenRunning = true;
        IPEndPoint recvEP = new IPEndPoint(IPAddress.Any, 0);
        while (_listenRunning)
        {
            try
            {
                byte[] receivedPackage = udpClient.Receive(ref recvEP);
                HandleReceivedData(receivedPackage);
            }
            catch (Exception e)
            {
                if (_listenRunning) Debug.LogWarning("Exception in UDPConnector.DataListener: " + e);
            }
        }
        udpClient.Close();
        Debug.Log("DataListener Stopped");
    }

    private void HandleReceivedData(byte[] inData)
    {
        lock (_receiveQueueLock) // Lock the queue so that it can't be acessed while writing to the queue
        {
            // put the data from the listener thread in the queue, to be read by the main thread
            _receiveQueue.Enqueue(inData);
        }
    }


    private void OnApplicationQuit()
    {
        _listenRunning = false;
        udpClient.Close();
    }
}