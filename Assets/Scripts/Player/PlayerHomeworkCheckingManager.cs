using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[Serializable]
public class QuestionsAndAnswersUI
{
    [Header("For UI")]
    [Tooltip("Defterde yazacak sorularýn texti.")] public TextMeshProUGUI questionText;
    [Tooltip("Defterde yazaca sorularýn cevap seçenekleri texti.")] public TextMeshProUGUI[] answerTexts;
}

public class PlayerHomeworkCheckingManager : MonoBehaviour
{
    [Header("Referances Scripts")]
    [SerializeField] private InGameTimeManage inGameTimeManage;
    [SerializeField] private PlayerFeatures playerFeatures;
    [SerializeField] private StudentsAndFeatures studentsAndFeatures;

    [Header("SetActive Objects")]
    [Tooltip("Odev kontrol sistemi ui'ýnýn ilk katmaný.")][SerializeField] private GameObject startChecking;
    [Tooltip("Odev kontrol sistemi ui'ýnýn katmanlarý. Defter sayfalarý")][SerializeField] private GameObject[] pages;

    [Header("Other")]
    [Tooltip("Sayfa baþýna düþecek olan soru sayýsý")][SerializeField] private int numberOfQuestionsPerPage;
    [Tooltip("Cevap anahtarý textler dizisi")][SerializeField] private TextMeshProUGUI[] answerTexts;
    [Tooltip("Öðrencilerin defterlerinde yazcak olan soru ve cavplar.")][SerializeField] private QuestionsAndAnswersUI[] questionsAndAnswersUI;

    private int currentPage; // anlýk hangi sayfada olduðumuzu tutacak deðiþken
    private int currentWriteQuestions;

    public void StartTheHomeworkChecking()
    {
        // Okulun ilk günü ödev olmayacaýndan il gün ödev kontrolü yapamaz. Her pazartesi ödev kontrol edebilir olsun.
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

            #region Cevap Anahtarýnýn Tutulacagi Textleri Ayarla
            for (int i = 0; i < answerTexts.Length; i++)
            {
                answerTexts[i].text = "";
            }
            for (int i = 0; i < playerFeatures.questionsCount; i++)
            {
                answerTexts[i].text = playerFeatures.correctAnswer[i].ToString();
            }
            #endregion

            // Oyuncunun sorduðu sorularý öðrencilerin defterlerine yaz
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
        }

    }
    public void SkipThePage()
    {
        int remanining = playerFeatures.questionsCount - currentWriteQuestions; // Oyuncunun sorduðu soru saysýndan sayfaya yazýlan anlýk soru sayýsýný çýkarýyoruz

        // "kalan" soru sayýsý sayfa baþýna düþen soru sayýsýndan büyük veya eþitse aþaðýdaki iþlemleri yap. deðilse else if'e gir.
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
        Debug.Log("Sonraki sayfaya geç");
    }
}
