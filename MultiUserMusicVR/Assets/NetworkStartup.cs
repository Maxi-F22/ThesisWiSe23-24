using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using TMPro;

public class NetworkStartup : MonoBehaviour
{  
    // set IP and Port of Host-Device
    public string serverIp = "192.168.0.10";
    private ushort serverPort = 7777;

    public void Host()
    {
        // sets connection data based on IP and Port and starts connection as host
        NetworkManager networkManager = NetworkManager.Singleton;

        if (networkManager != null)
        {
            networkManager.GetComponent<UnityTransport>().SetConnectionData(serverIp, serverPort, "0.0.0.0");

            bool hostStarted = networkManager.StartHost();
            if (hostStarted)
            {
                // remove GameObjects that are used to start connections
                Destroy(GameObject.Find("StartupXR"));
                Destroy(GameObject.Find("Canvas"));
            }
            else
            {
                Debug.Log("Connection Failed");
            }
        }
    }
    public void Join()
    {
        // sets connection data based on IP and Port of host and starts connection as client
        NetworkManager networkManager = NetworkManager.Singleton;

        if (networkManager != null)
        {
            networkManager.GetComponent<UnityTransport>().SetConnectionData(serverIp, serverPort);

            bool clientStarted = networkManager.StartClient();
            if (clientStarted)
            {
                // remove GameObjects that are used to start connections
                Destroy(GameObject.Find("StartupXR"));
                Destroy(GameObject.Find("Canvas"));
            }
            else
            {
                Debug.Log("Connection Failed");
            }
        }
    }
}

