using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Curriculum", menuName = "Scriptable Objects/Curriculum")]
public class Curriculum : ScriptableObject
{
    public string[] UniteNames = new string[8];
    public string[] TopicNames = new string[24];

    [Space(10)]

    [SerializeField]
    [Tooltip("Müfredattaki Ünite Verileri")] public UnitesData[] unitesData = new UnitesData[8];
}

// Müfredattaki üniteler
[Serializable]
public class UnitesData
{
    [Tooltip("Müfredattaki Konu Baþlýklarý Verileri")] public TopicsData[] topicsData = new TopicsData[3]; // Müfredattaki her ünitede 3 konu olacak
}
// Müfredattaki konular
[Serializable]
public class TopicsData
{
    [Tooltip("Müfredattaki Paragraf Verileri")] public string[] paragraphs = new string[30]; // Her konu baþlýðý altýnda 10 paragraf olacak
}