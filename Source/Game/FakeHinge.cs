using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game;

/// <summary>
/// FakeHinge Script.
/// used when hinge within hinge, like a ferris wheel
/// 
/// </summary>
public class FakeHinge : Script
{
    RigidBody hingeRigidBody;
    public CollisionWithInfo[] colliders;

    public Vector3 rotationAxis;

    float angle;


    /// <inheritdoc/>
    public override void OnStart()
    {
        // Here you can add code that needs to be called when script is created, just before the first game update
    }
    
    /// <inheritdoc/>
    public override void OnEnable()
    {
        // Here you can add code that needs to be called when script is enabled (eg. register for events)
        foreach (var collisioner in colliders)
        {
            collisioner.triggered += OnHingeHit;
        }
    }

    /// <inheritdoc/>
    public override void OnDisable()
    {
        // Here you can add code that needs to be called when script is disabled (eg. unregister from events)
        foreach (var collisioner in colliders)
        {
            collisioner.triggered -= OnHingeHit;
        }
    }

    /// <inheritdoc/>
    public override void OnUpdate()
    {
        // Here you can add code that needs to be called every frame

        //twist hinge colliders to keep same orientation despite rotation
    }

    private void OnHingeHit(Collision collision)
    {

        Vector3 collisionPoint = collision.Contacts[0].Point;
        Vector3 collisionDir = collision.Contacts[0].Normal;

        hingeRigidBody.AddForceAtPosition(collisionDir,collisionPoint);
        
    }
}
