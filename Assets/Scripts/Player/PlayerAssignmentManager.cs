using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI.Table;

public class PlayerAssignmentManager : MonoBehaviour, IGameTimeObserver
{
    [Header("Referances Scripts")]
    [SerializeField] private InGameTimeManage inGameTimeManage;
    [SerializeField] private TeachingManager teachingManager;
    [SerializeField] private PlayerFeatures playerFeatures;
    [SerializeField] private StudentsAndFeatures studentsAndFeatures;

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

    private bool hasHomeworkBeenAssignedThisWeek; // Bu hafta ödev verildi mi

    private void Awake()
    {
        inGameTimeManage.Attach(this); // Bu sýnýf IGameTimeObserver interface'ini içeriyor. Bu metot'a "this" göndererek bu class'ý gözlemci yapýyoruz.(Observer Pattern)
    }

    // Ödev Verme Sistemini baþlat
    public void StartTheAssignment()
    {
        // Dersler bittiyse ödev verebilsin. && Bu hafta ödev verrilmediyse ödev verebilsin. && Eðer haftanýn 3. günüyse ödev verebilsin.
        if (teachingManager.IsLessonsFinished && !hasHomeworkBeenAssignedThisWeek && (inGameTimeManage.CurrentNumberOfDay % 4) == 2)
        {
            hasHomeworkBeenAssignedThisWeek = true;

            startTheAssignmentSystem.SetActive(false);
            chooseHowManyQuestions.SetActive(true);
        }
        else
        {
            Debug.Log("Ödev verebilmek için dersleri bitirmen gerek");
        }
    }
    // Soru sayýsýný onayla. Oyuncunun girdiði deðere göre kaç soru sorulacaðýný belirle
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

