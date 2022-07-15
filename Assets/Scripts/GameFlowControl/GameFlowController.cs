using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameFlowController : MonoBehaviour
{
    public enum GameState
    {
        PreGame,
        Playing,
        Paused,
        Die,
        Finished
    }
    [SerializeField]
    int midGame, BackToFirstLlvl, BackToDashLvl, GotoBooslvl, GoToNeckAilvl;
    public bool debug;

    public int levelDebug = 1;
    private InGameMenuController _menuController;
    private PlayerInput _playerInput;

    public delegate void SwitchActionDelegate(string map);
    public  SwitchActionDelegate OnSwitch;
    public static GameFlowController Instance { get; private set; }

    public InGameMenuController MenuController
    {
        get
        {
            if (_menuController == null) _menuController = FindObjectOfType<InGameMenuController>();

            return _menuController;
        }
    }

    public GameState CurrentState { get; set; }

    public int NivelActual { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(this);
            Instance = this;
        }
    }

    private void Start()
    {
        if (debug) { 
            CurrentState = GameState.Playing;
            NivelActual = levelDebug;
            if(NivelActual == 7)
            {
                StartMusicBoosNekAi();
            }
        }
        else {
            CurrentState = GameState.PreGame; 
            
        }
        ChangeFPSTarget(60);
        Cursor.lockState = CursorLockMode.Locked;//Oculta el cursor
    }

    private void OnEnable()
    {
        _playerInput = FindObjectOfType<PlayerInput>();
    }


    public void StartGame()
    {
        CurrentState = GameState.Playing;
        Debug.Log("Playing" + CurrentState + " _" + CurrentState);
        Time.timeScale = 1.0f;
        NivelActual = 1;
        SceneLoader.Instance.LoadLevel(1);
        
        _menuController = FindObjectOfType<InGameMenuController>();

    }

    public void StartMusicGame()
    {
        try { AudioManager.PlayMusic("through space"); } catch (Exception e) { Debug.Log("musica"); }
    }
    public void StartMusicBoosLasers()
    {
        try { AudioManager.PlayMusic("laser_fight"); } catch (Exception e) { Debug.Log("musica"); }
    }
    public void StartMusicBoosBulletHell()
    {
        try { AudioManager.PlayMusic("bullet_hell"); } catch (Exception e) { Debug.Log("musica"); }
    }
    public void StartMusicBoosNekAi()
    {
        try { AudioManager.PlayMusic("nekai_theme"); } catch (Exception e) { Debug.Log("musica"); }
    }

    public void reiniciarUltimoLVL()
    {
        Debug.Log(NivelActual);
        SceneLoader.Instance.LoadLevel(NivelActual);
    }

    public void PasarLVL()
    {
        NivelActual += 1;
        SceneLoader.Instance.LoadLevel(NivelActual);
        _menuController = FindObjectOfType<InGameMenuController>();
        if(NivelActual != GoToNeckAilvl + 1)
        {
            StartMusicGame();
        }
        else
        {
            //StartMusicBoosNekAi();
        }
        
    } 
    public void GoToMidGame()
    {
        GoToSpecificScene(midGame);
    }
    public void BackToFistEvent()
    {
        GoToSpecificScene(BackToFirstLlvl);
    }
    public void BackToDash()
    {
        GoToSpecificScene(BackToDashLvl);
    }
    public void GoToBoos()
    {
        GoToSpecificScene(GotoBooslvl);
    } 
    public void GoToNekAi()
    {
        GoToSpecificScene(GoToNeckAilvl);
    }
    private void GoToSpecificScene( int lvl)
    {
        NivelActual = lvl;
        SceneLoader.Instance.LoadLevel(NivelActual);
        _menuController = FindObjectOfType<InGameMenuController>();
        if(lvl == GoToNeckAilvl)
        {
            StartMusicBoosNekAi();
        }
        else
        {
            StartMusicGame();
        }
        
    }

    public void returnIntro()
    {
        CurrentState = GameState.PreGame;
        Debug.Log("PreGame" + CurrentState + " _" + CurrentState);
        
        NivelActual = 0;
        SceneLoader.Instance.LoadLevel(NivelActual);
    }

    public void PauseGame()
    {
       
        if (CurrentState != GameState.Paused && CurrentState != GameState.Die)
        {
            //_playerInput.SwitchCurrentActionMap("MenuActions");
            
            

            CurrentState = GameState.Paused;
            Debug.Log("Pause" + CurrentState);
            MenuController.SetPaused();
            Time.timeScale = 0.0f;
            OnSwitch?.Invoke("MenuActions");
        }
        else
        {
           // _playerInput.SwitchCurrentActionMap("InGameActions");
           
            CurrentState = GameState.Playing;
            Debug.Log("Playing" + CurrentState);
            MenuController.UnSetPaused();
            Time.timeScale = 1.0f;
            OnSwitch?.Invoke("InGameActions");
        }
    }

    public void Die()
    {
        AudioManager.PlaySFX("GameOver");
        CurrentState = GameState.Die;
        Debug.Log("Die" + CurrentState);
        Time.timeScale = 0.0f;
        SceneLoader.Instance.LoadLevel(5);

        Time.timeScale = 1.0f;
    }

    public void FinishGame()
    {
        Application.Quit();
    }

    public void ChangeFPSTarget(int fps)
    {
        Application.targetFrameRate = fps;
    }
    public int GetFpsTarget()
    {
        return Application.targetFrameRate;
    }

    public void OpenSettingsGame()
    {
        MenuController.OpenSettings();
    }

    public void ExitSettingsGame()
    {
        
        if (CurrentState != GameState.PreGame)
        {
            PauseGame();
        }
        else
        {
            MenuController.ExitSettings();
        }
        

    }

    private void Update()
    {

    }
}