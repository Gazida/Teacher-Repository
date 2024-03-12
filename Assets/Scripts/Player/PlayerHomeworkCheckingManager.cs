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
    [SerializeField] private PlayerFeatures playerFeatures;
    [SerializeField] private StudentsAndFeatures studentsAndFeatures;

    [Header("SetActive Objects")]
    [Tooltip("Odev kontrol sistemi ui'ýnýn ilk katmaný.")][SerializeField] private GameObject startChecking;
    [Tooltip("Odev kontrol sistemi ui'ýnýn katmanlarý. Defter sayfalarý")][SerializeField] private GameObject[] pages;

    [Space(10)]
    [Tooltip("Öðrencilerin defterlerinde yazcak olan soru ve cavplar.")][SerializeField] private QuestionsAndAnswersUI[] questionsAndAnswersUI;

    public void StartTheHomeworkChecking()
    {
        // Oyuncunun sorduðu sorularý öðrencilerin defterlerine yaz
        for (int i = 0; i < 2; i++)
        {
            questionsAndAnswersUI[i].questionText.text = playerFeatures.homeWorkQuestions[i].question;

            for (int j = 0; j < 4; j++)
            {
                questionsAndAnswersUI[i].answerTexts[j].text = playerFeatures.homeWorkQuestions[i].answers[j];
            }
        }

        startChecking.SetActive(false);
        pages[0].SetActive(true);
    }
    public void SkipThePage()
    {
        Debug.Log("Sonraki sayfaya geç");
    }
}
