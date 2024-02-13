using FlaxEngine;
using System;


public class CollisionSample : Script
{//placed on models where if hit theres an event

    public delegate void Triggered();
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
        //timeout = 0.5f;
        if(triggered!=null){
            triggered();
        }
        
        
    }

    
}

