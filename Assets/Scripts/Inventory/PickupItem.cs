using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour, Interactuable
{
    [SerializeField] private ItemBase item;

    private bool used;

    public bool Used { get => used; set => used = value; }

    public IEnumerator Interact(Transform initiator)
    {
        if (!Used)
        {
            //agregarlo al inventario
            initiator.GetComponent<Inventory>().AddItem(item);

            Used = true;

            //Desactivas colision y sprite ya que si se destruye se pierde la info
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;

            string playerName = initiator.GetComponent<PlayerController>().Name;

            yield return DialogManager.Instance.ShowDialogText($"{playerName} found {item.Name}");
        }
    }
}
