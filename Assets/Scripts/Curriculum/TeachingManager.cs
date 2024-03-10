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
    [Tooltip("M�fredattaki toplam Unite say�s�")] public int numberOfUnitInTheCirriculum = 8;
    [Tooltip("M�fredattaki toplam Konu say�s�")] public int numberOfTopicInTheCirriculum = 8 * 3;
    [Tooltip("M�fredattaki toplam Paragraf say�s�")] public int numberOfParagraphInTheCirriculum = 8 * 3 * 30;

    [Header("In A Day Number Of")]
    [Tooltip("Bir g�n i�indeki ders say�s�")] public int numberOfLessonsInADay = 3;
    [Tooltip("Bir derste anlat�lacak paragraf say�s�")] public int numberOfParagraphInTheLesson = 10;

    [Header("In A Split Number Of")]
    [Tooltip("Bir �nitedeki konu say�s�")] public int numberOfTopicsInAUnit = 3;
    [Tooltip("Bir konudaki paragraf say�s�")] public int numberOfParagraphInATopic = 30;

    [Space(10)]
    [Tooltip("Teneff�s s�resi.")] public float breakTime = 120f;

    [Header("UI Elements")]
    [SerializeField] private Button teachButton;
    [SerializeField] private TMP_InputField teachParagraph;

    // Anl�k olarak i�lenen m�fredat
    private int currentNumberOfUnit; // Anl�k olarak anlat�lm�� olan �nitelerin say�s�
    private int currentNumberOfTopic; // Anl�k olarak anlat�lm�� olan konular�n say�s�
    private int currentNumberOfParagraph; // Anl�k olarak anlat�lm�� olan paragraflar�n say�s�

    private bool canTeachLesson = true; // Ders anlatabilir mi?

    private bool isTeacherLectureArea; // ��retmen = Oyuncu ders anlatma alan�nda m�?
    public bool IsTeacherLectureArea
    {
        get { return isTeacherLectureArea; }
        set { isTeacherLectureArea = value; }
    }

    private void Awake()
    {
        inGameTimeManage.Attach(this); // Bu s�n�f IGameTimeObserver interface'ini i�eriyor. Bu metot'a "this" g�ndererek bu class'� g�zlemci yap�yoruz.(Observer Pattern)
    }

    public void LectureMethot()
    {
        // Karakter ders anlatma trigger� i�indeyse ve Yokalama al�nd�ysa ders anlatabilsin
        if (isTeacherLectureArea && attendanceManager.IsAttendanceCompleted)
        {
            // Ders i�lenebiliyorsa dersi anlat. E�er g�n say�s� 4'�n katlar�na denk gelmiyorsa yani hafta sonu de�ilse ders anlatabilsin
            if (canTeachLesson && ((inGameTimeManage.CurrentNumberOfDay + 1) % 4) != 0) // +1 olmas�n�n sebebi g�n de�erleri de�i�kende 0 olarak ba�l�yor. Bu da 0%4 = 0 oldu�undan ilk g�n ders anlatazmaz hatas�na sebep oluyor.
            {
                // ��retme tahtas�na girilen paragraf m�fredattaki anlatmas� gerekn paragrafa e�itse paragaf� anlatm�� say. Daha sonra bir sonraki paragrafa ge�.
                if (teachParagraph.text.Equals(curriculum.unitesData[currentNumberOfUnit].topicsData[currentNumberOfTopic].paragraphs[currentNumberOfParagraph]))
                {
                    Debug.Log(curriculum.unitesData[currentNumberOfUnit].topicsData[currentNumberOfTopic].paragraphs[currentNumberOfParagraph]);
                    currentNumberOfParagraph++; // Her tu�a bas�ld���nda anlat�lan paragraf say�s�n� 1 artt�r.

                    // Anl�k anlat�lan paragraf say�s� bir derste anlat�lmas� gereken paragraf say�s�na e�it olursa dersi bitir.
                    if (currentNumberOfParagraph % numberOfParagraphInTheLesson == 0)
                    {
                        canTeachLesson = false; // �lk ders bitti�i i�in ders anlatma �zelli�ini kapat.

                        // "Anl�k olarak i�lenen paragraf say�s�"n� tutan de�i�ken "bir konudaki paragraf say�s�"na e�it olursa o g�nk� dersler bitmi� olur.
                        if (currentNumberOfParagraph == numberOfParagraphInATopic)
                        {
                            currentNumberOfTopic++; // O g�nk� ders bittiyse 1 tane konu bitmi� demektir.
                            canTeachLesson = false; // O g�nk� dersler bittiyse ders anlatabilme �zelli�ini kapat
                            sleepManager.SetCanSleep(true); // O g�nk� ders bitmi�se karakter tekrar uyuyabilir olsun.
                            Debug.Log("Bug�nk� dersler bitt: " + currentNumberOfTopic);

                            // "Anl�k i�lenen konu say�s�"n� tutan de�i�ken de�eri "bir �nitedeki konu say�s�"na e�it olursa anl�k de�er tutan de�i�keni s�f�rla. Yani bir sonraki haftaya tekrar kullan�ma haz�r hale getir.
                            if (currentNumberOfTopic == numberOfTopicsInAUnit)
                            {
                                currentNumberOfUnit++;
                                currentNumberOfTopic = 0;
                            }
                        }
                        // O g�nk� dersler bitmediyse teneff�s yapabilir
                        else
                        {
                            StartCoroutine(Break());
                            Debug.Log("Teneff�s");
                        }
                    }
                }    
            }
        }
    }
    // Teneff�s
    IEnumerator Break()
    {
        yield return new WaitForSeconds(breakTime);
        canTeachLesson = true;
    }
    //  Bir sonraki g�nde baz� de�erler ba�lang�� de�erine d�nmeli.
    public void NextDay()
    {
        canTeachLesson = true; // Bir sonraki g�ne ge�ildi�inde tekrar ders anlat�labilir omal�
        currentNumberOfParagraph = 0; // Bir sonraki g�nde paragraflar anlat�l�rken bu say� yine s�f�rdan ba�lamal�.
        Debug.Log("Sonraki g�ne ge�ildi.");
    }
    public void NextWeek()
    {
        currentNumberOfTopic = 0; // Bir sonraki haftada konular anlat�l�rken anl�k tutulan bu de�er s�f�rlanmal� ki "out of range index" hatas� olmas�n
        Debug.Log("Sonraki haftaya ge�ildi.");
    }
    public void NextMonth()
    {
        Debug.Log("Sonraki aya ge�ildi.");
    }
    public void NextYear()
    {
        Debug.Log("Sonraki y�la ge�ildi.");
    }
}
