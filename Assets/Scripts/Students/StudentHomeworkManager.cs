using System.Collections.Generic;
using UnityEngine;

public class StudentHomeworkManager : MonoBehaviour, IGameTimeObserver
{
    [Header("Referances Scripts")]
    [SerializeField] private InGameTimeManage inGameTimeManage;
    [SerializeField] private PlayerFeatures playerFeatures;
    [SerializeField] private StudentsAndFeatures studentsAndFeatures;

    private void Awake()
    {
        inGameTimeManage.Attach(this);
    }

    public void NextDay()
    {
        Debug.Log("StudentHomeworkManager sýnýfýnýn NextDay metodu"); 
    }

    public void NextWeek()
    {
        #region Onceki Odevi Sil
        for (int i = 0; i < studentsAndFeatures.studentDatas.Length; i++)
        {
            // Baþta tüm sorularý doðru yapmýþ olarak ayarla
            for (int j = 0; j < studentsAndFeatures.studentDatas[i].answers.Length; j++)
            {
                studentsAndFeatures.studentDatas[i].answers[j] = ' ';
            }
        }
        #endregion

        #region Sonraki Odevi Yap
        int questionCount = playerFeatures.questionsCount; // Oyuncunun soracaðý soru sayýsý

        // aþaðýdaki koddaki hesap þöyledir: sorulan soru sayýsý = 5 ise => 5 * 0.5 = 2.5 dir ve ceilToInt metodu ile 3'e yuvarlanýr.
        // Bu deðer bir öðrencinin ne kadar yanlýþ yapabileceðini belirlicek
        int tolerance = Mathf.FloorToInt(questionCount * 0.5f);

        Debug.Log(questionCount);

        // Kaç öðrenci varsa hepsinin ödevlerini yapmasýný saðla
        for (int i = 0; i < studentsAndFeatures.studentDatas.Length; i++)
        {
            // Baþta tüm sorularý doðru yapmýþ olarak ayarla
            for (int j = 0; j < questionCount; j++)
            {
                studentsAndFeatures.studentDatas[i].answers[j] = playerFeatures.correctAnswer[j];
            }
            // Max %50'lik bir hata oraný olmasýný saðla
            for (int j = 0; j < tolerance; j++)
            {
                // Random olarak 'a', 'b', 'c' veya 'd' seçimi yapýlýr
                char randomAnswer = (char)('a' + Random.Range(0, 4));
                int randomQuestion = Random.Range(0, questionCount);

                studentsAndFeatures.studentDatas[i].answers[randomQuestion] = randomAnswer;
            }
        }
        #endregion

    }

    public void NextMonth()
    {
        Debug.Log("StudentHomeworkManager sýnýfýnýn NextMonth metodu");
    }

    public void NextYear()
    {
        Debug.Log("StudentHomeworkManager sýnýfýnýn NextYear metodu");
    }
}
