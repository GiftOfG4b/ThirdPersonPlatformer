using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game;

/// <summary>
/// TimerClock Script.
/// </summary>
public class TimerClock : Script
{
    public Actor clockHand;
    Quaternion startDir;

    Quaternion endDir;

    //float timeLastHit;

    public float impulseTime = 3.75f;

    float countDownSinceLastHit = 0;
    bool reachTop = false;

    float quickTimerUp = 0;
    public float quickTime = .25f;
    public Collider thisCollider;

    /// <inheritdoc/>
    public override void OnStart()
    {
        // Here you can add code that needs to be called when script is created, just before the first game update

        startDir = Quaternion.Euler(0,0,90f);//Quaternion.LookRotation(clockHand.Transform.Forward,clockHand.Transform.Right);
        endDir = Quaternion.Euler(0,0,-90f);//Quaternion.LookRotation(clockHand.Transform.Up,clockHand.Transform.Right);
    }
    
    /// <inheritdoc/>
    public override void OnEnable()
    {
        thisCollider.CollisionEnter += OnCollisionHit;
    }

    /// <inheritdoc/>
    public override void OnDisable()
    {
        thisCollider.CollisionEnter -= OnCollisionHit;
    }

    /// <inheritdoc/>
    public override void OnUpdate()
    {
        //could be put in async code, if desired
        if (countDownSinceLastHit>0){
            //Debug.Log("down");
            if(reachTop){
                
                clockHand.Orientation = Quaternion.Lerp(startDir,endDir,countDownSinceLastHit/impulseTime);
                countDownSinceLastHit -=Time.DeltaTime;
                
            }
            else{
                clockHand.Orientation = Quaternion.Lerp(clockHand.Orientation,endDir,quickTimerUp/quickTime);
                if(quickTimerUp >= quickTime){
                    reachTop = true;
                }
                else{
                    //Debug.Log(quickTimerUp);
                    quickTimerUp+=Time.DeltaTime;
                }
            }
        }
        
    }//or use animation, as originally planned...


    public void OnCollisionHit(Collision collision){//or make into (unspecified action) on hit to activate it?
        countDownSinceLastHit = impulseTime;
        reachTop = false;
        quickTimerUp = 0;
    }

}
