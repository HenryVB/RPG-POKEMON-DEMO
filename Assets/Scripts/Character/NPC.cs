using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, Interactuable
{
    [SerializeField] private Dialog dialog;
    [SerializeField] List<Sprite> sprites;

    private SpriteAnimator animator;
    private void Start()
    {
        animator = new SpriteAnimator(GetComponent<SpriteRenderer>(), sprites);
        animator.Start(); 
    }

    private void Update()
    {
        animator.HandleUpdate();
    }

    public void Interact()
    {
        StartCoroutine(DialogManager.Instance.ShowDialog(dialog));
    }
}
