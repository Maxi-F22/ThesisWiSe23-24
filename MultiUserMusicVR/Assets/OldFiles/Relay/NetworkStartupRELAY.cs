using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using TMPro;

// deprecated NetworkStartup using Relay, commented out because Relay Package is no longer used
public class NetworkStartupRelay : MonoBehaviour
{
    // variables relating to Relay
    public int maxConnection = 5;
    public UnityTransport transport;
    public string joinCode = "";

    // connect to Relay Services on Awake
    // private async void Awake()
    // {
    //     await UnityServices.InitializeAsync();
    //     await AuthenticationService.Instance.SignInAnonymouslyAsync();
    // }

    // public async void Host()
    // {
            // Create Allocation on Relay Services and get joinCode
    //     Allocation allocation = await RelayService.Instance.CreateAllocationAsync(maxConnection);
    //     string newJoinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);

    //     Debug.Log(newJoinCode);

            // Set Connectiondata based on Allocation data
    //     transport.SetHostRelayData(allocation.RelayServer.IpV4, (ushort) allocation.RelayServer.Port,
    //         allocation.AllocationIdBytes, allocation.Key, allocation.ConnectionData);
        
    //     NetworkManager networkManager = NetworkManager.Singleton;
    //     networkManager?.StartHost();
    // }


    // public async void Join()
    // {
            // get Allocation data with joinCode from host
    //     JoinAllocation allocation = await RelayService.Instance.JoinAllocationAsync(joinCode);

            // Set Connectiondata based on Allocation data
    //     transport.SetClientRelayData(allocation.RelayServer.IpV4, (ushort) allocation.RelayServer.Port,
    //         allocation.AllocationIdBytes, allocation.Key, allocation.ConnectionData, allocation.HostConnectionData);
        
    //     NetworkManager networkManager = NetworkManager.Singleton;
    //     networkManager?.StartClient();
    // }
}
