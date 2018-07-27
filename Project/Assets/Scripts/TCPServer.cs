using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System;
using System.Threading;
using System.IO;

public class TCPServer : MonoBehaviour
{


    Thread ConnectThread;
    Thread ReadFromSerialThread;
    public Int32 port = 13000;
    IPAddress localAddr = IPAddress.Parse("127.0.0.1");
    //bool mRunning = true;
    //Byte[] bytes = new Byte[256];
    //String data = null;
    TcpListener server = null; //creo il listener
    StreamReader reader = null; //creo il reader per prelevare i dati ricevuti
    bool pendingConnection = false;
    static bool connected = false;
    static string msg = null;
    static int BPM = 0;
    // Use this for initialization


    void Start()
    {

        ThreadStart ts = new ThreadStart(SayHello);
        ConnectThread = new Thread(ts);
        ConnectThread.Start();
        print("thread invocato = " + ConnectThread.IsAlive);

    }


    void SayHello()
    {
        try
        {
            server = new TcpListener(localAddr, port); //avvio del server
            server.Start();
            print("Server Start");

            while (pendingConnection == false) //rimane in ascolto per uan connessione
            {
                pendingConnection = server.Pending();
                Thread.Sleep(100);
            }

            connected = true; //esce dal while perchè la connessione è stata stabilita
            print("Accepting client connection...");
            TcpClient client = server.AcceptTcpClient();
            print("Client connected, getting stream...");
            NetworkStream ns = client.GetStream();
            print("Network stream acquired, now reading...");
            reader = new StreamReader(ns);
            string msg = reader.ReadLine();
            print("Success!");
            print(msg);

            ThreadStart ts = new ThreadStart(ReadFromSerial);
            ReadFromSerialThread = new Thread(ts);
            ReadFromSerialThread.Start();

        }
        catch (ThreadAbortException)
        {
            print("exception");
        }
        //finally
        //{
        //    mRunning = false;
        //    server.Stop();
        //}

    }

    void ReadFromSerial()
    {
        print("Readfromserial");
        while (connected == true)
        {
            msg = reader.ReadLine(); //leggo il messaggio nello stream

            if (msg != "")
            {
                print(msg);

                try
                {
                    BPM = int.Parse(msg);
                }
                catch
                {
                    //se succede schifo 
                }

            }
            else
            {
                if (msg == null)
                {
                    print("Messaggio = null, connessione col client persa");
                    break; //esco dal ciclo se il messaggio è null, significa che la connessione è andata perduta
                }


            }
        }

        if (!ConnectThread.IsAlive) //chiudo e riapro il server in attesa di una nuova connessione
        {
            connected = false;
            pendingConnection = false;
            server.Stop();

            // richiama il thread per la connessione
            ThreadStart ts = new ThreadStart(SayHello);
            ConnectThread = new Thread(ts);
            ConnectThread.Start();
            print("Riavviato thread di connessione");
        }
    }

    public void StopListening()
    {
        //mRunning = false;
    }


    void OnApplicationQuit()
    {
        // stop listening thread
        StopListening();
        // wait fpr listening thread to terminate (max. 500ms)
        //ConnectThread.Join(500);
        //ReadFromSerialThread.Join(500);
        ReadFromSerialThread.Abort();
    }

    // Update is called once per frame
    void Update()
    {

        //if (msg != null)
        //{
        //    try
        //    {
        //        BPM = int.Parse(msg);
        //    }
        //    catch
        //    {
        //        //se succede schifo 
        //    }
        //}


    }

    public static int getvalueBPM()//funzione pubblica per permettere passare il valore ad altri script
    {
        return BPM;
    }

    public static bool getConnectionStatus()//funzione pubblica per permettere passare il valore ad altri script
    {
        return connected;
    }
}