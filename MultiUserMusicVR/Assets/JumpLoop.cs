using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.XR.Interaction.Toolkit;
using System.Threading;

public class JumpLoop : MonoBehaviour
{
    // variable to define the tempo, with wich the ball should hop - e.g. "60" for 60 beats per minute
    public float beatTempo;

    // bool variables to determine beginning of application, grabbed or not grabbed
    private bool startUpdate = false;
    private bool isGrabbed = true;

    // Rigidbody component of ball
    private Rigidbody rb;

    // timer to synchronize hopping
    private float sync = 0;

    // list of controllers for haptic feedback
    private List<ActionBasedController> xrControllers = new List<ActionBasedController>();
    
    // on collision with ground, set velocity of rigidbody to zero
    private void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.name == "Plane") 
        {
            isGrabbed = false;
            rb.velocity = new Vector3(0, 0, 0);
        }
    }

    // is called when GameObject is picked up
    public void GrabObject()
    {
        startUpdate = true;
        rb = gameObject.GetComponent<Rigidbody>();
        // add controllers to list
        foreach (ActionBasedController controller in (ActionBasedController[])GameObject.FindObjectsByType(typeof(ActionBasedController), FindObjectsSortMode.None))
        {
            if (!xrControllers.Contains(controller))
            {
                xrControllers.Add(controller);
            }
        }
        isGrabbed = true;
        // remove Rigidbody constraints from Beginning
        rb.constraints = RigidbodyConstraints.None;
    }

    void Update()
    {
        // update timer via deltaTime
        sync += Time.deltaTime;
        if (!isGrabbed)
        {
            // move ball via cosinus function to look like hopping
            transform.position = new Vector3(transform.position.x, 
            0.5f + Mathf.Cos(Mathf.Lerp(2, 0, Mathf.PingPong(sync, 60.0f / beatTempo) / (60.0f / beatTempo))), 
            transform.position.z);

            // play sound each time the value is lower than the threshold
            if (Mathf.Cos(Mathf.Lerp(2, 0, Mathf.PingPong(sync, 60.0f / beatTempo) / (60.0f / beatTempo))) < -0.35f)
            {
                gameObject.GetComponent<AudioSource>().Play();
            }
        }
        else if (isGrabbed && startUpdate)
        {
            // play haptic feedback on controllers in list each time the value is lower than the threshold
            if (Mathf.Cos(Mathf.Lerp(2, 0, Mathf.PingPong(sync, 60.0f / beatTempo) / (60.0f / beatTempo))) < -0.35f)
            {
                foreach (ActionBasedController ctr in xrControllers)
                {
                    ctr.SendHapticImpulse(0.3f, 0.2f);
                }
            }
        }
        

    }
}
