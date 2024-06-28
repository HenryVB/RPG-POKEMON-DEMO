using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestList : MonoBehaviour
{
    private List<Quest> quests = new List<Quest>();

    public List<Quest> Quests { get => quests; set => quests = value; }

    public event Action OnUpdated;

    public void AddQuest(Quest quest)
    {
        if (!Quests.Contains(quest))
            Quests.Add(quest);

        OnUpdated?.Invoke();
    }

    public bool IsStarted(string questName)
    {
        var questStatus = Quests.FirstOrDefault(q => q.Base.Name == questName)?.Status;
        return questStatus == QuestStatus.Started || questStatus == QuestStatus.Completed;
    }

    public bool IsCompleted(string questName)
    {
        var questStatus = Quests.FirstOrDefault(q => q.Base.Name == questName)?.Status;
        return questStatus == QuestStatus.Completed;
    }

    public static QuestList GetQuestList()
    {
        return FindObjectOfType<PlayerController>().GetComponent<QuestList>();
    }
}
