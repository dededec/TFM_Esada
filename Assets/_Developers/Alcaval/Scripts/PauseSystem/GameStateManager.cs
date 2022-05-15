using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager
{
    private static GameStateManager _instance;
    public static GameStateManager instance
    {
        get 
        {
            if(_instance == null)
                _instance = new GameStateManager();
            return _instance;
        }
    }

    public GameState CurrentGameState { get; private set; }

    public delegate void GameStateChangeHandler(GameState newGameState);
    public event GameStateChangeHandler onGameStateChanged;

    private GameStateManager()
    {

    }

    public void SetState(GameState newGameState)
    {
        if(newGameState == CurrentGameState)
            return;
        CurrentGameState = newGameState;
        onGameStateChanged?.Invoke(newGameState); 
    }

    #region Example of use

    // private void Awake() {
    //     GameStateManager.instance.onGameStateChanged += onGameStateChanged;
    // }

    // private void OnDestroy() {
    //     GameStateManager.instance.onGameStateChanged -= onGameStateChanged;
    // }

    // private void onGameStateChanged(GameState newGameState)
    // {
    //     enabled = newGameState == GameState.Gameplay;
    // }
        
    #endregion

}