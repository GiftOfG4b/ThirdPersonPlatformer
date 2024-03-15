using System;
using System.Collections.Generic;
using System.Threading;
using FlaxEngine;

namespace Game;

/// <summary>
/// MovingPlatformOnHit Script.
/// </summary>
public class MovingPlatformOnHit : Script
{
    Vector3[] waypoints;

    public float speed = 5f;
    float time;
    public float timeToReach=1;

    public float momentumTime = 5f;

    public float countDownSinceLastHit;
    public Actor platformActor;

    public Actor pathHolder;

    int prevWaypointIndex;
    int targetWaypointIndex;
    Vector3 targetWaypoint;
    Vector3 prevWayPoint;

    public Collider thisCollider;

    /// <inheritdoc/>
    public override void OnStart()
    {   
        time = 0;
        
        targetWaypointIndex = 0;
        

        // Here you can add code that needs to be called when script is created, just before the first game update
        
        waypoints = new Vector3[pathHolder.ChildrenCount];

        for (int i = 0; i< waypoints.Length; i++){
            waypoints[i] = pathHolder.GetChild(i).Position;
        }
        prevWayPoint = platformActor.Position;
        targetWaypoint = waypoints[targetWaypointIndex];
        platformActor.Position = targetWaypoint;

        //keep time to reach for each waypoint interval in array
        //assumes waypoints dont loop


    }
    
    /// <inheritdoc />
    public override void OnEnable()
    {
        thisCollider.CollisionEnter += OnCollisionHit;
    }

    /// <inheritdoc />
    public override void OnDisable()
    {
        thisCollider.CollisionEnter -= OnCollisionHit;
    }

    //only move to end if hit, otherwise come back to start
    //doesnt loop 
    //number of points beyond 2? tbd

    //onHit(){countDownTime=value;}//length of time untile stops moving

    /// <inheritdoc/>
    public override void OnUpdate()
    {
        if (countDownSinceLastHit>0){
            
            if(platformActor.Position != waypoints[^1])//if not at last waypoint
                FollowPathFWD();


            countDownSinceLastHit -=Time.DeltaTime;
        }
        else{
            
            if(platformActor.Position != waypoints[0])//if not at last waypoint
                FollowPathBckWD();
            //if at start pos
            //dont move
            //else
            //return to start pos
        }
    }


    /// <inheritdoc/>
    public void OnCollisionHit(Collision collision)
    {//on collider hit
        //if countdownSinceLastTriggered>0
        //if hit by cheese
        countDownSinceLastHit = momentumTime;
        //add event for clock animation (goes up to max before countdown from momentum time)
    }


    void FollowPathFWD() {
        //transform.position = waypoints [0];


        //as a force; keep direction from point to point,
        //convert force to accel to velocity, to pos change

        //linearly: just add or subtract to t
        
        //transform.LookAt(targetWaypoint);
            
        if (platformActor.Position == targetWaypoint){
            prevWayPoint = targetWaypoint;
            targetWaypointIndex ++;//doesnt loop back, so no modulo
            targetWaypoint = waypoints[targetWaypointIndex];
            
            timeToReach = (targetWaypoint-prevWayPoint).Length/speed;

            time = 0;
        }
        
        else{
            
            time += Time.DeltaTime;// *speed/length
            
            //Debug.Log(time/timeToReach);
            platformActor.Position = Vector3.Lerp(prevWayPoint, targetWaypoint, Math.Min(1,time/timeToReach));//Mathf.SmoothStep(0f,1f,t)
            
        }
               
    }

    void FollowPathBckWD() {
        //transform.position = waypoints [0];


        //as a force; keep direction from point to point,
        //convert force to accel to velocity, to pos change

        //linearly: just add or subtract to t
        
        //transform.LookAt(targetWaypoint);
            
        

        if (platformActor.Position == prevWayPoint){
            targetWaypoint = prevWayPoint;
            targetWaypointIndex --;//doesnt loop back, so no modulo
            prevWayPoint =  waypoints[targetWaypointIndex];
            
            
            timeToReach = (targetWaypoint-prevWayPoint).Length/speed;
 
            time = timeToReach;
        }
        
        else{
            
            time -= Time.DeltaTime;// *speed/length
            
            //Debug.Log(time/timeToReach);
            platformActor.Position = Vector3.Lerp(prevWayPoint, targetWaypoint, Math.Max(0,time/timeToReach));//Mathf.SmoothStep(0f,1f,t)
            
        }

        
        
    }

}
