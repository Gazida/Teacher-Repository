using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TeachingManager : MonoBehaviour, IGameTimeObserver
{
    [Header("Referances Scripts")]
    [SerializeField] private InGameTimeManage inGameTimeManage;
    [SerializeField] private AttendanceManager attendanceManager;
    [SerializeField] private PlayerSleepManager sleepManager;
    [SerializeField] private Curriculum curriculum;

    [Header("All Cirriculum Number Of")]
    [Tooltip("Müfredattaki toplam Unite sayýsý")] public int numberOfUnitInTheCirriculum = 8;
    [Tooltip("Müfredattaki toplam Konu sayýsý")] public int numberOfTopicInTheCirriculum = 8 * 3;
    [Tooltip("Müfredattaki toplam Paragraf sayýsý")] public int numberOfParagraphInTheCirriculum = 8 * 3 * 30;

    [Header("In A Day Number Of")]
    [Tooltip("Bir gün içindeki ders sayýsý")] public int numberOfLessonsInADay = 3;
    [Tooltip("Bir derste anlatýlacak paragraf sayýsý")] public int numberOfParagraphInTheLesson = 10;

    [Header("In A Split Number Of")]
    [Tooltip("Bir ünitedeki konu sayýsý")] public int numberOfTopicsInAUnit = 3;
    [Tooltip("Bir konudaki paragraf sayýsý")] public int numberOfParagraphInATopic = 30;

    [Space(10)]
    [Tooltip("Teneffüs süresi.")] public float breakTime = 120f;

    [Header("UI Elements")]
    [SerializeField] private Button teachButton;
    [SerializeField] private TMP_InputField teachParagraph;

    // Anlýk olarak iþlenen müfredat
    private int currentNumberOfUnit; // Anlýk olarak anlatýlmýþ olan ünitelerin sayýsý
    private int currentNumberOfTopic; // Anlýk olarak anlatýlmýþ olan konularýn sayýsý
    private int currentNumberOfParagraph; // Anlýk olarak anlatýlmýþ olan paragraflarýn sayýsý

    private bool canTeachLesson = true; // Ders anlatabilir mi?

    private bool isTeacherLectureArea; // Öðretmen = Oyuncu ders anlatma alanýnda mý?
    public bool IsTeacherLectureArea
    {
        get { return isTeacherLectureArea; }
        set { isTeacherLectureArea = value; }
    }

    private void Awake()
    {
        inGameTimeManage.Attach(this); // Bu sýnýf IGameTimeObserver interface'ini içeriyor. Bu metot'a "this" göndererek bu class'ý gözlemci yapýyoruz.(Observer Pattern)
    }

    public void LectureMethot()
    {
        // Karakter ders anlatma triggerý içindeyse ve Yokalama alýndýysa ders anlatabilsin
        if (isTeacherLectureArea && attendanceManager.IsAttendanceCompleted)
        {
            // Ders iþlenebiliyorsa dersi anlat. Eðer gün sayýsý 4'ün katlarýna denk gelmiyorsa yani hafta sonu deðilse ders anlatabilsin
            if (canTeachLesson && ((inGameTimeManage.CurrentNumberOfDay + 1) % 4) != 0) // +1 olmasýnýn sebebi gün deðerleri deðiþkende 0 olarak baþlýyor. Bu da 0%4 = 0 olduðundan ilk gün ders anlatazmaz hatasýna sebep oluyor.
            {
                // Öðretme tahtasýna girilen paragraf müfredattaki anlatmasý gerekn paragrafa eþitse paragafý anlatmýþ say. Daha sonra bir sonraki paragrafa geç.
                if (teachParagraph.text.Equals(curriculum.unitesData[currentNumberOfUnit].topicsData[currentNumberOfTopic].paragraphs[currentNumberOfParagraph]))
                {
                    Debug.Log(curriculum.unitesData[currentNumberOfUnit].topicsData[currentNumberOfTopic].paragraphs[currentNumberOfParagraph]);
                    currentNumberOfParagraph++; // Her tuþa basýldýðýnda anlatýlan paragraf sayýsýný 1 arttýr.

                    // Anlýk anlatýlan paragraf sayýsý bir derste anlatýlmasý gereken paragraf sayýsýna eþit olursa dersi bitir.
                    if (currentNumberOfParagraph % numberOfParagraphInTheLesson == 0)
                    {
                        canTeachLesson = false; // Ýlk ders bittiði için ders anlatma özelliðini kapat.

                        // "Anlýk olarak iþlenen paragraf sayýsý"ný tutan deðiþken "bir konudaki paragraf sayýsý"na eþit olursa o günkü dersler bitmiþ olur.
                        if (currentNumberOfParagraph == numberOfParagraphInATopic)
                        {
                            currentNumberOfTopic++; // O günkü ders bittiyse 1 tane konu bitmiþ demektir.
                            canTeachLesson = false; // O günkü dersler bittiyse ders anlatabilme özelliðini kapat
                            sleepManager.SetCanSleep(true); // O günkü ders bitmiþse karakter tekrar uyuyabilir olsun.
                            Debug.Log("Bugünkü dersler bitt: " + currentNumberOfTopic);

                            // "Anlýk iþlenen konu sayýsý"ný tutan deðiþken deðeri "bir ünitedeki konu sayýsý"na eþit olursa anlýk deðer tutan deðiþkeni sýfýrla. Yani bir sonraki haftaya tekrar kullanýma hazýr hale getir.
                            if (currentNumberOfTopic == numberOfTopicsInAUnit)
                            {
                                currentNumberOfUnit++;
                                currentNumberOfTopic = 0;
                            }
                        }
                        // O günkü dersler bitmediyse teneffüs yapabilir
                        else
                        {
                            StartCoroutine(Break());
                            Debug.Log("Teneffüs");
                        }
                    }
                }    
            }
        }
    }
    // Teneffüs
    IEnumerator Break()
    {
        yield return new WaitForSeconds(breakTime);
        canTeachLesson = true;
    }
    //  Bir sonraki günde bazý deðerler baþlangýç deðerine dönmeli.
    public void NextDay()
    {
        canTeachLesson = true; // Bir sonraki güne geçildiðinde tekrar ders anlatýlabilir omalý
        currentNumberOfParagraph = 0; // Bir sonraki günde paragraflar anlatýlýrken bu sayý yine sýfýrdan baþlamalý.
        Debug.Log("Sonraki güne geçildi.");
    }
    public void NextWeek()
    {
        currentNumberOfTopic = 0; // Bir sonraki haftada konular anlatýlýrken anlýk tutulan bu deðer sýfýrlanmalý ki "out of range index" hatasý olmasýn
        Debug.Log("Sonraki haftaya geçildi.");
    }
    public void NextMonth()
    {
        Debug.Log("Sonraki aya geçildi.");
    }
    public void NextYear()
    {
        Debug.Log("Sonraki yýla geçildi.");
    }
}
