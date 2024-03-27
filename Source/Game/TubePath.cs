using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game;

/// <summary>
/// TubePath Script.
/// </summary>
public class TubePath : Script
{

    //two trigger zones; keep both, plus center point of each; for simplicity of not making child object script, just use actor pos as center
    public Collider entranceTrigger; 
    Vector3 entranceCenter;
    public Collider exitTrigger; 
    Vector3 exitCenter;

    HashSet<RigidBody> rigidBodiesEnt = new HashSet<RigidBody>(); 
    HashSet<RigidBody> rigidBodiesExt = new HashSet<RigidBody>(); 


    /// <inheritdoc/>
    public override void OnStart()
    {
        // Here you can add code that needs to be called when script is created, just before the first game update
        entranceCenter = entranceTrigger.Position;
        exitCenter = exitTrigger.Position;
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

        if(rigidBodiesEnt.Count>0){

            foreach (var body in rigidBodiesEnt){
                Vector3 outwdMotion = body.LinearVelocity-Vector3.Project(body.LinearVelocity,body.Position-entranceCenter);
                float inRightDir = Vector3.Dot(body.LinearVelocity,body.Position-entranceCenter);
                if(outwdMotion.LengthSquared<0.4f && inRightDir>0f){//if going towards the entrance enough, pull into entrance

                    //add force towards entrance center

                    if((body.Position-entranceCenter).LengthSquared<0.25f){//if close to center, pull in
                        //pull in: async event
                    }
                }

            }

        }

        if(rigidBodiesExt.Count>0){

            foreach (var body in rigidBodiesExt)
            {
                
            }

        }



    }

    void MoveThroughTube(RigidBody body){//async method, while not at end, push through tube
        int startIndex = 0;
        //like path follow: nodes to reach, when at node go to next one, when at end, stop

    }

    void OnTriggerEnter(PhysicsColliderActor collider)
    {
        //keep list of actors with rigidbodies that have entered this zone (gravity range)
        RigidBody enteredRb = collider.AttachedRigidBody;
        if (enteredRb)
        {
            rigidBodiesEnt.Add(enteredRb);
        }

    }

    void OnTriggerExit(PhysicsColliderActor collider)
    {
        //remove specified actor from list of actors with rigidbodies that have entered this zone (gravity range)
        RigidBody enteredRb = collider.AttachedRigidBody;
        if (enteredRb)
        {
            rigidBodiesEnt.Remove(enteredRb);
        }
    }

}
