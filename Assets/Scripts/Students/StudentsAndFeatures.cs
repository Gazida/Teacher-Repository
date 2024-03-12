using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StudentDatas
{
    [Header("General Informations")]
    public string studentName;
    public int studentId;

    // Yoklama ile ilgili deðiþkenler
    [Header("Information About Attendance")]
    [Tooltip("Toplam yok yazýlma sayýsý.")] public int numberOfAbsences;
    [Tooltip("O gün okulda olup olmadýðýný tuttuðumuz deðiþken.")] public bool isCurrentlyAtSchool;
    [Tooltip("Öðretmenin tekrar tekrar yoklama almasýný önleyecek deðiþken.")] public bool wasAttendanceTaken; //Öðretmen yoklamayý aldýðýnda bu deðer true olacak.


    [Header("For Homework")]
    // Aþaðýdaki deðiþkenlere þuan ihtiyaç yok. Ýhtiyaç olursa yorum satýrý kaldýrýlacak.
    // [Tooltip("Bu deðiþkende öðretmenin(oyuncunun) verdiði ödevdeki sorular tutulacak.")] public string[] questions;
    [Tooltip("Bu deðiþkende öðretmenin(oyuncunun) verdiði ödevdeki cevaplar tutulacak.")] public char[] answers;

}

[CreateAssetMenu(fileName = "StudentsAndFeatures", menuName = "Scriptable Objects/StudentsAndFeatures")]
public class StudentsAndFeatures : ScriptableObject
{
    [SerializeField] public StudentDatas[] studentDatas;
}
