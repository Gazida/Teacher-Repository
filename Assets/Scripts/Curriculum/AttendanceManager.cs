using TMPro;
using UnityEngine;

public class AttendanceManager : MonoBehaviour, IGameTimeObserver
{
    [Header("Referances Scripts")]
    [SerializeField] private InGameTimeManage inGameTimeManage;
    [SerializeField] private StudentsAndFeatures students;

    [Header("Set Active Objects")]
    [SerializeField] private GameObject AttendanceSystem;

    [Header("For UI")]
    [SerializeField] private TextMeshProUGUI isHereText;

    // Yoklama sistemini kontrol eden de�i�kenler
    private bool isAttendanceStarted; // Bir g�nde yaln�zca bir kez yoklama ba�latabilmesi i�in gereken metot
    private bool isAttendanceCompleted; // Yoklama tamamland� m�. Bu de�i�ken sayesinde yoklama bitmeden ��retme sistemi ba�lat�lmayacak.
    private int numberOfStudentWhoseAttendanceWasTaken; // Yoklamas� al�nan ��renci say�s�

    public bool IsAttendanceCompleted { get { return isAttendanceCompleted;} }

    private void Awake()
    {
        inGameTimeManage.Attach(this);
    }

    // Bunu sonra sil sadece denemek i�indi
    private void Update()
    {
        if (Input.GetKeyDown("l"))
        {
            for (int i = 0; i < students.studentDatas.Length; i++)
            {
                students.studentDatas[i].wasAttendanceTaken = false;
            }
        }        
    }
    public void StartAttendance()
    {
        isHereText.text = "";

        if (!isAttendanceCompleted)
        {
            int numberOfStudentsNotInClass = Random.Range(0, 4); // Yok yaz�lacak ��renci say�s�
            int[] randStudentId = new int[3]; // Yok yaz�lacak ��rencilerin Id de�erlerini tutan dizi

            // Yoklamay� sadece bir kez ba�latmay� sa�layan if ifadesi
            if (!isAttendanceStarted)
            {
                isAttendanceStarted = true; // Yoklama ba�lat�ld�ysa bu de�i�keni true de�erine e�itle

                // Ba�ta t�m ��recileri s�n�fta kabul ediyoruz.
                for (int i = 0; i < students.studentDatas.Length; i++)
                {
                    students.studentDatas[i].isCurrentlyAtSchool = true;
                }
                // Yok yaz�lacak ��rencileri belirleme
                for (int i = 0; i < numberOfStudentsNotInClass; i++)
                {
                    randStudentId[i] = Random.Range(0, students.studentDatas.Length);
                    students.studentDatas[randStudentId[i]].isCurrentlyAtSchool = false;
                }
                Debug.Log($"{numberOfStudentsNotInClass} ki�i s�n�fta yok");
            }
            AttendanceSystem.SetActive(true);
        }
    }
    public void IsHere(int id)
    {
        // �f i�inde kontrol etti�imiz �ey ��rencinin yoklamas� al�nd�ysa o ��renciyi tekrar sorgulamas�n� �nlemek.
        if (!students.studentDatas[id].wasAttendanceTaken)
        {
            isHereText.text = students.studentDatas[id].isCurrentlyAtSchool.ToString(); // Derste olup olmad���n�n infosunun verilece�i text de�i�tiriliyor.
        }
    }
    public void YesHere(int id)
    {
        // �f i�inde kontrol etti�imiz �ey ��rencinin yoklamas� al�nd�ysa o ��renciyi tekrar sorgulamas�n� �nlemek.
        if (!students.studentDatas[id].wasAttendanceTaken)
        {
            Debug.Log("Burada");

            students.studentDatas[id].wasAttendanceTaken = true; // id de�erine sahip ��rencinin yoklamas� al�nd�ysa "wasAttendanceTaken" de�i�kenini true yap.
            numberOfStudentWhoseAttendanceWasTaken++; // Yoklamas� al�nd�ysa bu de�i�kenin say�s�n� 1 artt�r

            // Yoklamas� al�nan ��renci say�s� s�n�ftaki ��renci say�s�na e�itse yoklamay� tamamland� yap
            if(numberOfStudentWhoseAttendanceWasTaken == students.studentDatas.Length)
            {
                isAttendanceCompleted = true;
                AttendanceSystem.SetActive(false);
            }
        }
    }
    public void NotHere(int id)
    {
        // �f i�inde kontrol etti�imiz �ey ��rencinin yoklamas� al�nd�ysa o ��renciyi tekrar sorgulamas�n� �nlemek.
        if (!students.studentDatas[id].wasAttendanceTaken)
        {
            Debug.Log("Burada de�il");

            students.studentDatas[id].numberOfAbsences++; // E�er ��renci s�n�fta yoksa devams�zl�k say�s�n� 1 artt�r.
            students.studentDatas[id].wasAttendanceTaken = true; // id de�erine sahip ��rencinin yoklamas� al�nd�ysa "wasAttendanceTaken" de�i�kenini true yap.
            numberOfStudentWhoseAttendanceWasTaken++; // Yoklamas� al�nd�ysa bu de�i�kenin say�s�n� 1 artt�r

            // Yoklamas� al�nan ��renci say�s� s�n�ftaki ��renci say�s�na e�itse yoklamay� tamamland� yap
            if (numberOfStudentWhoseAttendanceWasTaken == students.studentDatas.Length)
            {
                isAttendanceCompleted = true;
                AttendanceSystem.SetActive(false);
            }
        }
    }

    public void NextDay()
    {
        isAttendanceStarted = false; // Sonraki g�ne ge�ildi�inde tekrar yoklama ba�latabilmesi i�in bu de�i�ken false yap�lmal�
        isAttendanceCompleted = false; // Sonraki g�ne ge�ildi�inde "yoklama tamamland� m�" de�i�keni false yap�lmal�. 
        numberOfStudentWhoseAttendanceWasTaken = 0; // Yoklamas� al�nan ��renci say�s� her g�n s�f�rdan ba�lamal�.

        // Yukar�daki metotlarda "wasAttendanceTaken" de�i�keni true yap�l�yor. B�ylece o g�n i�inde bir ki�inin sadece bir kez yoklamas� sa�lan�yor.
        // Ertesi g�ne ge�ince bu de�i�ken false yap�l�yor ��nk� yoklaman�n her g�n al�nmas� gerekir.
        for (int i = 0; i < students.studentDatas.Length; i++)
        {
            students.studentDatas[i].wasAttendanceTaken = false;
        }
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
