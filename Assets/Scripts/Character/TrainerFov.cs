using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class TrainerFov : MonoBehaviour, IPlayerTriggerable
{
    public bool IsTriggerRepeat => false;

    public void OnPlayerTriggered(PlayerController player)
    {
        //Cambio por ADDITIVE SCENE
        //Para que no se bugee al momento de caminar y detectar un obj triggerable
        //character.Animator.IsMoving = false;
        //GameController.Instance.OnEnterTrainersView(GetComponentInParent<TrainerController>());
    }
}
