using UnityEngine;
using System;
using System.Collections.Generic;
using Unity.Collections;

public class QuestManager:MonoBehaviour
{
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
        listQuestInProgress.Remove(listQuestInProgress.Find(x => x.QuestData.questID == finishedQuest.QuestData.questID));
        questsFinished = ListQuestFinished.Count;
        foreach (var group in listDailyQuest.listQuestGroupData)
        {
            if (group.QuestGroupID == finishedQuest.QuestData.questGroupID)
            {
                var quest = group.listQuest.Find(x => x.questID == finishedQuest.QuestData.questID);
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
