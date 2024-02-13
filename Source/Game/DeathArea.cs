using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game;

/// <summary>
/// DeathArea Script. for lava or great height, if enter, insta death
/// </summary>
public class DeathArea : Script
{
    public Collider thisTrigger; 
    /// <inheritdoc/>
    public override void OnStart()
    {
        // Here you can add code that needs to be called when script is created, just before the first game update
    }
    
    /// <inheritdoc/>
    public override void OnEnable()
    {
         // Register for event
        thisTrigger.TriggerEnter += OnTriggerEnter;
        //thisTrigger.TriggerExit += OnTriggerExit;
    }

    /// <inheritdoc/>
    public override void OnDisable()
    {
        // Unregister for event
        thisTrigger.TriggerEnter -= OnTriggerEnter;
        //thisTrigger.TriggerExit -= OnTriggerExit;
    }

    void OnTriggerEnter(PhysicsColliderActor collider)
    {
        //Debug.Log("fell to die");
        //Debug.Log("entered zone");
        IDamageable damagedEntity = collider.Parent.GetScript<IDamageable>();//collider.GetScript<IDamageable>();
        //hitting the collider bellow the actor, problem
        //Debug.Log("test type: " + collider.GetType());
        
        //Debug.Log("get script: " + collider.Parent.GetScript<IDamageable>());
        //say insta dead
        if(damagedEntity!=null){
            //Debug.Log("got damage script");
            damagedEntity.InstaDeath();
        }
    }
}
