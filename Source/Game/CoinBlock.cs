using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FlaxEngine;

namespace Game;

/// <summary>
/// CoinBlock Script. on hit, visual (screen/block shake & star comes out above) and score (add to score) 
/// </summary>
public class CoinBlock : Script
{
    public StaticModel CoinBlockModel;
    public MaterialBase MaterialNormal;
    public MaterialBase MaterialHit; 
    public MaterialBase MaterialOff; 

    private Task _task;
    //public int maxValue = 5;
    public float timeToShutDown = 5;
    public float timeToChangeColor = .5f;
    bool canBeHit = true;
    bool firstHit = true;

    public CollisionWithInfo blockCollider;

    public ParticleEffect starParticles;
    public float starParticleMag = 800f;

    /// <inheritdoc/>
    public override void OnStart()
    {
        CoinBlockModel.SetMaterial(0,MaterialNormal);
    }
    
    /// <inheritdoc/>
    public override void OnEnable()
    {
        // Here you can add code that needs to be called when script is enabled (eg. register for events)
        blockCollider.triggered += OnBlockHit;
    }

    /// <inheritdoc/>
    public override void OnDisable()
    {
        // Here you can add code that needs to be called when script is disabled (eg. unregister from events)
        blockCollider.triggered -= OnBlockHit;
    }

    private void OnBlockHit(Collision collision)
    {
        //Vector3 collisionPoint = collision.Contacts[0].Point;
        Vector3 collisionDir = collision.Contacts[0].Normal;
        //Debug.Log("canBeHit: " + canBeHit);
        if(canBeHit){
            //animate exterior
            //particle effect (or in animation)
            //Debug.Log("hitInzone");
            //score change
            ScoreShow.increase?.Invoke(1);

            //debug: change color on hit
            ChangeColor();
            //start countdown until cant be hit
            //once countdown ended no more score increase or effects
            if(firstHit){
                firstHit = false; 
                ShutDown();
            }
            //starParticles.LookAt(starParticles.Position+direction);
            
            //Debug.Log(starParticles.Parameters[0].Name);
            starParticles.Parameters[0].Value = -collisionDir*800f;
            starParticles.Play();
            
            //square glow scale up effect (particle)

        }
    }

    async Task ShutDown(){
        
        float startTime = Time.GameTime;
        
        // while (Time.GameTime<startTime+timeToShutDown){
        //     await Task.Yield();
        // }
        await Task.Delay((int) timeToShutDown*1000);
        canBeHit = false;


        //debug
        //change color on shutdown
        CoinBlockModel.SetMaterial(0,MaterialOff);


        
    }

    async Task ChangeColor(){
        //todo: figure out how that works
        //Scripting.InvokeOnUpdate(() => );
        CoinBlockModel.SetMaterial(0,MaterialHit);
        float startTime = Time.GameTime;
        
        while (Time.GameTime<startTime+timeToChangeColor){
            await Task.Yield();
        }

        //bug, canbehit was here too


        //debug
        //change color on shutdown
        if(canBeHit)//dumb solution to err: cut off color task despite being midway, otherwise it will changes its color back
            Scripting.InvokeOnUpdate(() => CoinBlockModel.SetMaterial(0,MaterialNormal));
            //CoinBlockModel.SetMaterial(0,MaterialNormal);
    }
}
