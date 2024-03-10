using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Curriculum", menuName = "Scriptable Objects/Curriculum")]
public class Curriculum : ScriptableObject
{
    public string[] UniteNames = new string[8];
    public string[] TopicNames = new string[24];

    [Space(10)]

    [SerializeField]
    [Tooltip("M�fredattaki �nite Verileri")] public UnitesData[] unitesData = new UnitesData[8];
}

// M�fredattaki �niteler
[Serializable]
public class UnitesData
{
    [Tooltip("M�fredattaki Konu Ba�l�klar� Verileri")] public TopicsData[] topicsData = new TopicsData[3]; // M�fredattaki her �nitede 3 konu olacak
}
// M�fredattaki konular
[Serializable]
public class TopicsData
{
    [Tooltip("M�fredattaki Paragraf Verileri")] public string[] paragraphs = new string[30]; // Her konu ba�l��� alt�nda 10 paragraf olacak
}