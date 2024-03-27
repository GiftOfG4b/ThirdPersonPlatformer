using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game;

/// <summary>
/// CrumbleBlock Script.
/// </summary>
public class CrumbleBlock : Script
{

    public Actor[] pieces; 
    //unlike gate, when collides, create pieces/seperate pieces in direction of hit
    /// <inheritdoc/>
    public override void OnStart()
    {
        // Here you can add code that needs to be called when script is created, just before the first game update
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

    void OnHit(Vector3 position, Vector3 direction){

        foreach (var item in pieces)
        {
            RigidBody rigidBodyItem = (RigidBody) item;

            rigidBodyItem.AddForceAtPosition(direction,position,ForceMode.Impulse);

            //after few seconds, delete from game
        }

        //using an async method, have each block follow a parabolic curve without using rb

    }
}
