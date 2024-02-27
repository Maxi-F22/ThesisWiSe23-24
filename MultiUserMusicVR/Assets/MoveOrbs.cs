using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.XR.CoreUtils;
using UnityEngine.XR.Interaction.Toolkit;

public class MoveOrbs : MonoBehaviour
{
    // list of controllers for haptic feedback
    private List<ActionBasedController> xrControllers = new List<ActionBasedController>();

    // hand that is grabbing the cylinder
    private GameObject hand;

    // bool to determine if cylinder is grabbed
    private bool moving = false;
    void Start()
    {
        // begin playing the sound at beginning of application
        gameObject.GetComponent<AudioSource>().Play();
    }
    // is called if the cylinder is gripped
    public void StartMoveOrb(SelectEnterEventArgs eventArgs)
    {
        moving = true;
        // save grabbing hand in variable
        hand = eventArgs.interactorObject.transform.gameObject;
    }

    // is called if cylinder is released
    public void StopMoveOrb()
    {
        moving = false;
    }

    void Update()
    {
        if (moving)
        {
            // add controllers to list
            foreach (ActionBasedController controller in (ActionBasedController[])GameObject.FindObjectsByType(typeof(ActionBasedController), FindObjectsSortMode.None))
            {
                if (!xrControllers.Contains(controller))
                {
                    xrControllers.Add(controller);
                }
            }

            // move cylinder according to X and Y coordinates of hand, Z stays the same
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, new Vector3(hand.transform.position.x, hand.transform.position.y, gameObject.transform.position.z), Time.deltaTime * 10);
            
            // iterate through list and get the grabbing object to play haptic feedback
            foreach (ActionBasedController ctr in xrControllers)
            {
                if (ctr.gameObject.GetNamedChild(hand.name))
                {
                    ctr.SendHapticImpulse(hand.transform.position.y/3.5f, 0.2f);
                }
            }
        }
        // change pitch and volume of AudioClip according to cylinder position
        AudioSource orbAudioSrc = gameObject.GetComponent<AudioSource>();
        orbAudioSrc.pitch = gameObject.transform.position.x / 5;
        orbAudioSrc.volume = gameObject.transform.position.y / 3.5f;
    }
}
