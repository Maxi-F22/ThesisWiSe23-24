using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

// inherits from Continuous Move Provider (Action-Based)
public class NetworkMoveProvider : ActionBasedContinuousMoveProvider
{
    // enableInputActions is only activated on the own PlayerCharacter XR Orign
    public bool enableInputActions;

    // ReadInput returns no input value if XR Origin is PlayerCharacter of someone else
    protected override Vector2 ReadInput()
    {
        if (!enableInputActions) return Vector2.zero;
        return base.ReadInput();
    }
}
