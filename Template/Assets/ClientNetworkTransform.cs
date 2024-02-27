using System.Collections;
using System.Collections.Generic;
using Unity.Netcode.Components;
using UnityEngine;

// inherits from Network Transform
public class ClientNetworkTransform : NetworkTransform
{
    // overrides function to make transform of GameObject editable by clients
    protected override bool OnIsServerAuthoritative()
    {
        return false;
    }
}
