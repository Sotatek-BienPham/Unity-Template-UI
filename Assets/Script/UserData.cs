using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class UserData : MonoBehaviour
{
    public static UserData Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    public int bigRewardRequire = 5;
    public int questsFinished = 0;
    public ListDailyQuest listDailyQuest;
    public List<QuestProgress> listQuestInProgress;
    public List<QuestProgress> ListQuestFinished;
    public Action<QuestProgress> onQuestFinish;
    // public Action<QuestProgress> onCreateNewQuest;

    private void Start()
    {
        onQuestFinish = OnFinishQuest;
        listQuestInProgress = new();
        ListQuestFinished = new();
        GenerateQuest();
    }
    /// <summary>
    /// Gọi GenerateQuest mỗi khi reset quest hằng ngày.
    /// </summary>
    public void GenerateQuest()
    {
        foreach (var item in listDailyQuest.listQuestGroupData)
        {
            QuestProgress newQuestProgress = new(item.listQuest[0]);
            listQuestInProgress.Add(newQuestProgress);
        }
        foreach (var item in listDailyQuest.listSingleQuestData)
        {
            QuestProgress newQuestProgress = new(item);
            listQuestInProgress.Add(newQuestProgress);
        }
    }
    public void OnFinishQuest(QuestProgress finishedQuest)
    {

        ListQuestFinished.Add(finishedQuest);
        listQuestInProgress.Remove(listQuestInProgress.Find(x => x.GetQuestData().questID == finishedQuest.GetQuestData().questID));
        questsFinished = ListQuestFinished.Count;
        foreach (var group in listDailyQuest.listQuestGroupData)
        {
            if (group.QuestGroupID == finishedQuest.GetQuestData().questGroupID)
            {
                var quest = group.listQuest.Find(x => x.questID == finishedQuest.GetQuestData().questID);
                string[] nameComponent = quest.questID.Split("_");
                string nextQuestName = nameComponent[0] + "_" + nameComponent[1] + "_" + (Convert.ToInt16(nameComponent[2]) + 1).ToString();
                var nextQuest = group.listQuest.Find(x => x.questID == nextQuestName);
                if (nextQuest != null)
                {
                    QuestProgress newQuestProgress = new(nextQuest);
                    listQuestInProgress.Add(newQuestProgress);
                    // onCreateNewQuest?.Invoke(newQuestProgress);
                }
                // Debug.LogError("Next quest not found: " + nextQuestName);
            }
        }
    }
}
