using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "Scriptable Objects/New Quest")]
public class Quest : ScriptableObject
{
    public string questTitle;
    public string questDescription;
    public string questType;

    public QuestList[] questList;
}

[Serializable]
public class QuestList
{
    public string quest;
    public bool isQuestCompleted;
}