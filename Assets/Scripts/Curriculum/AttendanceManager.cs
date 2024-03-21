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

    // Yoklama sistemini kontrol eden deðiþkenler
    private bool isAttendanceStarted; // Bir günde yalnýzca bir kez yoklama baþlatabilmesi için gereken metot
    private bool isAttendanceCompleted; // Yoklama tamamlandý mý. Bu deðiþken sayesinde yoklama bitmeden öðretme sistemi baþlatýlmayacak.
    private int numberOfStudentWhoseAttendanceWasTaken; // Yoklamasý alýnan öðrenci sayýsý

    public bool IsAttendanceCompleted { get { return isAttendanceCompleted;} }

    private void Awake()
    {
        inGameTimeManage.Attach(this);
    }

    // Bunu sonra sil sadece denemek içindi
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
            int numberOfStudentsNotInClass = Random.Range(0, 4); // Yok yazýlacak öðrenci sayýsý
            int[] randStudentId = new int[3]; // Yok yazýlacak öðrencilerin Id deðerlerini tutan dizi

            // Yoklamayý sadece bir kez baþlatmayý saðlayan if ifadesi
            if (!isAttendanceStarted)
            {
                isAttendanceStarted = true; // Yoklama baþlatýldýysa bu deðiþkeni true deðerine eþitle

                // Baþta tüm öðrecileri sýnýfta kabul ediyoruz.
                for (int i = 0; i < students.studentDatas.Length; i++)
                {
                    students.studentDatas[i].isCurrentlyAtSchool = true;
                }
                // Yok yazýlacak öðrencileri belirleme
                for (int i = 0; i < numberOfStudentsNotInClass; i++)
                {
                    randStudentId[i] = Random.Range(0, students.studentDatas.Length);
                    students.studentDatas[randStudentId[i]].isCurrentlyAtSchool = false;
                }
                Debug.Log($"{numberOfStudentsNotInClass} kiþi sýnýfta yok");
            }
            AttendanceSystem.SetActive(true);
        }
    }
    public void IsHere(int id)
    {
        // Ýf içinde kontrol ettiðimiz þey öðrencinin yoklamasý alýndýysa o öðrenciyi tekrar sorgulamasýný önlemek.
        if (!students.studentDatas[id].wasAttendanceTaken)
        {
            isHereText.text = students.studentDatas[id].isCurrentlyAtSchool.ToString(); // Derste olup olmadýðýnýn infosunun verileceði text deðiþtiriliyor.
        }
    }
    public void YesHere(int id)
    {
        // Ýf içinde kontrol ettiðimiz þey öðrencinin yoklamasý alýndýysa o öðrenciyi tekrar sorgulamasýný önlemek.
        if (!students.studentDatas[id].wasAttendanceTaken)
        {
            Debug.Log("Burada");

            students.studentDatas[id].wasAttendanceTaken = true; // id deðerine sahip öðrencinin yoklamasý alýndýysa "wasAttendanceTaken" deðiþkenini true yap.
            numberOfStudentWhoseAttendanceWasTaken++; // Yoklamasý alýndýysa bu deðiþkenin sayýsýný 1 arttýr

            // Yoklamasý alýnan öðrenci sayýsý sýnýftaki öðrenci sayýsýna eþitse yoklamayý tamamlandý yap
            if(numberOfStudentWhoseAttendanceWasTaken == students.studentDatas.Length)
            {
                isAttendanceCompleted = true;
                AttendanceSystem.SetActive(false);
            }
        }
    }
    public void NotHere(int id)
    {
        // Ýf içinde kontrol ettiðimiz þey öðrencinin yoklamasý alýndýysa o öðrenciyi tekrar sorgulamasýný önlemek.
        if (!students.studentDatas[id].wasAttendanceTaken)
        {
            Debug.Log("Burada deðil");

            students.studentDatas[id].numberOfAbsences++; // Eðer öðrenci sýnýfta yoksa devamsýzlýk sayýsýný 1 arttýr.
            students.studentDatas[id].wasAttendanceTaken = true; // id deðerine sahip öðrencinin yoklamasý alýndýysa "wasAttendanceTaken" deðiþkenini true yap.
            numberOfStudentWhoseAttendanceWasTaken++; // Yoklamasý alýndýysa bu deðiþkenin sayýsýný 1 arttýr

            // Yoklamasý alýnan öðrenci sayýsý sýnýftaki öðrenci sayýsýna eþitse yoklamayý tamamlandý yap
            if (numberOfStudentWhoseAttendanceWasTaken == students.studentDatas.Length)
            {
                isAttendanceCompleted = true;
                AttendanceSystem.SetActive(false);
            }
        }
    }

    public void NextDay()
    {
        isAttendanceStarted = false; // Sonraki güne geçildiðinde tekrar yoklama baþlatabilmesi için bu deðiþken false yapýlmalý
        isAttendanceCompleted = false; // Sonraki güne geçildiðinde "yoklama tamamlandý mý" deðiþkeni false yapýlmalý. 
        numberOfStudentWhoseAttendanceWasTaken = 0; // Yoklamasý alýnan öðrenci sayýsý her gün sýfýrdan baþlamalý.

        // Yukarýdaki metotlarda "wasAttendanceTaken" deðiþkeni true yapýlýyor. Böylece o gün içinde bir kiþinin sadece bir kez yoklamasý saðlanýyor.
        // Ertesi güne geçince bu deðiþken false yapýlýyor çünkü yoklamanýn her gün alýnmasý gerekir.
        for (int i = 0; i < students.studentDatas.Length; i++)
        {
            students.studentDatas[i].wasAttendanceTaken = false;
        }
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
