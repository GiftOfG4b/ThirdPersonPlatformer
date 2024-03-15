using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game;

/// <summary>
/// MouseAI Script.
/// </summary>
public class MouseAI : Script, IDamageable
{
    public Actor platformActor;

    enum HealthState
    {
        Normal,
        Weakened,
        Dead
    }
    HealthState mouseState = HealthState.Normal;


    public float _speed = 250f;

    public float turnSpeed = 45;

    Vector3[] defaultWayPoints;
    public Actor defaultPathHolder;

    Vector3[] waypoints;
    Vector3[] tempPath;

    int targetWaypointIndex;
    Vector3 targetWaypoint;

    public Collider mouseCollider;

    public Vector3 gravityDir = Vector3.Down;

    public float recoveryTime = 5f;
    float timeLastHit;

    /// <inheritdoc/>
    public override void OnStart()
    {
        // Here you can add code that needs to be called when script is created, just before the first game update
        defaultWayPoints = new Vector3[defaultPathHolder.ChildrenCount];

        for (int i = 0; i< waypoints.Length; i++){
            defaultWayPoints[i] = defaultPathHolder.GetChild(i).Position;
        }
        waypoints = defaultWayPoints;
        targetWaypointIndex = 0;
        targetWaypoint = waypoints[targetWaypointIndex];
    }
    
    /// <inheritdoc />
    public override void OnEnable()
    {
        mouseCollider.CollisionEnter += OnCollisionEnter;
    }

    /// <inheritdoc />
    public override void OnDisable()
    {
        mouseCollider.CollisionEnter -= OnCollisionEnter;
    }

    /// <inheritdoc/>
    public override void OnUpdate()
    {
        // mouse behaviour here, simple state machine
        if (mouseState == HealthState.Normal)
        {
            FollowPath();
        }
        else if (mouseState == HealthState.Weakened)
        {
            //animation graph: fall, helmet break
            if(Time.GameTime<timeLastHit+recoveryTime){
                
            }
            else{
                //recover (get up anim)
                //check if path reachable to default path (by definition of path if any point reachable to default path then all points reachable)
                //otherwise pathfind
                var currentPos = this.Actor.Position;
                var targetPos = waypoints[0];
                if(!Navigation.FindPath(currentPos, targetPos, out tempPath)){//if cant find path, create new one where the mouse is
                    
                }

                else{
                    
                }
                
                mouseState = HealthState.Normal;
            }
        }
        else{
            //todo: death animation instead
            this.Actor.IsActive = false;
        }

    }

    void FollowPath() {

        //possible note: for future ref, if needs to bounce and slide
        //project point on navmesh for bounce 
        //slide, release navmesh when arriving at top of slide before sliding down    
        //at bottom, climb if too steep, otherwise pathfind around

        //transform.position = waypoints [0];

        
        //transform.LookAt(targetWaypoint);
        
            
        if (platformActor.Position == targetWaypoint){
            //prevWayPoint = targetWaypoint;
            targetWaypointIndex = (targetWaypointIndex + 1) % waypoints.Length;
            targetWaypoint = waypoints[targetWaypointIndex];
            
        }
        
        else{
            //Debug.Log(time/timeToReach);
            platformActor.Position = Vector3.MoveTowards(platformActor.Position,targetWaypoint, _speed * Time.DeltaTime);
            
        }

        //turn to face (set if not facing condition)
        TurnToFace(targetWaypoint);

    }

    void TurnToFace(Vector3 lookTarget) {
        // Vector3 dirToLookTarget = lookTarget - platformActor.Position;
        // float targetAngle = 90 - Mathf.Atan2 (dirToLookTarget.Z, dirToLookTarget.X) * Mathf.RadiansToDegrees;


        /*Vector3 targetDir = lookTarget - transform.position;
        float angleBetween = Vector3.Angle(transform.forward, targetDir);
        */

        //float angle = Mathf.MoveTowardsAngle (platformActor.Transform.eulerangle.y, targetAngle, turnSpeed * Time.DeltaTime);
        //transform.eulerAngles = Vector3.up * angle;      

        Quaternion quatTarget = platformActor.LookingAt(lookTarget);
        platformActor.Orientation = Quaternion.Lerp(platformActor.Orientation,quatTarget,Time.DeltaTime*turnSpeed);

        //other part, only height(x? component)

    }

    private void OnCollisionEnter(Collision collision)
    {
        //Vector3 collisionPoint = collision.Contacts[0].Point;
        //Vector3 collisionDir = collision.Contacts[0].Normal;
        
        CheeseHealth cheeseHit =collision.OtherActor.GetScript<CheeseHealth>();

        if(cheeseHit!=null){
            //hurt the cheese if not vertical hit, otherwise hurt mouse
            if(Vector3.Dot(collision.Contacts[0].Normal,gravityDir)>0.625f){
                cheeseHit.TakeHit(collision.Contacts[0].Normal, collision.Contacts[0].Point);
            }
            else{
                TakeHit(collision.Contacts[0].Normal, collision.Contacts[0].Point);
            }
        }
        
    }

    public void TakeHit(Vector3 damageDir, Vector3 damagePos){
        //when collider hit(true damage), call this function
        //bounce it a bit
        //if first, transition to stun state
        //if mid transition, no damage
        //if stun state, dead state
        if (mouseState == HealthState.Normal)
        {
            mouseState = HealthState.Weakened;
            timeLastHit = Time.GameTime;
        }
        else if (mouseState == HealthState.Weakened)
        {
            mouseState = HealthState.Dead;
        }

    }

    public void InstaDeath(){
        mouseState = HealthState.Dead;
        //death effect/event
    }
}
