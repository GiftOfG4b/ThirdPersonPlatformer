using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game;

/// <summary>
/// SpringFollowTarget Script.
/// </summary>
public class SpringFollowTarget : Script
{
    //more like damped spring 

    /// <summary>
    /// Target actor to follow.
    /// </summary>
    public Actor TargetActor;

    public float vertSpeedMult = 3f;
    public float horzSpeedMult = 6f;

    public Vector3 upDir = Vector3.Up;

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


        
        Vector3 targetPosition = TargetActor.Position;
        Vector3 thisPos = Actor.Position;
        Vector3 deltaPos = targetPosition-thisPos;

        //Actor.Position = Vector3.Lerp(thisPos,targetPosition,vertSpeedMult*Time.DeltaTime);

        //lerp vertical and horizontal components seperately

        Vector3 vertDelta = Vector3.Project(deltaPos,upDir);
        Vector3 horizDelta = deltaPos-vertDelta;
        Actor.Position += horizDelta;//*horzSpeedMult*Time.DeltaTime
        Actor.Position += vertDelta*vertSpeedMult*Time.DeltaTime;
        //if too far from actor vertically, bring closer (gravDir), but dont affect in other directions
    }
}
