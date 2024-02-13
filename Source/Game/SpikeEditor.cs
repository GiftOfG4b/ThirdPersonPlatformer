using System;
using System.Collections.Generic;
using FlaxEngine;
using FlaxEditor;


namespace Game;

/// <summary>
/// SpikeEditor Script.
/// </summary>
public class SpikeEditor : Script
{
    //4 by 4 positions (or select rectangle)
    //

    [Tooltip("The generated foliage instances bounds (in actor local-space).")]
    public Vector2 Bounds = new Vector2(5000);

    BoundingBox boundingBox;

    Actor[] spikeModels;

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
    
    void Input(){

    }

    private void OnSceneGUI() {
        
    }

    void GenerateSpikesInBoundingBox(float spikeWidth, float spikeLength){
        //two spike choices only
        int spikeX = (int)(Bounds.X/spikeWidth);
        int spikeY = (int)(Bounds.Y/spikeLength);
        int max = spikeModels.Length;
        Random seedRandom = new Random();
        for (int i = 0; i < spikeX; i++)
        {
            for (int j = 0; j < spikeY; j++)
            {
                //rand num, 0 or 1

                int localModel = seedRandom.Next(0,max);
                //spawn those actors
                
                Model spike = new Model();//

            }
            
        }
    }

#if FLAX_EDITOR
    public override void OnDebugDrawSelected()
    {
        // Draw bounds area in editor
        var bounds = new BoundingBox(Vector3.Zero, new Vector3(Bounds.X, 500.0f, Bounds.Y));
        bounds = BoundingBox.Transform(bounds, Actor.LocalToWorldMatrix);
        DebugDraw.DrawBox(bounds, Color.AliceBlue);
    }
#endif
}
