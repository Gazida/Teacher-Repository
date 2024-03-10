using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI.Table;

public class PlayerAssignmentManager : MonoBehaviour, IGameTimeObserver
{
    [Header("FOR UI")]
    [SerializeField][Tooltip("Oyuncunun kaç soru soracaðýný gireceði girdi alaný")] private TMP_InputField numberOfQustionsInputField;
    [SerializeField][Tooltip("Oyuncunun gireceði sorunun girdi alaný")] private TMP_InputField questionInputField;
    [SerializeField][Tooltip("Oyuncunun gireceði cevaplarýn girdi alaný")] private TMP_InputField[] answersInputField;

    [Space(10)]
    [SerializeField][Tooltip("Aktifliði deðiþtirilecek layer.")] private GameObject startTheAssignmentSystem;
    [SerializeField][Tooltip("Aktifliði deðiþtirilecek layer.")] private GameObject chooseHowManyQuestions;
    [SerializeField][Tooltip("Aktifliði deðiþtirilecek layer.")] private GameObject enterTheQuestionAndAnswer;
    [SerializeField][Tooltip("Aktifliði deðiþtirilecek layer.")] private GameObject chooseCorrectAnswer;

    private int numberOfQuestion; // Oyuncunun kaç soru ödev vereceðini bu deðiþkende tutucaz
    private int numberOfQustionsEntered; // Oyuncu her soru girdiðinde bu deðer artacak. numberOfQuestion deðiþkenine eþit olduðunda ise soru girme iþlemi bitmiþ olacak.

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
        chooseHowManyQuestions.SetActive(false); // Soru sayýsýný onaylayýnca "Soru Sayýsý Belirleme" kýsmýný kapat
        enterTheQuestionAndAnswer.SetActive(true); // Soru sayýsýný onaylayýnca Soru ve Cevap Girme" kýsmýný aç
    }
    public void ConfirmTheQuestion()
    {
        // Soru alanýna bir soru girilmediyse return döndür. (Metottan Çýk)
        if (questionInputField.text == "")
            return;

        for (int i = 0; i < answersInputField.Length; i++)
        {
            // Cevaplardan biri girilmediyse return dönder ve metottan çýk.
            if (answersInputField[i].text == "")
                return;
        }
        enterTheQuestionAndAnswer.SetActive(false); // Soruyu onaylayýnca "Soru ve Cevap Girme" kapat
        chooseCorrectAnswer.SetActive(true); // Soruyu onaylanýnca "Doðru Cevabý Seçme" kýsmýnýaç
        numberOfQuestion++;
    }
    public void ChooseA()
    {
        chooseCorrectAnswer.SetActive(false); // Doðru cevabý seçince "Doðru Cevabý Seçme" kýsmýný kapat
        enterTheQuestionAndAnswer.SetActive(true); // Doðru cevabý sçince "Soru ve Cevap Girme" kýsmýný aç
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
        Debug.Log("Ben AttendanceManager sýnýfýna ait NextWeek metoduyum.");
    }

    public void NextMonth()
    {
        Debug.Log("Ben AttendanceManager sýnýfýna ait NextMonth metoduyum.");
    }

    public void NextYear()
    {
        Debug.Log("Ben AttendanceManager sýnýfýna ait NextYear metoduyum.");
    }
}
