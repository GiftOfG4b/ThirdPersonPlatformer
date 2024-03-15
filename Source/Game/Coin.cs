using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game;

/// <summary>
/// Coin Script.
/// </summary>
public class Coin : Script
{
    int value = 1; //each coin == 1

    Actor coinTarget;
    public TriggerSample triggerArea;

    //pro: if each coin hooked up directly to a score class, could easily make a coin total

    //otherwise, find a way to get invocation list.length


    /// <inheritdoc/>
    public override void OnStart()
    {
        // Here you can add code that needs to be called when script is created, just before the first game update
        coinTarget = Actor;//placed on the coin itself, not on area entered 

        //todo: in total score; add method that can be called at start by all coins in scene, that adds their existence to the total possible
    }
    
    /// <inheritdoc/>
    public override void OnEnable()
    {
        triggerArea.triggered += OnEntered;
        // Here you can add code that needs to be called when script is enabled (eg. register for events)
    }

    /// <inheritdoc/>
    public override void OnDisable()
    {
        triggerArea.triggered -= OnEntered;
        // Here you can add code that needs to be called when script is disabled (eg. unregister from events)
    }

    void OnEntered()
    {//todo: make sure only character can trigger pickup
        //when area entered, activate this method, this method calls the score bus static class, which changes the score show class
        //staticscoreclass.changescore(int+)
        //delete self


        ScoreShow.increase?.Invoke(value);

        if (coinTarget){
            coinTarget.IsActive = false;
        }


    }


}
