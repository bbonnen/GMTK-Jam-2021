using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public PlayerController player;
    public GameObject Pinata;
    public enum GameStates{TitleScreen, Paused, InGame };
    public GameStates currentState = GameStates.TitleScreen;

    public delegate void GameStartHandler();
    public event GameStartHandler GameStarted;

    // Start is called before the first frame update
    void Awake()
    {
        //Example of how to register Event Listenter
        //GameManager.Instance.GameStarted += OnGameStart;
        GameStarted += OnGameStart;
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        if (Pinata == null)
            Pinata = GameObject.FindGameObjectWithTag("Pinata");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Function called when GameStarted Event is triggered
    void OnGameStart()
    {
        
    }
}
