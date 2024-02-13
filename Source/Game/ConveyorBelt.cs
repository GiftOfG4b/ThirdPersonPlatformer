using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game;

/// <summary>
/// ConveyorBelt Script. moves rigidbodies in the direction of its flow
/// </summary>
public class ConveyorBelt : Script
{
    float speed;
    public Collider thisCollider;

    Actor[] touchingBodies;

    public Vector3 gravityDir = Vector3.Down;

    /// <inheritdoc/>
    public override void OnStart()
    {
        // Here you can add code that needs to be called when script is created, just before the first game update
    }
    
    /// <inheritdoc />
    public override void OnEnable()
    {
        thisCollider.CollisionEnter += OnCollisionEnter;
    }

    /// <inheritdoc />
    public override void OnDisable()
    {
        thisCollider.CollisionEnter -= OnCollisionEnter;
    }

    /// <inheritdoc/>
    public override void OnUpdate()
    {
        // Here you can add code that needs to be called every frame

        //for each touching bodies, add force proportional to the bodie's velocity vs the conveyor belt's velocity
        //(aka relative friction)
        //proportion term: relative to downward force times area touching(find how to get)

    }

    private void OnCollisionEnter(Collision collision)
    {
        //Vector3 collisionPoint = collision.Contacts[0].Point;
        //Vector3 collisionDir = collision.Contacts[0].Normal;
        
        IDamageable damagedEntity =collision.OtherActor.Parent.GetScript<IDamageable>();//ugly with an e, should be cleaner at some point
        Debug.Log("get type: " + collision.OtherActor.Parent.GetScript<IDamageable>());
        if(damagedEntity!=null){
            //hurt the cheese
            //(or make seperate hurt class yada yada)
            //collision.OtherActor.//As<CheeseHealth>().OnHit(collision.Contacts[0].Normal);
            damagedEntity.TakeHit(collision.Contacts[0].Normal, collision.Contacts[0].Point);
        }
        
    }

    private void OnCollisionExit(Collision collision)
    {
        //count bodies as not touching the conveyor belt anymore
    }
}
