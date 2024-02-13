using System;
using System.Collections.Generic;
using FlaxEngine;
//using PhysicsFeaturesTour;

namespace Game;

/// <summary>
/// GateSimple Script. also used for breakable blocks, meaning trigger is the disabled target, 
/// unless different visuals applied (animations might fix)
/// </summary>
public class GateSimple : Script
{
    public Actor gateTarget;

    public CollisionSample button;
    /// <inheritdoc/>
    public override void OnStart()
    {
        // Here you can add code that needs to be called when script is created, just before the first game update
    }
    
    /// <inheritdoc/>
    public override void OnEnable()
    {
        // Here you can add code that needs to be called when script is enabled (eg. register for events)
        button.triggered += OnButtonPressed;
    }

    /// <inheritdoc/>
    public override void OnDisable()
    {
        // Here you can add code that needs to be called when script is disabled (eg. unregister from events)
        button.triggered -= OnButtonPressed;
    }

    private void OnButtonPressed()
    {
        //disable object
        if(gateTarget){
            gateTarget.IsActive = false;
        }	
        //add animation here, that changes door mat, or breaks block()
        //or add seperate script for animation
    }



}
