using System;
using System.Collections.Generic;
using FlaxEngine;


namespace Game;

/// <summary>
/// GameManager Script.
/// </summary>
/// should it be static? no different manager per scene for different screens
public class GameManager : Script
{
    //on death event, put restart screen

    //todo: restart at last checkpoint
    //can restart scene, but get new moon position; and already picked up items
    //store extra data before restart, that persists
    //mark data as saveable

    //if press restart button, restart game,

    //if press exit, exit game

    
    public SceneReference AnotherScene;

    public CheeseHealth cheeseHealth;

    public SceneAnimationPlayer uiAnimControl; 
    //public SceneAnimation blackFade;

    public Actor GameOverScreen;
    public PauseScreenUI pauseScreenUI;
    /// <inheritdoc/>
    public override void OnStart()
    {
        // Here you can add code that needs to be called when script is created, just before the first game update
    }
    
    /// <inheritdoc/>
    public override void OnEnable()
    {
        // Here you can add code that needs to be called when script is enabled (eg. register for events)
        cheeseHealth.cheeseDeath += GameOver;
    }

    /// <inheritdoc/>
    public override void OnDisable()
    {
        // Here you can add code that needs to be called when script is disabled (eg. unregister from events)
        cheeseHealth.cheeseDeath -= GameOver;
    }

    /// <inheritdoc/>
    public override void OnUpdate()
    {
        if (Input.GetKeyUp(KeyboardKeys.Escape)){
            Engine.RequestExit();}
        if (Input.GetKeyUp(KeyboardKeys.Backspace)){
            //Restart();
            GameOver();
        }
    }

    public void Quit(){
        Engine.RequestExit();
        //show pause screen
    }
    void Pause(){
        Time.TimeScale = 0;
        Screen.CursorVisible = true;
        Screen.CursorLock = CursorLockMode.None;
        //show pause screen
    }

    private void GameOver(){
        //freeze game
        //ui animation first,
        //uiAnimControl.Animation = blackFade;
        //activate game over screen (actor)
        //await end screen
        
        //uiAnimControl.Animation = blackFade;
        uiAnimControl.Play();
        GameOverScreen.IsActive = true;
        
        //circle whipe shader, controlled by scene animation
        //Restart();//restart func called by animation
    }
    public void Restart(){

        // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Level.ChangeSceneAsync(Level.Scenes[0].ID);//();
        

    }
}
