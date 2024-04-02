using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class QuestionsAndAnswersUI
{
    [Header("For UI")]
    [Tooltip("Defterde yazacak sorular�n texti.")] public TextMeshProUGUI questionText;
    [Tooltip("Defterde yazacak sorular�n cevap se�enekleri texti.")] public TextMeshProUGUI[] answerTexts;
    [Tooltip("��renci cevaplar�n� belirticek image'lar.")] public Image[] studentAnswer;
}

public class PlayerHomeworkCheckingManager : MonoBehaviour
{
    public delegate void StopCheckingDelegate();
    public static event StopCheckingDelegate stopCheckingHomework;

    [Header("Referances Scripts")]
    [SerializeField] private InGameTimeManage inGameTimeManage;
    [SerializeField] private PlayerFeatures playerFeatures;
    [SerializeField] private StudentsAndFeatures studentsAndFeatures;

    [Header("Set Active Objects")]
    [Tooltip("Odev kontrol sistemi ui'�n�n ilk katman�.")][SerializeField] private GameObject startChecking;
    [Tooltip("Odev kontrol sistemi ui'�n�n katmanlar�. Defter sayfalar�")][SerializeField] private GameObject[] pages;

    [Header("Other")]
    [Tooltip("Sayfa ba��na d��ecek olan soru say�s�")][SerializeField] private int numberOfQuestionsPerPage;
    [Tooltip("Cevap anahtar� textler dizisi")][SerializeField] private TextMeshProUGUI[] answerTexts;
    [Tooltip("��rencilerin defterlerinde yazcak olan soru ve cavplar.")][SerializeField] private QuestionsAndAnswersUI[] questionsAndAnswersUI;

    private int currentPage; // anl�k hangi sayfada oldu�umuzu tutacak de�i�ken
    private int currentWriteQuestions;

    //
    private int studentId;
    public int StudentId { get { return studentId; } set {  studentId = value; } }

    private void OnEnable()
    {
        stopCheckingHomework += StopTheHomeworkChecking;
    }
    private void OnDisable()
    {
        stopCheckingHomework -= StopTheHomeworkChecking;
    }

    public void StartTheHomeworkChecking()
    {
        // Okulun ilk g�n� �dev olmayaca�ndan il g�n �dev kontrol� yapamaz. Her pazartesi �dev kontrol edebilir olsun.
        if (inGameTimeManage.CurrentNumberOfDay != 0 && ((inGameTimeManage.CurrentNumberOfDay + 1) % 4) == 1 )
        {

            #region Odevi Baslatmadan Once Textlerin Icini Bosalt

            for (int i = 0; i < questionsAndAnswersUI.Length; i++)
            {
                questionsAndAnswersUI[i].questionText.text = "";

                for (int j = 0; j < 4; j++)
                {
                    questionsAndAnswersUI[i].answerTexts[j].text = "";
                }
            }

            #endregion

            #region Cevap Anahtar�n�n Tutulacagi Textleri Ayarla
            for (int i = 0; i < answerTexts.Length; i++)
            {
                answerTexts[i].text = "";
            }
            for (int i = 0; i < playerFeatures.questionsCount; i++)
            {
                answerTexts[i].text = playerFeatures.correctAnswer[i].ToString();
            }
            #endregion

            // Oyuncunun sordu�u sorular� ��rencilerin defterlerine yaz
            for (int i = 0; i < playerFeatures.questionsCount; i++)
            {
                questionsAndAnswersUI[i].questionText.text = playerFeatures.homeWorkQuestions[i].question;

                for (int j = 0; j < 4; j++)
                {
                    questionsAndAnswersUI[i].answerTexts[j].text = playerFeatures.homeWorkQuestions[i].answers[j];
                }
            }

            startChecking.SetActive(false);
            pages[currentPage].SetActive(true);

            currentWriteQuestions += numberOfQuestionsPerPage;
            currentPage++;

            // Kontrol� ba�latmadan �nce t�m ��renci cevplar�n� kapat
            for(int i = 0; i < playerFeatures.questionsCount; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    questionsAndAnswersUI[i].studentAnswer[j].gameObject.SetActive(false);
                }
            }
            // ��renci cevaplar�n� i�aretle(belirle)
            for (int i = 0; i < playerFeatures.questionsCount; i++)
            {
                if (studentsAndFeatures.studentDatas[studentId].answers[i] == 'a')
                {
                    questionsAndAnswersUI[i].studentAnswer[0].gameObject.SetActive(true);
                }
                else if (studentsAndFeatures.studentDatas[studentId].answers[i] == 'b')
                {
                    questionsAndAnswersUI[i].studentAnswer[1].gameObject.SetActive(true);
                }
                else if (studentsAndFeatures.studentDatas[studentId].answers[i] == 'c')
                {
                    questionsAndAnswersUI[i].studentAnswer[2].gameObject.SetActive(true);
                }
                else if (studentsAndFeatures.studentDatas[studentId].answers[i] == 'd')
                {
                    questionsAndAnswersUI[i].studentAnswer[3].gameObject.SetActive(true);
                }
            }
        }

    }
    public void SkipThePage()
    {
        int remanining = playerFeatures.questionsCount - currentWriteQuestions; // Oyuncunun sordu�u soru says�ndan sayfaya yaz�lan anl�k soru say�s�n� ��kar�yoruz

        // "kalan" soru say�s� sayfa ba��na d��en soru say�s�ndan b�y�k veya e�itse a�a��daki i�lemleri yap. de�ilse else if'e gir.
        if (remanining >= numberOfQuestionsPerPage)
        {
            pages[currentPage - 1].SetActive(false);
            pages[currentPage].SetActive(true);

            currentWriteQuestions += numberOfQuestionsPerPage;
            currentPage++;
        }
        else if (remanining > 0 && remanining < numberOfQuestionsPerPage)
        {
            pages[currentPage - 1].SetActive(false);
            pages[currentPage].SetActive(true);

            currentWriteQuestions += remanining;
            currentPage++;
        }
        Debug.Log("Sonraki sayfaya ge�");
    }

    public void StopChecking()
    {
        stopCheckingHomework?.Invoke();
    }
    private void StopTheHomeworkChecking()
    {
        pages[currentPage - 1].SetActive(false);
        startChecking.SetActive(true);
        currentPage = 0;
        currentWriteQuestions = 0;
    }
}
