using System;
using UnityEngine;

[Serializable]
public struct StudentDatas
{
    [Header("General Informations")]
    public string studentName;
    public int studentId;

    // Yoklama ile ilgili deðiþkenler
    [Header("Information About Attendance")]
    [Tooltip("Toplam yok yazýlma sayýsý.")] public int numberOfAbsences;
    [Tooltip("O gün okulda olup olmadýðýný tuttuðumuz deðiþken.")] public bool isCurrentlyAtSchool;
    [Tooltip("Öðretmenin tekrar tekrar yoklama almasýný önleyecek deðiþken.")] public bool wasAttendanceTaken; //Öðretmen yoklamayý aldýðýnda bu deðer true olacak.
}

[CreateAssetMenu(fileName = "StudentsAndFeatures", menuName = "Scriptable Objects/StudentsAndFeatures")]
public class StudentsAndFeatures : ScriptableObject
{
    [SerializeField] public StudentDatas[] studentDatas;
}
