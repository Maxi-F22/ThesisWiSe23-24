using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrabObject : MonoBehaviour
{
    // assign the physics hand object via Editor
    public GameObject handPresenceObject;

    // calls function on SelectEntered Event on Controller to hide hand object
    public void HideHand() 
    {
        handPresenceObject.SetActive(false);
    }

    // calls function on SelectExited Event on Controller to show hand object 0.5 seconds after releasing grabbed object
    public void ShowHand() 
    {
        Invoke("ShowHandSetActive", 0.5f);
    }

    private void ShowHandSetActive()
    {
        handPresenceObject.SetActive(true);
    }
}
