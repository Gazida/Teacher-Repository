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

    private bool hasHomeworkBeenAssignedThisWeek; // Bu hafta �dev verildi mi

    private void Awake()
    {
        inGameTimeManage.Attach(this); // Bu s�n�f IGameTimeObserver interface'ini i�eriyor. Bu metot'a "this" g�ndererek bu class'� g�zlemci yap�yoruz.(Observer Pattern)
    }

    // �dev Verme Sistemini ba�lat
    public void StartTheAssignment()
    {
        // Dersler bittiyse �dev verebilsin. && Bu hafta �dev verrilmediyse �dev verebilsin. && E�er haftan�n 3. g�n�yse �dev verebilsin.
        if (teachingManager.IsLessonsFinished && !hasHomeworkBeenAssignedThisWeek && (inGameTimeManage.CurrentNumberOfDay % 4) == 2)
        {
            hasHomeworkBeenAssignedThisWeek = true;

            startTheAssignmentSystem.SetActive(false);
            chooseHowManyQuestions.SetActive(true);
        }
        else
        {
            Debug.Log("�dev verebilmek i�in dersleri bitirmen gerek");
        }
    }
    // Soru say�s�n� onayla. Oyuncunun girdi�i de�ere g�re ka� soru sorulaca��n� belirle
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

        playerFeatures.questionsCount = numberOfQuestion; // sorulacak soru say�s�n� oyuncuda bulunan "soru say�s�" de�i�kenine ata.
        chooseHowManyQuestions.SetActive(false); // Soru say�s�n� onaylay�nca "Soru Say�s� Belirleme" k�sm�n� kapat
        enterTheQuestionAndAnswer.SetActive(true); // Soru say�s�n� onaylay�nca Soru ve Cevap Girme" k�sm�n� a�
    }
    // Soruyu Onayla. Oyuncunun girdi�i soruyu onayla.
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

        if (numberOfQustionsEntered != numberOfQuestion)
        {
            // Player �zelliklerinde bulunan �dev sorular� de�i�keninde bulunan "question" de�i�kenine oyuncunun girdi�i soruyu e�itle.
            playerFeatures.homeWorkQuestions[numberOfQustionsEntered].question = questionInputField.text;

            for (int i = 0; i < answersInputField.Length; i++)
            {
                // Player �zelliklerinde bulunan �dev sorular� de�i�keninde bulunan "answers" de�i�kenine oyuncunun girdi�i cevplar� e�itle.
                playerFeatures.homeWorkQuestions[numberOfQustionsEntered].answers[i] = answersInputField[i].text;
            }

            enterTheQuestionAndAnswer.SetActive(false); // Soruyu onaylay�nca "Soru ve Cevap Girme" kapat
            chooseCorrectAnswer.SetActive(true); // Soruyu onaylan�nca "Do�ru Cevab� Se�me" k�sm�n�a�
        }
    }
    // Dor�u cevap A ise bu metodu �al��t�r
    public void ChooseA()
    {
        // Girilen soru say�s� istenilen soru say�s�na ula�mad�ysa cevplar� onayla ve soru yazma b�l�m�ne tekrar d�n 
        if (numberOfQustionsEntered != numberOfQuestion)
        {
            playerFeatures.correctAnswer[numberOfQustionsEntered] = 'a'; // 

            chooseCorrectAnswer.SetActive(false); // Do�ru cevab� se�ince "Do�ru Cevab� Se�me" k�sm�n� kapat
            enterTheQuestionAndAnswer.SetActive(true); // Do�ru cevab� s�ince "Soru ve Cevap Girme" k�sm�n� a�

            numberOfQustionsEntered++;

            // Girilen soru say�s� istenilen soru say�s�na ula�t�ysa t�m sistemi kapat
            if (numberOfQustionsEntered == numberOfQuestion)
            {
                startTheAssignmentSystem.SetActive(true);
                chooseHowManyQuestions.SetActive(false);
                enterTheQuestionAndAnswer.SetActive(false);
                chooseCorrectAnswer.SetActive(false);
            }
        }
    }
    // Dor�u cevap B ise bu metodu �al��t�r
    public void ChooseB()
    {
        // Girilen soru say�s� istenilen soru say�s�na ula�mad�ysa cevplar� onayla ve soru yazma b�l�m�ne tekrar d�n 
        if (numberOfQustionsEntered != numberOfQuestion)
        {
            playerFeatures.correctAnswer[numberOfQustionsEntered] = 'b'; // 

            chooseCorrectAnswer.SetActive(false); // Do�ru cevab� se�ince "Do�ru Cevab� Se�me" k�sm�n� kapat
            enterTheQuestionAndAnswer.SetActive(true); // Do�ru cevab� s�ince "Soru ve Cevap Girme" k�sm�n� a�

            numberOfQustionsEntered++;

            // Girilen soru say�s� istenilen soru say�s�na ula�t�ysa t�m sistemi kapat
            if (numberOfQustionsEntered == numberOfQuestion)
            {
                startTheAssignmentSystem.SetActive(true);
                chooseHowManyQuestions.SetActive(false);
                enterTheQuestionAndAnswer.SetActive(false);
                chooseCorrectAnswer.SetActive(false);
            }
        }
    }
    // Dor�u cevap C ise bu metodu �al��t�r
    public void ChooseC()
    {
        // Girilen soru say�s� istenilen soru say�s�na ula�mad�ysa cevplar� onayla ve soru yazma b�l�m�ne tekrar d�n 
        if (numberOfQustionsEntered != numberOfQuestion)
        {
            playerFeatures.correctAnswer[numberOfQustionsEntered] = 'c'; // 

            chooseCorrectAnswer.SetActive(false); // Do�ru cevab� se�ince "Do�ru Cevab� Se�me" k�sm�n� kapat
            enterTheQuestionAndAnswer.SetActive(true); // Do�ru cevab� s�ince "Soru ve Cevap Girme" k�sm�n� a�

            numberOfQustionsEntered++;

            // Girilen soru say�s� istenilen soru say�s�na ula�t�ysa t�m sistemi kapat
            if (numberOfQustionsEntered == numberOfQuestion)
            {
                startTheAssignmentSystem.SetActive(true);
                chooseHowManyQuestions.SetActive(false);
                enterTheQuestionAndAnswer.SetActive(false);
                chooseCorrectAnswer.SetActive(false);
            }
        }
    }
    // Dor�u cevap D ise bu metodu �al��t�r
    public void ChooseD()
    {
        // Girilen soru say�s� istenilen soru say�s�na ula�mad�ysa cevplar� onayla ve soru yazma b�l�m�ne tekrar d�n 
        if (numberOfQustionsEntered != numberOfQuestion)
        {
            playerFeatures.correctAnswer[numberOfQustionsEntered] = 'd'; // 

            chooseCorrectAnswer.SetActive(false); // Do�ru cevab� se�ince "Do�ru Cevab� Se�me" k�sm�n� kapat
            enterTheQuestionAndAnswer.SetActive(true); // Do�ru cevab� s�ince "Soru ve Cevap Girme" k�sm�n� a�

            numberOfQustionsEntered++;

            // Girilen soru say�s� istenilen soru say�s�na ula�t�ysa t�m sistemi kapat
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
        Debug.Log("Ben AttendanceManager s�n�f�na ait NextDay metoduyum.");
    }

    public void NextWeek()
    {
        hasHomeworkBeenAssignedThisWeek = false; // Sonraki haftaya ge�ildi�inde "bu hafta �dev verildi mi" de�i�kenini false yap
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
