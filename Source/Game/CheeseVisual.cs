using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FlaxEngine;

namespace Game;

/// <summary>
/// CheeseVisual Script.
/// </summary>
public class CheeseVisual : Script
{
    //public SceneAnimationPlayer cheeseHealthPlayer; //flashing red or twirling stars above

    //public Material cheeseMat;

    public MaterialBase MaterialNorm;
    public MaterialBase MaterialHurt;

    public Texture cheeseTexture; //hurt unhurt etc all on one texture, simply decal

    public StaticModel cheeseModel;

    Color hurtColor;

    Color normalColor;


    bool isHurt = false;
    

    /// <inheritdoc/>
    public override void OnStart()
    {
        // Here you can add code that needs to be called when script is created, just before the first game update
        //cheeseMat = (Material) cheeseModel.GetMaterial(0);//material base to material
        
    }
    
    /// <inheritdoc/>
    public override void OnEnable()
    {
        // Here you can add code that needs to be called when script is enabled (eg. register for events)
        isHurt = false;
    }

    /// <inheritdoc/>
    public override void OnDisable()
    {
        // Here you can add code that needs to be called when script is disabled (eg. unregister from events)
        isHurt = false;
    }


    public enum EyePosition { normal, hurt, dead}
    public EyePosition eyeState;
    void ChangeEyeOffset(EyePosition pos)
    {

    }

    public void Damaged(){
        isHurt = true;
        FlashColor();
    }

    public void Cured(){
        isHurt = false;
    }

    async Task FlashColor(){
        bool isHurtColor = false;
        float maxTime = 5f;
        float startTime = Time.GameTime;
        while (isHurt && (Time.GameTime<startTime+maxTime)){
            //cheeseModel.SetMaterial(0, cheeseMat);
            if (isHurtColor){
                //cheeseMat.SetParameterValue("Color", normalColor);
                cheeseModel.SetMaterial(0, MaterialNorm);
            }
            else{
                //cheeseMat.SetParameterValue("Color", hurtColor);
                cheeseModel.SetMaterial(0, MaterialHurt);
            }
            isHurtColor = !isHurtColor;
            await Task.Delay(125);//miliseconds waited until the next check
        }
        //cheeseMat.SetParameterValue("Color", normalColor);   
        cheeseModel.SetMaterial(0, MaterialNorm);
    }
}
