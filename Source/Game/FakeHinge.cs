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
{// rename as double hinge?
    public RigidBody hingeRigidBody;
    public CollisionWithInfo[] colliders;

    //public Vector3 rotationAxis;

    //float angle;
    public Actor[] insideActors;//child objects of the double hinge, that keep their roation despite the parents rotation
    Quaternion[] insideActRot;


    /// <inheritdoc/>
    public override void OnStart()
    {
        // Here you can add code that needs to be called when script is created, just before the first game update
        //hingeRigidBody 
        insideActRot = new Quaternion[insideActors.Length];
        int i = 0;
        foreach (var insideActor in insideActors){
            insideActRot[i] = insideActor.Orientation;
            i++;
        }
        
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
    public override void OnFixedUpdate()
    {
        // Rotate inner hinges to keep same orientation (always vertical) // seems preformance intense
        for (int i = 0; i < insideActRot.Length; i++){
            insideActors[i].Orientation = insideActRot[i];
        }
    }


    private void OnHingeHit(Collision collision)
    {

        Vector3 collisionPoint = collision.Contacts[0].Point;
        Vector3 collisionDir = collision.Contacts[0].Normal;
        //take mass of colliding body into account for force magnitude
        if( collision.OtherActor.AttachedRigidBody){
            float otherRbMass = collision.OtherActor.AttachedRigidBody.Mass;
            
            if (hingeRigidBody){
                hingeRigidBody.AddForceAtPosition(-otherRbMass*collisionDir,collisionPoint);            
            }
        }
        
    }
}
