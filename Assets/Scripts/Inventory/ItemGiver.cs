using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGiver : MonoBehaviour
{
    [SerializeField] private ItemBase item;
    [SerializeField] private int quantity = 1;
    [SerializeField] private Dialog dialog;

    bool used = false;

    public IEnumerator GiveItem(PlayerController player)
    {
        yield return DialogManager.Instance.ShowDialog(dialog);

        player.GetComponent<Inventory>().AddItem(item, quantity);

        used = true;

        //Dialogos dependiendo del # de items
        string dialogText = $"{player.Name} received {item.Name}";
        if (quantity > 1)
            dialogText = $"{player.Name} received {quantity} {item.Name}s";

        yield return DialogManager.Instance.ShowDialogText(dialogText);
    }

    public bool CanBeGiven()
    {
        return item != null && quantity > 0 && !used;
    }
}
