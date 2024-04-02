using StarterAssets;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TeachingManager : MonoBehaviour, IGameTimeObserver
{
    [Header("Referances Scripts")]
    [SerializeField] private FirstPersonController firstPersonController;
    [SerializeField] private InGameTimeManage inGameTimeManage;
    [SerializeField] private AttendanceManager attendanceManager;
    [SerializeField] private PlayerSleepManager sleepManager;
    [SerializeField] private Curriculum curriculum;

    [Header("All Cirriculum Number Of")]
    [Tooltip("Müfredattaki toplam Unite sayýsý : (8)")] public int numberOfUnitInTheCirriculum = 8;
    [Tooltip("Müfredattaki toplam Konu sayýsý : (8 * 3)")] public int numberOfTopicInTheCirriculum = 8 * 3;
    [Tooltip("Müfredattaki toplam Paragraf sayýsý : (8 * 3 * 30)")] public int numberOfParagraphInTheCirriculum = 8 * 3 * 30;

    [Header("In A Day Number Of")]
    [Tooltip("Bir gün içindeki ders sayýsý : (3)")] public int numberOfLessonsInADay = 3;
    [Tooltip("Bir derste anlatýlacak paragraf sayýsý : (10)")] public int numberOfParagraphInTheLesson = 10;

    [Header("In A Split Number Of")]
    [Tooltip("Bir ünitedeki konu sayýsý : (3)")] public int numberOfTopicsInAUnit = 3;
    [Tooltip("Bir konudaki paragraf sayýsý : (30)")] public int numberOfParagraphInATopic = 30;

    [Space(10)]
    [Tooltip("Teneffüs süresi.")] public float breakTime = 120f;

    [Header("UI Elements")]
    [SerializeField] private Button teachButton;
    [SerializeField] private Button stopLessonButton;
    [SerializeField] private TMP_InputField teachParagraph;

    [Header("Set Active GameObjects")]
    [SerializeField] private GameObject teachPanel;

    // Anlýk olarak iþlenen müfredat
    private int currentNumberOfUnit; // Anlýk olarak anlatýlmýþ olan ünitelerin sayýsý
    private int currentNumberOfTopic; // Anlýk olarak anlatýlmýþ olan konularýn sayýsý
    private int currentNumberOfParagraph; // Anlýk olarak anlatýlmýþ olan paragraflarýn sayýsý

    // Ders anlatabilir mi
    private bool isStartLesson; // Ders baþladý mý ?
    private bool canTeachLesson = true; // Ders anlatabilir mi ?
    private bool isLessonsFinished; // O günkü dersler bitti mi ?
    private bool isCurrentLessonFinished; // O anki ders bitti mi ?
    private bool isTeacherLectureArea; // Öðretmen = Oyuncu ders anlatma alanýnda mý ?

    public bool IsStartLesson
    {
        get { return isStartLesson; }
        set
        {
            isStartLesson = value;
        }
    }
    public bool CanTeachLesson
    {
        get { return canTeachLesson; }
        set
        {
            canTeachLesson = value;
        }
    }
    public bool IsLessonsFinished
    {
        get { return isLessonsFinished; }
        set
        {
            isLessonsFinished = value;
        }
    }
    public bool IsCurrentLessonsFinished
    {
        get { return isCurrentLessonFinished; }
        set
        {
            isCurrentLessonFinished = value;
        }
    }
    public bool IsTeacherLectureArea
    {
        get { return isTeacherLectureArea; }
        set { isTeacherLectureArea = value; }
    }

    private void Awake()
    {
        inGameTimeManage.Attach(this); // Bu sýnýf IGameTimeObserver interface'ini içeriyor. Bu metot'a "this" göndererek bu class'ý gözlemci yapýyoruz.(Observer Pattern)
    }
    public void StartLesson()
    {
        // Yoklamayý aldýysa ders anlatabilir
        if (attendanceManager.IsAttendanceCompleted)
        {
            isStartLesson = true;
            teachPanel.SetActive(true);
        }
        else
        {
            Debug.Log("Yoklamayý almadýn");
        }
    }
    public void LectureMethot()
    {
        // Ders iþlenebiliyorsa dersi anlat.
        if (canTeachLesson)
        {
            // Öðretme tahtasýna girilen paragraf müfredattaki anlatmasý gereken paragrafa eþitse paragafý anlatmýþ say. Daha sonra bir sonraki paragrafa geç.
            if (teachParagraph.text.Equals(curriculum.unitesData[currentNumberOfUnit].topicsData[currentNumberOfTopic].paragraphs[currentNumberOfParagraph]))
            {
                Debug.Log(curriculum.unitesData[currentNumberOfUnit].topicsData[currentNumberOfTopic].paragraphs[currentNumberOfParagraph]);
                currentNumberOfParagraph++; // Her tuþa basýldýðýnda anlatýlan paragraf sayýsýný 1 arttýr.

                // Anlýk anlatýlan paragraf sayýsý bir derste anlatýlmasý gereken paragraf sayýsýna eþit olursa dersi bitir.
                if (currentNumberOfParagraph % numberOfParagraphInTheLesson == 0)
                {
                    canTeachLesson = false; // Ýlk ders bittiði için ders anlatma özelliðini kapat.
                    isCurrentLessonFinished = true;

                    // "Anlýk olarak iþlenen paragraf sayýsý"ný tutan deðiþken "bir konudaki paragraf sayýsý"na eþit olursa o günkü dersler bitmiþ olur.
                    if (currentNumberOfParagraph == numberOfParagraphInATopic)
                    {
                        isLessonsFinished = true;
                        currentNumberOfTopic++; // O günkü ders bittiyse 1 tane konu bitmiþ demektir.
                        canTeachLesson = false; // O günkü dersler bittiyse ders anlatabilme özelliðini kapat
                        sleepManager.SetCanSleep(true); // O günkü ders bitmiþse karakter tekrar uyuyabilir olsun.
                        Debug.Log("Bugünkü dersler bitt: " + currentNumberOfTopic);

                        stopLessonButton.gameObject.SetActive(true); // Dersler biterse dersi bitirme butonunu aktif yap
                        teachPanel.SetActive(false); // Dersler biterse öðretme panelini kapat

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

                        teachPanel.SetActive(false); // Dersler biterse öðretme panelini kapat
                        stopLessonButton.gameObject.SetActive(true);
                    }
                }
            }
            // Girdiði cümle(paragraf) müfredata uygun deðilse uyarý ver
            else
            {
                Debug.Log("Konuyu yanlýþ anlatýyorsun");
            }
        }
    }

    // Teneffüs
    IEnumerator Break()
    {
        yield return new WaitForSeconds(breakTime);
        canTeachLesson = true;
        isCurrentLessonFinished = false; // Teneffüs bittikten sonra "o anki ders bitti mi" deðiþkenini false yap

        //startLessonPanel.SetActive(true); // Teneffüs biterse ders baþlatma panelini aç
    }
    //  Bir sonraki günde bazý deðerler baþlangýç deðerine dönmeli.
    public void NextDay()
    {
        isStartLesson = false;
        isLessonsFinished = false; // Sonraki güne geçildiðinde ders bitti mi deðiþkeni false olmalý
        canTeachLesson = true; // Bir sonraki güne geçildiðinde tekrar ders anlatýlabilir olmalý
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
