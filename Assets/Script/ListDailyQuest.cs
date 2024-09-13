using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "List Daily Quest",menuName = "Scriptable Object/List Daily Quest")]
public class ListDailyQuest : ScriptableObject
{
    public List<QuestGroupData> listQuestGroupData;
    public List<QuestData> listSingleQuestData;
}