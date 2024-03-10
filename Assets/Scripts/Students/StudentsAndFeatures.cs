using System;
using UnityEngine;

[Serializable]
public struct StudentDatas
{
    [Header("General Informations")]
    public string studentName;
    public int studentId;

    // Yoklama ile ilgili de�i�kenler
    [Header("Information About Attendance")]
    [Tooltip("Toplam yok yaz�lma say�s�.")] public int numberOfAbsences;
    [Tooltip("O g�n okulda olup olmad���n� tuttu�umuz de�i�ken.")] public bool isCurrentlyAtSchool;
    [Tooltip("��retmenin tekrar tekrar yoklama almas�n� �nleyecek de�i�ken.")] public bool wasAttendanceTaken; //��retmen yoklamay� ald���nda bu de�er true olacak.
}

[CreateAssetMenu(fileName = "StudentsAndFeatures", menuName = "Scriptable Objects/StudentsAndFeatures")]
public class StudentsAndFeatures : ScriptableObject
{
    [SerializeField] public StudentDatas[] studentDatas;
}
