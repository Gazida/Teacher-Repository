using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[Serializable]
public class QuestionsAndAnswersUI
{
    [Header("For UI")]
    [Tooltip("Defterde yazacak sorular�n texti.")] public TextMeshProUGUI questionText;
    [Tooltip("Defterde yazaca sorular�n cevap se�enekleri texti.")] public TextMeshProUGUI[] answerTexts;
}

public class PlayerHomeworkCheckingManager : MonoBehaviour
{
    [Header("Referances Scripts")]
    [SerializeField] private PlayerFeatures playerFeatures;
    [SerializeField] private StudentsAndFeatures studentsAndFeatures;

    [Header("SetActive Objects")]
    [Tooltip("Odev kontrol sistemi ui'�n�n ilk katman�.")][SerializeField] private GameObject startChecking;
    [Tooltip("Odev kontrol sistemi ui'�n�n katmanlar�. Defter sayfalar�")][SerializeField] private GameObject[] pages;

    [Space(10)]
    [Tooltip("��rencilerin defterlerinde yazcak olan soru ve cavplar.")][SerializeField] private QuestionsAndAnswersUI[] questionsAndAnswersUI;

    public void StartTheHomeworkChecking()
    {
        // Oyuncunun sordu�u sorular� ��rencilerin defterlerine yaz
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
        Debug.Log("Sonraki sayfaya ge�");
    }
}
