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
        GameObject leftHand = gameObject.GetNamedChild("Left Hand Presence Physics");
        GameObject rightHand = gameObject.GetNamedChild("Right Hand Presence Physics");
        leftHand.name += OwnerClientId;
        rightHand.name += OwnerClientId;
        GameObject leftContr = gameObject.GetNamedChild("Left Controller");
        GameObject rightContr = gameObject.GetNamedChild("Right Controller");
        leftContr.name += OwnerClientId;
        rightContr.name += OwnerClientId;
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
            var clientHandAnimation = GetComponentsInChildren<Animator>();

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
            foreach (var hand in clientHandAnimation)
            {
                hand.enabled = false;
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
