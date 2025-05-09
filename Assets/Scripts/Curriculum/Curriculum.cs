using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Curriculum", menuName = "Scriptable Objects/Curriculum")]
public class Curriculum : ScriptableObject
{
    [SerializeField]
    [Tooltip("Müfredattaki Ünite Verileri")] public UnitesData[] unitesData;
}

// Müfredattaki üniteler
[Serializable]
public class UnitesData
{
    [Tooltip("Müfredattaki Ünitelerin İsimleri")] public string uniteName;
    [Tooltip("Müfredattaki Konu Başlıkları Verileri")] public TopicsData[] topicsData; // Müfredattaki her ünitede 3 konu olacak
}
// Müfredattaki konular
[Serializable]
public class TopicsData
{
    [Tooltip("Müfredattaki Konuların İsimleri")] public string topicName;
    [Tooltip("Müfredattaki Paragraf Verileri")] public string[] paragraphs; // Her konu başlığı altında 10 paragraf olacak
}