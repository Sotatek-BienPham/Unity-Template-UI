using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "List Daily Quest",menuName = "Scriptable Object/List Daily Quest")]
public class ListDailyQuest : ScriptableObject
{
    public RewardType bigRewardType = RewardType.Unknown;
    public int bigRewardAmount = 300;
    public List<QuestGroupData> listQuestGroupData;
    public List<QuestData> listSingleQuestData;
}