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
        Debug.Log("StudentHomeworkManager s�n�f�n�n NextDay metodu"); 
    }

    public void NextWeek()
    {
        #region Onceki Odevi Sil
        for (int i = 0; i < studentsAndFeatures.studentDatas.Length; i++)
        {
            // Ba�ta t�m sorular� do�ru yapm�� olarak ayarla
            for (int j = 0; j < studentsAndFeatures.studentDatas[i].answers.Length; j++)
            {
                studentsAndFeatures.studentDatas[i].answers[j] = ' ';
            }
        }
        #endregion

        #region Sonraki Odevi Yap
        int questionCount = playerFeatures.questionsCount; // Oyuncunun soraca�� soru say�s�

        // a�a��daki koddaki hesap ��yledir: sorulan soru say�s� = 5 ise => 5 * 0.5 = 2.5 dir ve ceilToInt metodu ile 3'e yuvarlan�r.
        // Bu de�er bir ��rencinin ne kadar yanl�� yapabilece�ini belirlicek
        int tolerance = Mathf.FloorToInt(questionCount * 0.5f);

        Debug.Log(questionCount);

        // Ka� ��renci varsa hepsinin �devlerini yapmas�n� sa�la
        for (int i = 0; i < studentsAndFeatures.studentDatas.Length; i++)
        {
            // Ba�ta t�m sorular� do�ru yapm�� olarak ayarla
            for (int j = 0; j < questionCount; j++)
            {
                studentsAndFeatures.studentDatas[i].answers[j] = playerFeatures.correctAnswer[j];
            }
            // Max %50'lik bir hata oran� olmas�n� sa�la
            for (int j = 0; j < tolerance; j++)
            {
                // Random olarak 'a', 'b', 'c' veya 'd' se�imi yap�l�r
                char randomAnswer = (char)('a' + Random.Range(0, 4));
                int randomQuestion = Random.Range(0, questionCount);

                studentsAndFeatures.studentDatas[i].answers[randomQuestion] = randomAnswer;
            }
        }
        #endregion

    }

    public void NextMonth()
    {
        Debug.Log("StudentHomeworkManager s�n�f�n�n NextMonth metodu");
    }

    public void NextYear()
    {
        Debug.Log("StudentHomeworkManager s�n�f�n�n NextYear metodu");
    }
}
