using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game;

/// <summary>
/// PercentageController Script.
/// moves platform according to how much hinge is rotated
/// </summary>
public class PercentageController : Script
{
    Actor hingeRotated;
    public float maxAngle;
    public float angle;
    public float gearRatio;//percentage = angle*gearRatio

    float percentage;



    //real question: add force to one == add force to other
    //door wants to fall back due to gravity, hinge wants to roll back into place

    //horizontal gear difference
    //set both friction (vs speed rotation)
    //and force that tries to bring back to original place
    
    //pushing against platform ,adds force to gear in relation to gearRatio


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

    /// <inheritdoc/>
    public override void OnUpdate()
    {
        // Here you can add code that needs to be called every frame
    }
}
