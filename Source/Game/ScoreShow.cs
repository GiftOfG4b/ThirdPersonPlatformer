using System;
using System.Collections.Generic;
using FlaxEngine;
using FlaxEngine.GUI;

namespace Game;

/// <summary>
/// ScoreShow Script. 
/// </summary>
public class ScoreShow : Script
{
    [Tooltip("The current level max score")]
    public int totalScore{ get; set; }
    int score = 0;//player score/ number of stars found

    [Tooltip("Reference to the player score visual control")]
    public UIControl controlVisual { get; set; }

    RichTextBox scoreRender;

    //public delegate void Increase(int amount); //normally followed by static Increase increase;
    public static Action<int> increase;//delegate with no return type and int parameter


    /// <inheritdoc/>
    public override void OnStart()
    {
        // Here you can add code that needs to be called when script is created, just before the first game update
        Debug.Log( controlVisual.Control);
        scoreRender = (RichTextBox) controlVisual.Control;
    }
    
    /// <inheritdoc/>
    public override void OnEnable()
    {
        // Here you can add code that needs to be called when script is enabled (eg. register for events)
        //hook up to score change events?
        //score events needs to be global 
        increase += ScoreChange;
    }

    /// <inheritdoc/>
    public override void OnDisable()
    {
        // Here you can add code that needs to be called when script is disabled (eg. unregister from events)
        //hook up to score change events
        increase -= ScoreChange;
    }

    void ScoreChange(int pickupAmount){
        score += pickupAmount;
        scoreRender.Text = ": "+ score.ToString();//?how to do
    }
}
