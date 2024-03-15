using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game;

/// <summary>
/// HardRangeHingeLimit Script.
/// </summary>
public class HardRangeHingeLimit : Script
{
    public float lowerLimit = -90f;
    public float upperLimit = 90f;
    public HingeJoint hingeJoint;
    /// <inheritdoc/>
    public override void OnStart()
    {
        // Here you can add code that needs to be called when script is created, just before the first game update
        if (hingeJoint != null)
        {
            hingeJoint.Limit = new LimitAngularRange(lowerLimit,upperLimit);
        }
    }
}
