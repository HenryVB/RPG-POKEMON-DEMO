using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { Start,ActionSelection,MoveSelection,RunningTurn, Busy,PartyScreen,BattleOver}
public enum BattleAction{ Move, SwitchPkmn, UseItem, Run}
public class BattleSystem : MonoBehaviour
{
    [SerializeField] private BattleUnit playerUnit;
    [SerializeField] private BattleUnit enemyUnit;
    [SerializeField] private BattleDialogBox dialogBox;

    private BattleState state;
    private int currentAction;
    private int currentMove;

    public event Action<bool,bool> onBattleOver;

    private PokemonParty playerParty;
    private Pokemon wildPokemon;

    private bool isTrainerBattle;
    private int escapeAttempts;

    [Header("Audio")]
    [SerializeField] private AudioClip wildBattleMusic;
    //[SerializeField] private AudioClip battleVictoryMusic;


    public void StartBattle(PokemonParty playerParty, Pokemon wildPokemon)
    {
        isTrainerBattle = false; //MIENTRAS NO SE IMPLEMENTA ESTARÁ EN FALSO
        escapeAttempts = 0;
        this.playerParty = playerParty;
        this.wildPokemon = wildPokemon;
        AudioManager.instance.PlayMusic(wildBattleMusic);
        StartCoroutine(SetupBattle());
    }

    public IEnumerator SetupBattle() {
        // ten en cuenta que esto ha cambiado CON PARAMETROS DE POR MEDIO #CAP 17
        playerUnit.Setup(playerParty.GetHealthyPokemon());
        enemyUnit.Setup(wildPokemon);

        dialogBox.SetMoveNames(playerUnit.Pokemon.Moves);

        yield return dialogBox.TypeDialog($"A wild {enemyUnit.Pokemon.Base.Name} appeared.");
        //yield return new WaitForSeconds(1f);

        ActionSelection();
    }

    /*
    private void ChooseFirstTurn()
    {
        if (playerUnit.Pokemon.Speed >= enemyUnit.Pokemon.Speed)
            ActionSelection();

        else
            StartCoroutine(EnemyMove());
    }*/

    private void ActionSelection()
    {
        state = BattleState.ActionSelection;
        dialogBox.SetDialog("Choose an Action");
        dialogBox.EnableActionSelector(true);
    }

    private void MoveSelection() {
        state = BattleState.MoveSelection;
        dialogBox.EnableActionSelector(false);
        dialogBox.EnableDialogText(false);
        dialogBox.EnableMoveSelector(true);
    }


    public void HandleUpdate()
    {
      if(state == BattleState.ActionSelection)
        {
            HandleActionSelection();
        }
      else if(state == BattleState.MoveSelection)
        {
            HandleMoveSelection();
        }
    }

