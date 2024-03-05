using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game;

/// <summary>
/// ConveyorBelt Script. moves rigidbodies in the direction of its flow
/// </summary>
public class ConveyorBelt : Script
{
    public float speed = 200f;

    Vector3 rotationAxis;//coveyor belt's rotation axis, simple, so just one for whole thing, no extra rotation
    
    //touching force direction: cross product of conveyor surface normal and rotation axis


    public Collider thisCollider;
    Vector3 colliderBoxShape;
    Vector3 topConveyCenter;

    Vector3 conveyorNormal;

    Actor[] touchingBodies;

    //public Vector3 gravityDir = Vector3.Down;

    //Vector3[] contactNormal;
    RigidBody conveyorBelt;
    public Vector3 topConveyDir = Vector3.Left;
    Vector3 topConveyVelocity;
    Vector3 bottomConveyVelocity;

    
    int frame = 0; 
    int collisionCount = 0;
    float maxDistanceCheck = .125f;

    /// <inheritdoc/>
    public override void OnStart()
    {
        // Here you can add code that needs to be called when script is created, just before the first game update
        topConveyVelocity = topConveyDir*speed;
        conveyorBelt = (RigidBody)this.Actor;
        bottomConveyVelocity = -topConveyVelocity;

    }
    
    /// <inheritdoc />
    public override void OnEnable()
    {
        //thisCollider.CollisionEnter += OnCollisionEnter;
        
    }

    /// <inheritdoc />
    public override void OnDisable()
    {
        //thisCollider.CollisionEnter -= OnCollisionEnter;
    }

    /// <inheritdoc/>
    public override void OnUpdate()
    {
        // Here you can add code that needs to be called every frame

        //for each touching bodies, add force proportional to the bodie's velocity vs the conveyor belt's velocity
        // foreach (Vector3 normal in contactNormal)
        // {
        //     Vector3 conveyorVelocity = Vector3.Cross(rotationAxis,normal); 
        // }
        
        
        
        //(aka relative friction)
        //proportion term: relative to downward force times area touching(find how to get)
        //simpler way: dont consider how much touching, just (if) touching
        //decide if add force at point where touching, or just add force

        


    }

    public override void OnFixedUpdate(){
        //janky life hack from youtube:

        //change conveyor belt position each frame sligtly, 
        //using rigidbody.position
        //then change it back in the physics frame
        //using rigidbody.moveposition()
        //only the movepos will affect objects on it, pushing them forward

        //minimum distance/frameTime = conveySpeed

        //minimum distance = conveySpeed * frameTime;
        //rigidbody.position -= conveyVelocity * frameTime;
        //rigidbody.movePosition = conveyVelocity * frameTime;
        
        //in flax: doesnt work

        //check if changing actor pos would have same effect
        //check if addforce.addforce velocity change same effect as

        conveyorBelt.EnableSimulation = false;
        conveyorBelt.Position -= topConveyVelocity * Time.DeltaTime;
        conveyorBelt.EnableSimulation = true;
        conveyorBelt.AddMovement(topConveyVelocity* Time.DeltaTime); 

        // rotating parts
        //sim false
        //rotation = less
        //sim true
        //conveyorbelt.addrotation

        //conveyorBelt.LinearVelocity = (conveyVelocity);//* Time.DeltaTime
        //on actor class
        //change pos 
        //then addMovement
        // if(collisionCount>0){
        //     RayCastHit[] rayCastHits;
        //     Physics.BoxCastAll(topConveyCenter,colliderBoxShape,conveyorNormal, out rayCastHits, Quaternion.Default,maxDistanceCheck);
        //     Physics.SphereCastAll()
        // }
        
        
    }


    // private void OnCollisionEnter(Collision collision)
    // {
    //     //Vector3 collisionPoint = collision.Contacts[0].Point;
    //     //Vector3 collisionDir = collision.Contacts[0].Normal;
        
    //     //RigidBody collidedRigidBody = collision.OtherActor.AttachedRigidBody;
    //     //keep collision info


    //     // if(collidedRigidBody!=null){
            
    //     // }
    //     collisionCount=Math.Max(1,collisionCount++);
    // }

    // //to do: figure out collision stay : ray cast?


    // private void OnCollisionExit(Collision collision)
    // {
    //     //count bodies as not touching the conveyor belt anymore
    //     collisionCount--;
    // }

    
}
