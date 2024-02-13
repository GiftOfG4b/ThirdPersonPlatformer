using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game;

/// <summary>
/// PathUI Script.
/// </summary>
public class PathUI : Script
{
    Vector3[] waypoints;
    public Actor pathHolder;

    SpriteRender capsuleSprite;//render several along path

    //make long 2d rectangle, that rotates around its long axis to face camera
    //have the capsule strite on it, set to repeating mode
    SpriteRender circleSprite;//render at end points
    

    /// <inheritdoc/>
    public override void OnStart()
    {
        waypoints = new Vector3[pathHolder.ChildrenCount];

        for (int i = 0; i< waypoints.Length; i++){
            waypoints[i] = pathHolder.GetChild(i).Position;

            SpriteRender wayPointCircle = circleSprite;//or new sprite render? tbd
            wayPointCircle.Transform = new Transform(waypoints[i],Quaternion.Identity);
            Level.SpawnActor(wayPointCircle);

        }

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
}
