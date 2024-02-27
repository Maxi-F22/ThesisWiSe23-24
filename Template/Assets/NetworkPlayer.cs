using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem.XR;
using Unity.XR.CoreUtils;

// inherits from NetworkBehaviour
public class NetworkPlayer : NetworkBehaviour
{

    // upon spawning of PlayerObject in network, call DisableClientInput and add OwnerClientId to the name of every controller component
    public override void OnNetworkSpawn()
    {
        DisableClientInput();
    }

    // disable every input, camera, animation and audiolistener on every PlayerObject that spawns, except the own
    public void DisableClientInput()
    {
        if (IsClient && !IsOwner) 
        {
            var clientMoveProvider = GetComponent<NetworkMoveProvider>();
            var clientTurnProvider = GetComponent<ActionBasedSnapTurnProvider>();
            var clientControllers = GetComponentsInChildren<ActionBasedController>();
            var clientHead = GetComponentInChildren<TrackedPoseDriver>();
            var clientCamera = GetComponentInChildren<Camera>();
            var clientAudio = GetComponentInChildren<AudioListener>();

            clientCamera.enabled = false;
            clientMoveProvider.enableInputActions = false;
            clientTurnProvider.enableTurnLeftRight = false;
            clientTurnProvider.enableTurnAround = false;
            clientHead.enabled = false;
            clientAudio.enabled = false;

            foreach (var controller in clientControllers)
            {
                controller.enableInputActions = false;
                controller.enableInputTracking = false;
            }
        }
    }

    // on grabbing an object while being a client, send call to Server to request ownership
    public void OnSelectGrabbable(SelectEnterEventArgs eventArgs) 
    {
        if (IsClient && IsOwner) 
        {
            NetworkObject networkObjectSelected = eventArgs.interactableObject.transform.GetComponent<NetworkObject>();
            if (networkObjectSelected != null) 
            {
                RequestGrabbableOwnershipServerRpc(OwnerClientId, networkObjectSelected);
                eventArgs.interactableObject.transform.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
    }

    [ServerRpc]
    public void RequestGrabbableOwnershipServerRpc(ulong newOwnerClientId, NetworkObjectReference networkObjectReference)
    {
        if (networkObjectReference.TryGet(out NetworkObject networkObject))
        {
            networkObject.ChangeOwnership(newOwnerClientId);
        }
        else
        {
            Debug.Log($"Unable to change owner to ClientId {newOwnerClientId}");
        }
    }
}
