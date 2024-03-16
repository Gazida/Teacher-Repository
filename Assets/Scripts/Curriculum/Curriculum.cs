using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Curriculum", menuName = "Scriptable Objects/Curriculum")]
public class Curriculum : ScriptableObject
{
    [SerializeField]
    [Tooltip("M�fredattaki �nite Verileri")] public UnitesData[] unitesData;
}

// M�fredattaki �niteler
[Serializable]
public class UnitesData
{
    [Tooltip("M�fredattaki �nitelerin �simleri")] public string uniteName;
    [Tooltip("M�fredattaki Konu Ba�l�klar� Verileri")] public TopicsData[] topicsData; // M�fredattaki her �nitede 3 konu olacak
}
// M�fredattaki konular
[Serializable]
public class TopicsData
{
    [Tooltip("M�fredattaki Konular�n �simleri")] public string topicName;
    [Tooltip("M�fredattaki Paragraf Verileri")] public string[] paragraphs; // Her konu ba�l��� alt�nda 10 paragraf olacak
}