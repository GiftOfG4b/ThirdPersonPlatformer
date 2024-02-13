using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game;

/// <summary>
/// MovingPlatformPerCentage Script.
/// 
/// for start and end point, according to value t,
/// takes an intermediate point between
/// 
/// </summary>
public class MovingPlatformPerCentage : Script
{

    public float speed = 5f;

    private float percentage = 0f; // percentage of way
    public float Percentage   //gotten/set by other values
    {
        get { return percentage; }
        set { percentage = value; }
    }
    public float timeToReach=1;
    public Actor platformActor;
    public Actor pathHolder;

    Vector3[] waypoints;//only 2 long? start and end?
    Vector3 targetWaypoint;
    Vector3 prevWayPoint;

    /// <inheritdoc/>
    public override void OnStart()
    {
        // Here you can add code that needs to be called when script is created, just before the first game update
        waypoints = new Vector3[pathHolder.ChildrenCount];

        for (int i = 0; i< waypoints.Length; i++){
            waypoints[i] = pathHolder.GetChild(i).Position;
        }
        targetWaypoint = waypoints[1];
        prevWayPoint = waypoints[0];

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
    
        platformActor.Position = Vector3.Lerp(prevWayPoint, targetWaypoint, Math.Min(1,percentage/timeToReach));

    }
}
