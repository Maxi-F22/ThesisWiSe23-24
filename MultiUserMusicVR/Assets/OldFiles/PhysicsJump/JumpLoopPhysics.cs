using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// deprecated JumpLoop file when balls where operated by physics
public class JumpLoopPhysics : MonoBehaviour
{
    // variables to control the speed of the balls hopping
    public float gravityScale;
    public float verticalVelocityScale;

    // global Unity Gravity Scale
    public static float globalGravity = -9.81f;
    private Rigidbody rb;
    
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }
    
    private void OnCollisionEnter(Collision collision) 
    {
        // on collision with ground, set vertical velocity and play sound
        if (collision.gameObject.name == "Plane") 
        {
            rb.velocity = new Vector3(0, verticalVelocityScale, 0);
            gameObject.GetComponent<AudioSource>().Play();
        }
    }

    // set rigidbody gravity based on variable every frame
    void FixedUpdate ()
    {
        Vector3 gravity = globalGravity * gravityScale * Vector3.up;
        rb.AddForce(gravity, ForceMode.Acceleration);
    }
}
