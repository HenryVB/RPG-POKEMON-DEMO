using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [SerializeField] private GameObject dialogBox;
    [SerializeField] private Text dialogText;
    [SerializeField] private float lettersPerSecond;

    private static DialogManager instance;

    public static DialogManager Instance { get => instance; set => instance = value; }

    public event Action OnShowDialog;
    public event Action OnCloseDialog;

    private Dialog dialog;
    private int currentLine = 0;
    private bool isTyping;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    public IEnumerator ShowDialog(Dialog dialog) {
        yield return new WaitForEndOfFrame();

        OnShowDialog?.Invoke();
        
        this.dialog = dialog;
        dialogBox.SetActive(true);
        StartCoroutine(TypeDialog(dialog.Lines[0]));
    }

    public IEnumerator TypeDialog(string line)
    {
        isTyping = true;
        dialogText.text = "";
        foreach (var letter in line.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
        isTyping = false; 
    }

    internal void HandleUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !isTyping)
        {
            currentLine++;
            if (currentLine < dialog.Lines.Count)
                StartCoroutine(TypeDialog(dialog.Lines[currentLine]));
            else
            {
                currentLine = 0;
                dialogBox.SetActive(false);
                OnCloseDialog?.Invoke();    
            } 
                
                
        }
    }
}