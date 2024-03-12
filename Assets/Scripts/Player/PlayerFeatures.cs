using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerFeatures", menuName = "Scriptable Objects/PlayerFeatures")]
public class PlayerFeatures : ScriptableObject
{
    [Header("Player")]
    [Tooltip("Karakter ismi")] public string characterName;
    [Tooltip("Karakter ya��")] public byte characterAge;
    [Tooltip("��retmenlik Bran��")] public string characterBranch;

    [Header("Bank Account")]
    [Tooltip("Maa�")] public float salary;
    [Tooltip("Hesap bakiyesi")] public float accountBalance;
    [Tooltip("Kart �ifresi")] public int cardPassword;

    // Bu de�erler oyun i�i "HUD UI" sisteminde image'lar�n fillAmount de�erlerini belirleyece�i i�in 0-1 aras�nda s�n�rland�r�ld� 
    [Header("Humanoid Features")]
    [Tooltip("Ac�kma durumunun kontrol edilece�i de�i�ken")][Range(0, 1)] public float hunger;
    [Tooltip("Susama durumunun kontrol edilece�i de�i�ken")][Range(0, 1)] public float thirst;
    [Tooltip("Mana'n�n durumunun kontrol edilece�i de�i�ken")][Range(0, 1)] public float mana;
    [Tooltip("Tuvalet ihtiyac�n�n kontrol edilece�i de�i�ken")][Range(0, 1)] public float needForToilet;


    [Header("Homework For Students")] // �dev Sisteminde kullan�lacak de�i�kenler
    [Tooltip("Oyuncunun soru ve cevap girdilerinin tutulaca�� yap�")]public HomeWorkQuestions[] homeWorkQuestions;
    [Tooltip("�dev sorular�n�n do�ru cevaplar�n�n tutulaca�� dizi. Dizi boyutunu 10 olarak ayarla." +
        " Di�er scriptlerde soru say�s�na g�re kontrol sa�lan�yor olacak zaten.")] public char[] correctAnswer;
    [Tooltip("�dev i�in verilecek soru say�s�n� tutacak de�i�ken.")] public int questionsCount;
}

// Oyuncunun soru ve cevap girdilerinin tutulaca�� s�n�f(yap�)
[Serializable]
public class HomeWorkQuestions
{
    [Tooltip("Soru")] public string question;
    [Tooltip("Cevaplar")] public string[] answers;
}
