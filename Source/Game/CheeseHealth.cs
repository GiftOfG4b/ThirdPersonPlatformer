using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FlaxEngine;

namespace Game;

/// <summary>
/// CheeseHealth Script. keeps track of health and changes graphic
/// </summary>
public class CheeseHealth : Script, IDamageable
{
    public CheeseVisual cheeseVisual;
    public float regenTime = 2f;//time while weakened untile comes back to normal

    //dead state, probably uneeded, just fall apart
    enum HealthState
    {
        Normal,
        Weakened,
        Dead
    }

    HealthState cheeseState = HealthState.Normal;

    //public event Action cheeseDeath;

    public delegate void Death();
    public event Death cheeseDeath;

    /// <inheritdoc/>
    public override void OnStart()
    {
        // Here you can add code that needs to be called when script is created, just before the first game update
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


    public void TakeHit(Vector3 hitDir, Vector3 hitPoint){
        //when collider hit(true damage), call this function
        //bounce it in hit direction(by bounce already)
        //if first, transition to weakened state
        //if weakened state, dead state
        if(cheeseState == HealthState.Normal){
            cheeseState = HealthState.Weakened;
            //play harm effect
            Task.Run(Regen);

        }
        else if(cheeseState == HealthState.Weakened)
        {
            cheeseState = HealthState.Dead;
            //play dead effect (squash and freeze?)
            //cheeseDeath.Invoke();
            cheeseDeath();
        }
        // else
        // {
        //     //log error, shouldnt get hit if dead
        //     Debug.Log("hit when dead, error");
        // }

    }

    public void InstaDeath(){
        Debug.Log("cheeseDeath");
        cheeseState = HealthState.Dead;
        //death event(for effect)
        //cheeseDeath.Invoke();
        cheeseDeath();

    }

    async Task Regen(){//todo: use animation instead
        
        float startTime = Time.GameTime;
        cheeseVisual.Damaged();

        //start hurt animation: change eyes to hurt and flash

        // while (Time.GameTime<startTime+regenTime){
        //     await Task.Yield();
        // }
        await Task.Delay((int)regenTime*1000);
        //if interupted by player dying, dont return to this state
        
        if (cheeseState != HealthState.Dead){
            cheeseState = HealthState.Normal;
        }
        cheeseVisual.Cured();
        


        //meanwhile

        //debug
        //change color on shutdown
        //CoinBlockModel.SetMaterial(0,MaterialOff);
        
    }
}
