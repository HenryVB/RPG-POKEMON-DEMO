using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    [SerializeField] private Text nameText;
    [SerializeField] private Text countText;

    private RectTransform rectTransform;
    private void Awake()
    {
        //para scrollear dinamico con los valores del rectransform del scroll
        rectTransform = GetComponent<RectTransform>();
    }

    //Altura del RectTransform
    public float Height => rectTransform.rect.height;

    public Text NameText { get => nameText; set => nameText = value; }
    public Text CountText { get => countText; set => countText = value; }

    public void SetData(ItemSlot itemSlot)
    {
        NameText.text = itemSlot.Item.Name;
        CountText.text = $"X {itemSlot.Count}";
    }
}
