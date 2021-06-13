using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public PlayerController player;
    public GameObject presentPref;
    public GameObject Pinata;
    public GameObject gameOverBanner;
    public float presentSpawnOffsetRange;
    public enum GameStates{TitleScreen, Paused, InGame };
    public GameStates currentState = GameStates.TitleScreen;

    public Vector3[] SpawnPointPositions;
    public Vector2 minSpawnRange = new Vector2(-8.3f, -4.3f);
    public Vector2 maxSpawnRange = new Vector2(8.3f, 3.3f);
    public float timeBetweenSpawns = 15f;
    public Text scoreDisplay;

    private float timeSinceSpawn = 0;
    private int score = 0;

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
        timeSinceSpawn += Time.deltaTime;
        if(timeSinceSpawn > timeBetweenSpawns)
        {
            SpawnPresent();
        }
    }

    //Function called when GameStarted Event is triggered
    void OnGameStart()
    {
       
    }

    public void PinataDied()
    {
        Debug.Log("Pinata Died");
        Instantiate(gameOverBanner, new Vector3(0.0f,0.0f,0.0f), Quaternion.identity);
    }

    private void SpawnPresent()
    {
        timeSinceSpawn = 0;
        //Use spawnpoints to create new minigame points
        float xOffset = Random.Range(3.0f, -3.0f);
        // Choose spawnpoint
        int randomSpawnIndex = (int)Mathf.Floor(Random.value * SpawnPointPositions.Length);
        Vector3 offset = new Vector3(Random.Range(presentSpawnOffsetRange, -presentSpawnOffsetRange), 0.0f, 0.0f);
        Vector3 randomSpawnPoint = new Vector3(Random.Range(minSpawnRange.x, maxSpawnRange.x), Random.Range(minSpawnRange.y, maxSpawnRange.y), 0);

        Instantiate(presentPref, randomSpawnPoint, Quaternion.identity);
    }

    public void PresentWrapped()
    {
        score++;
        scoreDisplay.text = score.ToString();
        SpawnPresent();
    }
}