    private void HandleActionSelection()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
                ++currentAction;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
                --currentAction;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
                currentAction -=2;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentAction += 2;
        }

        currentAction = Math.Clamp(currentAction, 0, 3);

        dialogBox.UpdateActionSelection(currentAction);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (currentAction == 0)
            {
                MoveSelection();
            }

            else if (currentAction == 1)
            {// BAG
            }

            else if (currentAction == 2)
            {// POKEMON TBD
            }

            else if (currentAction == 3)
            {// POKEMON RUN
               StartCoroutine(RunTurns(BattleAction.Run));
            }
        }
    }

    private void HandleMoveSelection()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ++currentMove;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            --currentMove;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentMove -= 2;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentMove += 2;
        }

        currentMove = Math.Clamp(currentMove, 0, playerUnit.Pokemon.Moves.Count - 1);

        dialogBox.UpdateMoveSelection(currentMove, playerUnit.Pokemon.Moves[currentMove]);

        if (Input.GetKeyDown(KeyCode.Z)) {
            dialogBox.EnableMoveSelector(false);
            dialogBox.EnableDialogText(true);
            //StartCoroutine(PlayerMove()); # cap 24
            StartCoroutine(RunTurns(BattleAction.Move));
        }

        else if (Input.GetKeyDown(KeyCode.X))
        {
            dialogBox.EnableMoveSelector(false);
            dialogBox.EnableDialogText(true);
            ActionSelection(); //regresa a las acciones
        }

    }

    IEnumerator RunTurns(BattleAction playerAction)
    {
        state = BattleState.RunningTurn;

        if (playerAction == BattleAction.Move)
        {
            playerUnit.Pokemon.CurrentMove = playerUnit.Pokemon.Moves[currentMove];
            enemyUnit.Pokemon.CurrentMove = enemyUnit.Pokemon.DoRandomMove();

            //who goes fist
            bool playerGoesFirst = playerUnit.Pokemon.Speed >= enemyUnit.Pokemon.Speed;

            var firstUnit = (playerGoesFirst) ? playerUnit : enemyUnit;
            var secondUnit = (playerGoesFirst) ? enemyUnit : playerUnit;

            var secondPokemon = secondUnit.Pokemon;

            //first turn
            yield return RunMove(firstUnit, secondUnit, firstUnit.Pokemon.CurrentMove);
            //yield return RunAfterTurn #cap 24
            if (state == BattleState.BattleOver) yield break; //termina la batalla

            if (secondPokemon.HP > 0)
            {
                //second turn
                yield return RunMove(secondUnit, firstUnit, secondUnit.Pokemon.CurrentMove);
                //yield return RunAfterTurn #cap 24
                if (state == BattleState.BattleOver) yield break; //termina la batalla
            }

        }

        else {
            //Cambio de pokemon, uso de items y otras
            if (playerAction == BattleAction.Run)
                yield return TryToEscape();
        }
        
        if(state != BattleState.BattleOver)
            ActionSelection();
    }

    /* CAP 24 SE DEJA DE USAR
    IEnumerator PlayerMove()
    {
        state = BattleState.RunningTurn;

        var move = playerUnit.Pokemon.Moves[currentMove];
        yield return RunMove(playerUnit, enemyUnit, move);

        //if the battle stat was not changed by RunMove, the go to the next step
        if (state == BattleState.RunningTurn)
        StartCoroutine(EnemyMove());
        
    }
    

    IEnumerator EnemyMove() { 
        state = BattleState.RunningTurn;
        
        var move = enemyUnit.Pokemon.DoRandomMove();
        yield return RunMove(enemyUnit,playerUnit,move);
        //if the battle stat was not changed by RunMove, the go to the next step
        if (state == BattleState.RunningTurn)
            ActionSelection();
    }
    */

    IEnumerator RunMove(BattleUnit sourceUnit, BattleUnit targetUnit, Move move) {
        move.PP--;
        yield return dialogBox.TypeDialog($"{sourceUnit.Pokemon.Base.name} used {move.Base.name}");
        yield return new WaitForSeconds(1f);

        sourceUnit.PlayAttackAnimation(); //atk animacion
        yield return new WaitForSeconds(1f);

        targetUnit.PlayHitAnimation();
        AudioManager.instance.PlaySfx(AudioId.Hit);

        var damageDetails = targetUnit.Pokemon.TakeDamage(move, sourceUnit.Pokemon);
        yield return targetUnit.Hud.UpdateHP(targetUnit.Pokemon);
        yield return ShowDamageDetails(damageDetails);

        if (damageDetails.Fainted)
        {
            yield return dialogBox.TypeDialog($"{targetUnit.Pokemon.Base.name} fainted");
            targetUnit.PlayFaintAnimation();

            yield return new WaitForSeconds(2f);

            //logica para cambiar de pokemon #14 pero no entra en alcance

            CheckForBattleOver(targetUnit);

        }

    }


    private void CheckForBattleOver(BattleUnit faintedUnit)
    {
        if (faintedUnit.IsPLayerUnit)
        {
            //LOGICA PARA SACAR OTRO POKE PERO AHORA NO VA
            BattleOver(false);//revisar si es true o false y viceversa
        }

        else
        {
            //AudioManager.instance.PlayMusic(battleVictoryMusic); //Cancion de victoria
            BattleOver(true);//revisar si es true o false y viceversa
        }
    }

    IEnumerator ShowDamageDetails(DamageDetails dmgDetails)
    {
        if(dmgDetails.Critical > 1f)
            yield return dialogBox.TypeDialog("A critical Hit");

        if(dmgDetails.TypeEfectiveness > 1f)
            yield return dialogBox.TypeDialog("It's super effective");

        if (dmgDetails.TypeEfectiveness < 1f)
            yield return dialogBox.TypeDialog("It's not very effective");

    }

    private void BattleOver(bool isPlayerWinner = false, bool isRunning = false) { 
       state = BattleState.BattleOver;
       onBattleOver(isPlayerWinner,isRunning);
    }

    IEnumerator TryToEscape() {
        state = BattleState.Busy;

        if (isTrainerBattle)
        {
            //Logica que no puede huir por estar en una batalla con entrenador
        }

        ++escapeAttempts;

        int playerSpeed = playerUnit.Pokemon.Speed;
        int enemySpeed = enemyUnit.Pokemon.Speed;

        if (enemySpeed <= playerSpeed) {
            yield return dialogBox.TypeDialog($"Run away safely");
            BattleOver(true,true);
        }

        else
        {
            float f = (playerSpeed * 128) / enemySpeed + 30 * escapeAttempts;
            f = f % 256;

            if (UnityEngine.Random.Range(0, 256) < f) {
                yield return dialogBox.TypeDialog($"Run away safely");
                BattleOver(true,true);
            }

            else
            {
                yield return dialogBox.TypeDialog($"Can't Escape");
                state = BattleState.RunningTurn;
                BattleOver(true, true);
            }
        }
    }
}
