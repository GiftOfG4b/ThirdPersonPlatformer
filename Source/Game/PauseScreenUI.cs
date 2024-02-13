using System.Collections.Generic;
using FlaxEngine;
using FlaxEngine.GUI;

namespace Game;

/// <summary>
/// PauseScreenUI Script.
/// </summary>
public class PauseScreenUI : Script
{
    GameManager gameManager;
    //[Tooltip("The menu buttons.")]
    //public List<UIControl> Buttons;
    public UIControl QuitButton;
    public UIControl RestartButton;

    public override void OnStart()
    {
        // if (Buttons == null || Buttons.Count == 0)
        // {
        //     Debug.Log("No buttons");
        //     return;
        // }

        // foreach (var button in Buttons)
        // {
        //     button.Get<Button>().ButtonClicked += OnButtonClicked;
        // }
        if (QuitButton!=null)
        {
            QuitButton.Get<Button>().ButtonClicked += OnQuitButtonClicked;
        }
        if (RestartButton!=null)
        {
            RestartButton.Get<Button>().ButtonClicked += OnRestartButtonClicked;
        }

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

    private void OnButtonClicked(Button button)
    {
        Debug.Log("Clicked: " + button.Text);
    }

    private void OnRestartButtonClicked(Button button)
    {
        Debug.Log("Clicked: " + button.Text);

    }

    private void OnQuitButtonClicked(Button button)
    {
        Debug.Log("Clicked: " + button.Text);
        gameManager.Quit();
    }
    
}
