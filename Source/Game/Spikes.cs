using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game;

/// <summary>
/// Spikes Script.
/// </summary>
public class Spikes : Script
{
    public Collider thisCollider; //breaking my own convention, this time no seperate collider class, just straight up the collider info

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

    
}
