using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game;

/// <summary>
/// ChangeColorEvent Script.
/// </summary>
public class ChangeColorEvent : Script
{

    private float timeout;

    public StaticModel ModelToControl;

    public MaterialBase MaterialOn;
    public MaterialBase MaterialOff;

    public CollisionSample button;

    /// <inheritdoc/>
    public override void OnStart()
    {
        timeout = 0;
    }
    
    /// <inheritdoc/>
    public override void OnEnable()
    {
        // Here you can add code that needs to be called when script is enabled (eg. register for events)
        button.triggered += OnEventStart;
    }

    /// <inheritdoc/>
    public override void OnDisable()
    {
        // Here you can add code that needs to be called when script is disabled (eg. unregister from events)
        button.triggered -= OnEventStart;
    }

    private void OnEventStart()
    {
        timeout = 0.5f;
        
    }


    /// <inheritdoc />
    public override void OnUpdate()
    {
        //todo: cleanup
        bool isOn = false;
        if (timeout > 0)
        {
            timeout -= Time.UnscaledDeltaTime;
            isOn = timeout > 0;
        }

        if (ModelToControl)
            ModelToControl.SetMaterial(0, isOn ? MaterialOn : MaterialOff);
    }
}
