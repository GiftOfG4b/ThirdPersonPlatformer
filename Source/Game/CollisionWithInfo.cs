using FlaxEngine;
using System;


public class CollisionWithInfo : Script
{//placed on models where if hit theres an event

    public delegate void Triggered(Collision collision);
    public event Triggered triggered;//subscribe actions to button press, like script for door to open

    Collider thisCollider; 
    /// <inheritdoc />
    public override void OnStart()
    {
        //timeout = 0;
        thisCollider = Actor.As<Collider>();
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
        // Vector3 collisionPoint = collision.Contacts[0].Point;
        // Vector3 collisionDir = collision.Contacts[0].Normal;
        //timeout = 0.5f;
        if(triggered!=null){
            triggered(collision);
        }
        
        
    }

    
}

