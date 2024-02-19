using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game;

/// <summary>
/// MovingPlatform Script.
/// </summary>
public class MovingPlatform : Script
{
    Vector3[] waypoints;

    public float speed = 200f;
    
    float time;
    public float timeToReach=1;//also set as time to start, like for multiple
    public Actor platformActor;

    public Actor pathHolder;

    int prevWaypointIndex;
    public int targetWaypointIndex = 0;
    Vector3 targetWaypoint;
    public Vector3 prevWayPoint;

    /// <inheritdoc/>
    public override void OnStart()
    {   
        time = 0;
        
        
                
        waypoints = new Vector3[pathHolder.ChildrenCount];

        for (int i = 0; i< waypoints.Length; i++){
            waypoints[i] = pathHolder.GetChild(i).Position;
        }
        
        
        prevWayPoint = platformActor.Position;
        
        
        targetWaypoint = waypoints[targetWaypointIndex];
        //timeToReach=1;
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
        FollowPath();
    }


    void FollowPath() {
        //transform.position = waypoints [0];

        
        //transform.LookAt(targetWaypoint);
        
            
        if (platformActor.Position == targetWaypoint){
            prevWayPoint = targetWaypoint;
            targetWaypointIndex = (targetWaypointIndex + 1) % waypoints.Length;
            targetWaypoint = waypoints[targetWaypointIndex];
            
            timeToReach = (targetWaypoint-prevWayPoint).Length/speed;

            time = 0;
        }
        
        else{
            
            time += Time.DeltaTime;//speed
            
            //Debug.Log(time/timeToReach);
            platformActor.Position = Vector3.Lerp(prevWayPoint, targetWaypoint, Math.Min(1,time/timeToReach));//Mathf.SmoothStep(0f,1f,t)
            
        }        
        
    }


}
