using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game;

/// <summary>
/// PlanetGravity Script.
/// 
/// to work: for all rigidbody, turn off gravity when needing these different gravity scripts
/// 
/// instead: set constand force in direction of gravity, as dictated by this script
/// 
/// 
/// </summary>
public class PlanetGravity : Script
{
    Vector3 planetCenter;
    HashSet<RigidBody> rigidBodies = new HashSet<RigidBody>(); 
    /// <inheritdoc/>
    public override void OnStart()
    {
        // Here you can add code that needs to be called when script is created, just before the first game update
        planetCenter = this.Actor.Position;
    }
    
    /// <inheritdoc/>
    public override void OnEnable()
    {
        // Here you can add code that needs to be called when script is enabled (eg. register for events)
    }

    /// <inheritdoc/>
    public override void OnDisable()
    {
        // Here you can add code that needs to be called when script is disabled (eg. unregister from events)
    }

    /// <inheritdoc/>
    public override void OnUpdate()
    {
        // Here you can add code that needs to be called every frame
    }

    void OnTriggerEnter(PhysicsColliderActor collider)
    {
        //keep list of actors with rigidbodies that have entered this zone (gravity range)
        RigidBody enteredRb = collider.AttachedRigidBody;
        if (enteredRb)
        {
            rigidBodies.Add(enteredRb);
        }

    }

    void OnTriggerExit(PhysicsColliderActor collider)
    {
        //remove specified actor from list of actors with rigidbodies that have entered this zone (gravity range)
        RigidBody enteredRb = collider.AttachedRigidBody;
        if (enteredRb)
        {
            rigidBodies.Remove(enteredRb);
        }
    }
}
