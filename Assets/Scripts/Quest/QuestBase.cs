using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quests/Create a New Quest")]
public class QuestBase : ScriptableObject
{
    [SerializeField] private string name;
    [SerializeField] private string description;

    [SerializeField] private Dialog startDialogue;
    [SerializeField] private Dialog inProgressDialogue;
    [SerializeField] private Dialog completedDialogue;

    [SerializeField] private ItemBase requiredItem;
    [SerializeField] private ItemBase rewardItem;

    public string Name { get => name; set => name = value; }
    public string Description { get => description; set => description = value; }
    public Dialog StartDialogue { get => startDialogue; set => startDialogue = value; }
    public Dialog InProgressDialogue { get => inProgressDialogue?.Lines?.Count > 0 ? inProgressDialogue : startDialogue; set => inProgressDialogue = value; }
    public Dialog CompletedDialogue { get => completedDialogue; set => completedDialogue = value; }
    public ItemBase RequiredItem { get => requiredItem; set => requiredItem = value; }
    public ItemBase RewardItem { get => rewardItem; set => rewardItem = value; }
}
