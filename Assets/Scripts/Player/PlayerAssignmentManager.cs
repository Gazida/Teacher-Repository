using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI.Table;

public class PlayerAssignmentManager : MonoBehaviour, IGameTimeObserver
{
    [Header("FOR UI")]
    [SerializeField][Tooltip("Oyuncunun ka� soru soraca��n� girece�i girdi alan�")] private TMP_InputField numberOfQustionsInputField;
    [SerializeField][Tooltip("Oyuncunun girece�i sorunun girdi alan�")] private TMP_InputField questionInputField;
    [SerializeField][Tooltip("Oyuncunun girece�i cevaplar�n girdi alan�")] private TMP_InputField[] answersInputField;

    [Space(10)]
    [SerializeField][Tooltip("Aktifli�i de�i�tirilecek layer.")] private GameObject startTheAssignmentSystem;
    [SerializeField][Tooltip("Aktifli�i de�i�tirilecek layer.")] private GameObject chooseHowManyQuestions;
    [SerializeField][Tooltip("Aktifli�i de�i�tirilecek layer.")] private GameObject enterTheQuestionAndAnswer;
    [SerializeField][Tooltip("Aktifli�i de�i�tirilecek layer.")] private GameObject chooseCorrectAnswer;

    private int numberOfQuestion; // Oyuncunun ka� soru �dev verece�ini bu de�i�kende tutucaz
    private int numberOfQustionsEntered; // Oyuncu her soru girdi�inde bu de�er artacak. numberOfQuestion de�i�kenine e�it oldu�unda ise soru girme i�lemi bitmi� olacak.

    public void StartTheAssignment()
    {
        startTheAssignmentSystem.SetActive(false);
        chooseHowManyQuestions.SetActive(true);
    }
    public void ConfirmTheQuestionCount()
    {
        numberOfQuestion = int.Parse(numberOfQustionsInputField.text);

        if (numberOfQuestion < 0)
        {
            numberOfQuestion = 1;
        }
        else if (numberOfQuestion > 10)
        {
            numberOfQuestion = 10;
        }
        chooseHowManyQuestions.SetActive(false); // Soru say�s�n� onaylay�nca "Soru Say�s� Belirleme" k�sm�n� kapat
        enterTheQuestionAndAnswer.SetActive(true); // Soru say�s�n� onaylay�nca Soru ve Cevap Girme" k�sm�n� a�
    }
    public void ConfirmTheQuestion()
    {
        // Soru alan�na bir soru girilmediyse return d�nd�r. (Metottan ��k)
        if (questionInputField.text == "")
            return;

        for (int i = 0; i < answersInputField.Length; i++)
        {
            // Cevaplardan biri girilmediyse return d�nder ve metottan ��k.
            if (answersInputField[i].text == "")
                return;
        }
        enterTheQuestionAndAnswer.SetActive(false); // Soruyu onaylay�nca "Soru ve Cevap Girme" kapat
        chooseCorrectAnswer.SetActive(true); // Soruyu onaylan�nca "Do�ru Cevab� Se�me" k�sm�n�a�
        numberOfQuestion++;
    }
    public void ChooseA()
    {
        chooseCorrectAnswer.SetActive(false); // Do�ru cevab� se�ince "Do�ru Cevab� Se�me" k�sm�n� kapat
        enterTheQuestionAndAnswer.SetActive(true); // Do�ru cevab� s�ince "Soru ve Cevap Girme" k�sm�n� a�
    }
    public void ChooseB()
    {
        chooseCorrectAnswer.SetActive(false);
        enterTheQuestionAndAnswer.SetActive(true);
    }
    public void ChooseC()
    {
        chooseCorrectAnswer.SetActive(false);
        enterTheQuestionAndAnswer.SetActive(true);
    }
    public void ChooseD()
    {
        chooseCorrectAnswer.SetActive(false);
        enterTheQuestionAndAnswer.SetActive(true);
    }

    public void NextDay()
    {
        throw new System.NotImplementedException();
    }

    public void NextWeek()
    {
        Debug.Log("Ben AttendanceManager s�n�f�na ait NextWeek metoduyum.");
    }

    public void NextMonth()
    {
        Debug.Log("Ben AttendanceManager s�n�f�na ait NextMonth metoduyum.");
    }

    public void NextYear()
    {
        Debug.Log("Ben AttendanceManager s�n�f�na ait NextYear metoduyum.");
    }
}
