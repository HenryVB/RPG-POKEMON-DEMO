using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { FreeRoam, Battle, Dialog, Cutscene, Paused }
public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private BattleSystem battleSystem;
    [SerializeField] private Camera worldCamera;


    private GameState state;
    private GameState stateBeforePause;

    //CAMBIO POR ADDITIVE SCENE
    private SceneDetails currentScene;
    private SceneDetails prevScene;

    public static GameManager Instance { get; private set; }
    
    //CAMBIO POR ADDITIVE SCENE
    public SceneDetails CurrentScene { get => currentScene; set => currentScene = value; }
    public SceneDetails PrevScene { get => prevScene; set => prevScene = value; }

    private void Awake()
    {
        Instance = this;
        //ConditionsDB.Init();
    }

    private void Start()
    {
        battleSystem.onBattleOver += EndBattle;


        DialogManager.Instance.OnShowDialog += () =>
        {
            state = GameState.Dialog;
        };

        DialogManager.Instance.OnCloseDialog += () =>
        {
            if (state == GameState.Dialog)
                state = GameState.FreeRoam;
        };
    }

    public void PauseGame(bool pause)
    {
        if (pause)
        {
            stateBeforePause = state;
            state = GameState.Paused;
        }
        else
        {
            state = stateBeforePause;
        }
    }

    public void StartBattle()
    {
        state = GameState.Battle;
        battleSystem.gameObject.SetActive(true);
        worldCamera.gameObject.SetActive(false);

        var playerParty = playerController.GetComponent<PokemonParty>();
        
        //Se cambia para que tome el componente actual en escena en lugar que busque y obtenga aleatoriamente
        var wildPokemon = CurrentScene.GetComponent<MapArea>().getRandomWildPokemon();
        //var wildPokemon = FindObjectOfType<MapArea>().GetComponent<MapArea>().getRandomWildPokemon();

        //var wildPokemonCopy = new Pokemon(wildPokemon.Base, wildPokemon.Level);

        battleSystem.StartBattle(playerParty, wildPokemon);
    }

    /*
    TrainerController trainer;
    public void StartTrainerBattle(TrainerController trainer)
    {
        state = GameState.Battle;
        battleSystem.gameObject.SetActive(true);
        worldCamera.gameObject.SetActive(false);

        this.trainer = trainer;
        var playerParty = playerController.GetComponent<PokemonParty>();
        var trainerParty = trainer.GetComponent<PokemonParty>();

        battleSystem.StartTrainerBattle(playerParty, trainerParty);
    }

    public void OnEnterTrainersView(TrainerController trainer)
    {
        state = GameState.Cutscene;
        StartCoroutine(trainer.TriggerTrainerBattle(playerController));
    }
    */

    void EndBattle(bool won)
    {
        /*
        if (trainer != null && won == true)
        {
            trainer.BattleLost();
            trainer = null;
        }*/

        state = GameState.FreeRoam;
        battleSystem.gameObject.SetActive(false);
        worldCamera.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (state == GameState.FreeRoam)
        {
            playerController.HandleUpdate();
        }
        else if (state == GameState.Battle)
        {
            battleSystem.HandleUpdate();
        }
        else if (state == GameState.Dialog)
        {
            DialogManager.Instance.HandleUpdate();
        }
    }

    internal void SetCurrentScene(SceneDetails currentScene)
    {
        PrevScene = CurrentScene;
        CurrentScene = currentScene;
    }
}
