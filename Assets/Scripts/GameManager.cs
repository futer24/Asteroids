using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
 

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;              //Static instance of 

    public UIManager uiManager { get; private set; }
    public SpawnController spawnManager { get; private set; }
    public SoundManager soundManager { get; private set; }

    [SerializeField] private GameDataScriptableObject gameData;
    [SerializeField] private GameEvent _clearEnemiesEvent = default;
    [SerializeField] private GameEvent _gameOverEvent = default;
    [SerializeField] private GameEvent _playerSpawnEvent = default;
    [SerializeField] private GameEventInt _waveSpawnEvent = default;
    

    public enum GameStates
    {
        LOADING = 0,
        MAIN_MENU,
        WAVE_PREPARE,
        PLAYING,
        GAMEOVER

    }

    private GameManager.GameStates previousState = GameManager.GameStates.LOADING;
    private GameManager.GameStates currentState = GameManager.GameStates.LOADING;

    public event Action<GameDataScriptableObject> OnGameDataUpdate;

    //Awake is always called before any Start functions
    void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);


        uiManager = GetComponentInChildren<UIManager>();
        spawnManager = GetComponentInChildren<SpawnController>();
        soundManager = GetComponentInChildren<SoundManager>();

    }

     // Start is called before the first frame update
    void Start()
    {
        OnStateChange(GameManager.GameStates.MAIN_MENU);
    }



    public void OnStateChange(GameManager.GameStates newState)
    {
        if (newState == currentState)
            return;
 
        switch (newState)
        {
            case GameManager.GameStates.LOADING:
                break;
            case GameManager.GameStates.MAIN_MENU:
                _waveSpawnEvent.RaiseEvent(0); //Spawn the decorative asteroids for menu
                uiManager.SetActivePanel(UIManager.GamePanel.STARTPANEL);
                break;
            case GameManager.GameStates.WAVE_PREPARE:
                _clearEnemiesEvent.RaiseEvent(); //   spawnManager.ClearEnemies();
                OnGameDataUpdate?.Invoke(gameData);
                uiManager.SetActivePanel(UIManager.GamePanel.WAVEPANEL);
                StartCoroutine(Utils.DelayedAction( ()=>
                {
                    OnStateChange(GameManager.GameStates.PLAYING);
                }
                ));
                break;
            case GameManager.GameStates.PLAYING:
                _waveSpawnEvent.RaiseEvent(gameData.Wave);
                _playerSpawnEvent.RaiseEvent();
                uiManager.SetActivePanel(UIManager.GamePanel.PLAYINGPANEL);
                break;
            case GameManager.GameStates.GAMEOVER:
                _clearEnemiesEvent.RaiseEvent();  
                OnGameDataUpdate?.Invoke(gameData);
                uiManager.SetActivePanel(UIManager.GamePanel.GAMEOVERPANEL);
                _gameOverEvent.RaiseEvent();
                StartCoroutine(Utils.DelayedAction(() =>
                {
                    OnStateChange(GameManager.GameStates.MAIN_MENU);
                }
                ));
                break;
        }

        previousState = currentState;
        currentState = newState;
    }


    private void Update()
    {
      
        if (currentState != GameManager.GameStates.MAIN_MENU) return;
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            OnStateChange(GameManager.GameStates.WAVE_PREPARE);
        }

    }

    public void CheckLives()
    {
        gameData.Lives--;
        OnGameDataUpdate?.Invoke(gameData);
        if (gameData.Lives==0)
        {
    
            StartCoroutine(Utils.DelayedAction(() =>
            {
                OnStateChange(GameManager.GameStates.GAMEOVER);
            }
           ));
           
        }
        else
        {
           
            StartCoroutine(Utils.DelayedAction(() =>
            {
                _playerSpawnEvent.RaiseEvent();
            }
            ));
            
        }

    }

    public void CheckWave()
    {
        gameData.Wave++;
        OnStateChange(GameStates.WAVE_PREPARE);
    }

    public void ResetModel()
    {
        gameData.InitValues();

    }

}
