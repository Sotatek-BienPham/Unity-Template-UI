using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="QuestGroup_0",menuName ="Scriptable Object/Quest Group Data")]
public class QuestGroupData : ScriptableObject
{
    public string QuestGroupID;
    public List<QuestData> listQuest;
}
