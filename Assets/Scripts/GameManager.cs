using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameObject player;
    public enum GameStates{TitleScreen, Paused, InGame };
    public GameStates currentState = GameStates.TitleScreen;

    public delegate void GameStartHandler();
    public event GameStartHandler GameStarted;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