        playerFeatures.questionsCount = numberOfQuestion; // sorulacak soru sayýsýný oyuncuda bulunan "soru sayýsý" deðiþkenine ata.
        chooseHowManyQuestions.SetActive(false); // Soru sayýsýný onaylayýnca "Soru Sayýsý Belirleme" kýsmýný kapat
        enterTheQuestionAndAnswer.SetActive(true); // Soru sayýsýný onaylayýnca Soru ve Cevap Girme" kýsmýný aç
    }
    // Soruyu Onayla. Oyuncunun girdiði soruyu onayla.
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

        if (numberOfQustionsEntered != numberOfQuestion)
        {
            // Player özelliklerinde bulunan ödev sorularý deðiþkeninde bulunan "question" deðiþkenine oyuncunun girdiði soruyu eþitle.
            playerFeatures.homeWorkQuestions[numberOfQustionsEntered].question = questionInputField.text;

            for (int i = 0; i < answersInputField.Length; i++)
            {
                // Player özelliklerinde bulunan ödev sorularý deðiþkeninde bulunan "answers" deðiþkenine oyuncunun girdiði cevplarý eþitle.
                playerFeatures.homeWorkQuestions[numberOfQustionsEntered].answers[i] = answersInputField[i].text;
            }

            enterTheQuestionAndAnswer.SetActive(false); // Soruyu onaylayýnca "Soru ve Cevap Girme" kapat
            chooseCorrectAnswer.SetActive(true); // Soruyu onaylanýnca "Doðru Cevabý Seçme" kýsmýnýaç
        }
    }
    // Dorðu cevap A ise bu metodu çalýþtýr
    public void ChooseA()
    {
        // Girilen soru sayýsý istenilen soru sayýsýna ulaþmadýysa cevplarý onayla ve soru yazma bölümüne tekrar dön 
        if (numberOfQustionsEntered != numberOfQuestion)
        {
            playerFeatures.correctAnswer[numberOfQustionsEntered] = 'a'; // 

            chooseCorrectAnswer.SetActive(false); // Doðru cevabý seçince "Doðru Cevabý Seçme" kýsmýný kapat
            enterTheQuestionAndAnswer.SetActive(true); // Doðru cevabý sçince "Soru ve Cevap Girme" kýsmýný aç

            numberOfQustionsEntered++;

            // Girilen soru sayýsý istenilen soru sayýsýna ulaþtýysa tüm sistemi kapat
            if (numberOfQustionsEntered == numberOfQuestion)
            {
                startTheAssignmentSystem.SetActive(true);
                chooseHowManyQuestions.SetActive(false);
                enterTheQuestionAndAnswer.SetActive(false);
                chooseCorrectAnswer.SetActive(false);
            }
        }
    }
    // Dorðu cevap B ise bu metodu çalýþtýr
    public void ChooseB()
    {
        // Girilen soru sayýsý istenilen soru sayýsýna ulaþmadýysa cevplarý onayla ve soru yazma bölümüne tekrar dön 
        if (numberOfQustionsEntered != numberOfQuestion)
        {
            playerFeatures.correctAnswer[numberOfQustionsEntered] = 'b'; // 

            chooseCorrectAnswer.SetActive(false); // Doðru cevabý seçince "Doðru Cevabý Seçme" kýsmýný kapat
            enterTheQuestionAndAnswer.SetActive(true); // Doðru cevabý sçince "Soru ve Cevap Girme" kýsmýný aç

            numberOfQustionsEntered++;

            // Girilen soru sayýsý istenilen soru sayýsýna ulaþtýysa tüm sistemi kapat
            if (numberOfQustionsEntered == numberOfQuestion)
            {
                startTheAssignmentSystem.SetActive(true);
                chooseHowManyQuestions.SetActive(false);
                enterTheQuestionAndAnswer.SetActive(false);
                chooseCorrectAnswer.SetActive(false);
            }
        }
    }
    // Dorðu cevap C ise bu metodu çalýþtýr
    public void ChooseC()
    {
        // Girilen soru sayýsý istenilen soru sayýsýna ulaþmadýysa cevplarý onayla ve soru yazma bölümüne tekrar dön 
        if (numberOfQustionsEntered != numberOfQuestion)
        {
            playerFeatures.correctAnswer[numberOfQustionsEntered] = 'c'; // 

            chooseCorrectAnswer.SetActive(false); // Doðru cevabý seçince "Doðru Cevabý Seçme" kýsmýný kapat
            enterTheQuestionAndAnswer.SetActive(true); // Doðru cevabý sçince "Soru ve Cevap Girme" kýsmýný aç

            numberOfQustionsEntered++;

            // Girilen soru sayýsý istenilen soru sayýsýna ulaþtýysa tüm sistemi kapat
            if (numberOfQustionsEntered == numberOfQuestion)
            {
                startTheAssignmentSystem.SetActive(true);
                chooseHowManyQuestions.SetActive(false);
                enterTheQuestionAndAnswer.SetActive(false);
                chooseCorrectAnswer.SetActive(false);
            }
        }
    }
    // Dorðu cevap D ise bu metodu çalýþtýr
    public void ChooseD()
    {
        // Girilen soru sayýsý istenilen soru sayýsýna ulaþmadýysa cevplarý onayla ve soru yazma bölümüne tekrar dön 
        if (numberOfQustionsEntered != numberOfQuestion)
        {
            playerFeatures.correctAnswer[numberOfQustionsEntered] = 'd'; // 

            chooseCorrectAnswer.SetActive(false); // Doðru cevabý seçince "Doðru Cevabý Seçme" kýsmýný kapat
            enterTheQuestionAndAnswer.SetActive(true); // Doðru cevabý sçince "Soru ve Cevap Girme" kýsmýný aç

            numberOfQustionsEntered++;

            // Girilen soru sayýsý istenilen soru sayýsýna ulaþtýysa tüm sistemi kapat
            if (numberOfQustionsEntered == numberOfQuestion)
            {
                startTheAssignmentSystem.SetActive(true);
                chooseHowManyQuestions.SetActive(false);
                enterTheQuestionAndAnswer.SetActive(false);
                chooseCorrectAnswer.SetActive(false);
            }
        }
    }

    public void NextDay()
    {
        Debug.Log("Ben AttendanceManager sýnýfýna ait NextDay metoduyum.");
    }

    public void NextWeek()
    {
        hasHomeworkBeenAssignedThisWeek = false; // Sonraki haftaya geçildiðinde "bu hafta ödev verildi mi" deðiþkenini false yap
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
