using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class HandPresencePhysics : MonoBehaviour
{
    // assign target controller to control hand physics via Editor
    public Transform target;

    // Rigidbody component of targeted hand 
    private Rigidbody rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // is called every frame in a fixed frame-rate (independent from in-game frame-rate), used to calculate physics
    void FixedUpdate()
    {
        // makes non-kinetic rigidbody follow the position of the controller
        rb.velocity = (target.position - transform.position) / Time.fixedDeltaTime;

        // calculates the rotation difference of the controller and the rigidbody
        Quaternion rotationDifference = target.rotation * Quaternion.Inverse(transform.rotation);
        rotationDifference.ToAngleAxis(out float angleInDegree, out Vector3 rotationAxis);

        Vector3 rotationDifferenceInDegree = angleInDegree * rotationAxis;

        // applies the rotation of the controller to the rigidbody
        rb.angularVelocity = rotationDifferenceInDegree * Mathf.Deg2Rad / Time.fixedDeltaTime;
    }
}
