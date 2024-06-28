using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryItem : MonoBehaviour,IPlayerTriggerable
{
    // Start is called before the first frame update
    [SerializeField] private Dialog dialog;

    public void OnPlayerTriggered(PlayerController player)
    {
        player.Character.Animator.IsMoving = false;
        StartCoroutine(DialogManager.Instance.ShowDialog(dialog));
    }

    public bool IsTriggerRepeat => false;
}
