using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game;

/// <summary>
/// TriggerSample Script, events just for entering, not exiting
/// 
/// ex: coins or fell off map
/// </summary>
public class TriggerSample : Script
{
    public delegate void Triggered();
    public event Triggered triggered;

    Collider thisTrigger; 
    /// <inheritdoc/>
    public override void OnStart()
    {
        // Here you can add code that needs to be called when script is created, just before the first game update
        thisTrigger = Actor.As<Collider>();
    }
    
    /// <inheritdoc/>
    public override void OnEnable()
    {
         // Register for event
        thisTrigger.TriggerEnter += OnTriggerEnter;
        //thisTrigger.TriggerExit += OnTriggerExit;
    }

    /// <inheritdoc/>
    public override void OnDisable()
    {
        // Unregister for event
        thisTrigger.TriggerEnter -= OnTriggerEnter;
        //thisTrigger.TriggerExit -= OnTriggerExit;
    }

    void OnTriggerEnter(PhysicsColliderActor collider)
    {
        //throw triggered event (entered zone)
        if(triggered!=null){
            triggered();
        }
    }

    // void OnTriggerExit(PhysicsColliderActor collider)
    // {
    //     //throw triggered
    // }
}
