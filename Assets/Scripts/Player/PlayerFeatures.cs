using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerFeatures", menuName = "Scriptable Objects/PlayerFeatures")]
public class PlayerFeatures : ScriptableObject
{
    [Header("Player")]
    [Tooltip("Karakter ismi")] public string characterName;
    [Tooltip("Karakter yaþý")] public byte characterAge;
    [Tooltip("Öðretmenlik Branþý")] public string characterBranch;

    [Header("Bank Account")]
    [Tooltip("Maaþ")] public float salary;
    [Tooltip("Hesap bakiyesi")] public float accountBalance;
    [Tooltip("Kart þifresi")] public int cardPassword;

    // Bu deðerler oyun içi "HUD UI" sisteminde image'larýn fillAmount deðerlerini belirleyeceði için 0-1 arasýnda sýnýrlandýrýldý 
    [Header("Humanoid Features")]
    [Tooltip("Acýkma durumunun kontrol edileceði deðiþken")][Range(0, 1)] public float hunger;
    [Tooltip("Susama durumunun kontrol edileceði deðiþken")][Range(0, 1)] public float thirst;
    [Tooltip("Mana'nýn durumunun kontrol edileceði deðiþken")][Range(0, 1)] public float mana;
    [Tooltip("Tuvalet ihtiyacýnýn kontrol edileceði deðiþken")][Range(0, 1)] public float needForToilet;


    [Header("Homework For Students")] // Ödev Sisteminde kullanýlacak deðiþkenler
    [Tooltip("Oyuncunun soru ve cevap girdilerinin tutulacaðý yapý")]public HomeWorkQuestions[] homeWorkQuestions;
    [Tooltip("Ödev sorularýnýn doðru cevaplarýnýn tutulacaðý dizi. Dizi boyutunu 10 olarak ayarla." +
        " Diðer scriptlerde soru sayýsýna göre kontrol saðlanýyor olacak zaten.")] public char[] correctAnswer;
    [Tooltip("Ödev için verilecek soru sayýsýný tutacak deðiþken.")] public int questionsCount;
}

// Oyuncunun soru ve cevap girdilerinin tutulacaðý sýnýf(yapý)
[Serializable]
public class HomeWorkQuestions
{
    [Tooltip("Soru")] public string question;
    [Tooltip("Cevaplar")] public string[] answers;
}
