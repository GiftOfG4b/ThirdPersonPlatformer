using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game;

/// <summary>
/// FadeToBlackMat Script.
/// </summary>
public class FadeToBlackMat : Script
{
    public Material fadeBlackMat;
    MaterialParameter radiusParameter;
    
    [Limit(0, 1)]
    public float parameterValue = 1f;
    /// <inheritdoc/>
    public override void OnStart()
    {
        // Here you can add code that needs to be called when script is created, just before the first game update
        radiusParameter = fadeBlackMat.GetParameter("Radius");
        if(radiusParameter!=null){
            //Debug.Log("Got Radius");
            radiusParameter.Value = parameterValue;
        }
    }
    
    
    /// <inheritdoc/>
    public override void OnUpdate()
    {
        // Here you can add code that needs to be called every frame
        //fadeBlackMat.SetParameterValue("Radius",parameterValue);
        radiusParameter.Value = parameterValue;
    }

    void EditRadius(float value){
        fadeBlackMat.SetParameterValue("Radius",value);
    }
}
