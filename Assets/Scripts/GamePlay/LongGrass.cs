using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class LongGrass : MonoBehaviour, IPlayerTriggerable
{
    public void OnPlayerTriggered(PlayerController player)
    {
        if (UnityEngine.Random.Range(1, 101) <= 10)
        {
            //CAMBIO POR ADDITIVE SCENE
            //Para que no se bugee al momento de caminar y detectar un obj triggerable
            player.Character.Animator.IsMoving = false;
            GameManager.Instance.StartBattle();
        }
    }
}
