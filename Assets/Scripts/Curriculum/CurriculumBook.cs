using TMPro;
using UnityEngine;

public class CurriculumBook : MonoBehaviour
{
    [Header("Referances Scripts")]
    [SerializeField] private Curriculum curriculum;

    [Header("UI Elements")]
    [Tooltip("M�fredattaki paragraflar bu textlere atanacak")][SerializeField] private TextMeshProUGUI[] paragraphTexts;
    [Tooltip("M�fredattaki konu ba�l�klar� bu texte atanacak")][SerializeField] private TextMeshProUGUI topicNameText;
    [Tooltip("M�fredattaki �nite ba�l�klar� bu texte atanacak")][SerializeField] private TextMeshProUGUI unitNameText;

    // anl�k tutulan de�erler
    private int currentParagraphIndex;
    private int currentTopicIndex;
    private int currentUnitIndex;

    private int unitCount;
    private int topicCount;
    private int paragraphCount;

    private void Start()
    {
        unitCount = curriculum.unitesData.Length;
        topicCount = curriculum.unitesData[0].topicsData.Length;
        paragraphCount = curriculum.unitesData[0].topicsData[0].paragraphs.Length;
    }

    // Sonraki sayfaya ge�
    public void NextPage()
    {
        // Paragraflar� ekranda g�stermek i�in d�ng�y� ba�lat�n
        for (int i = 0; i < paragraphTexts.Length; i++)
        {
            // �u anda g�r�nt�lenen paragraf� ekranda g�sterin
            paragraphTexts[i].text = curriculum.unitesData[currentUnitIndex].topicsData[currentTopicIndex].paragraphs[currentParagraphIndex];
            currentParagraphIndex++; // Bir sonraki paragrafa ge�mek i�in indeksi art�r�n

            // E�er currentParagraphIndex, paragraf say�s�na e�itse, bu konu tamamland� demektir
            if (currentParagraphIndex == paragraphCount)
            {
                currentTopicIndex++; // Bir sonraki konuya ge�mek i�in indeksi art�r�n
                currentParagraphIndex = 0; // Paragraf indeksini s�f�rlay�n

                Debug.LogWarning("�lk konu tamamland�"); // Uyar�: �lk konu tamamland�

                // E�er currentTopicIndex, konu say�s�na e�itse, bu �nite tamamland� demektir
                if (currentTopicIndex == topicCount)
                {
                    currentUnitIndex++; // Bir sonraki �niteye ge�mek i�in indeksi art�r�n
                    currentTopicIndex = 0; // Konu indeksini s�f�rlay�n

                    Debug.LogWarning("�lk �nite tamamland�"); // Uyar�: �lk �nite tamamland�

                    // E�er currentUnitIndex, �nite say�s�na e�itse, m�fredat tamamland� demektir
                    if (currentUnitIndex == unitCount)
                    {
                        currentUnitIndex = 0; // �nite indeksini s�f�rlay�n
                        Debug.Log("M�fredat tamamland�"); // Bilgi: M�fredat tamamland�
                    }
                }
            }
            Debug.Log("index: " + currentParagraphIndex);
        }
        topicNameText.text = $"{curriculum.unitesData[currentUnitIndex].topicsData[currentTopicIndex].topicName}:";
        unitNameText.text = $"{curriculum.unitesData[currentUnitIndex].uniteName}:";
    }
    // �nceki sayfaya ge�
    public void PrevPage()
    {
        for (int i = paragraphTexts.Length - 1; i >= 0; i--)
        {
            currentParagraphIndex--; // Bir �nceki paragrafa ge�mek i�in indeksi azalt

            if (currentParagraphIndex < 0)
            {
                currentTopicIndex--; // Bir �nceki konuya ge�mek i�in indeksi azalt
                currentParagraphIndex = curriculum.unitesData[0].topicsData[0].paragraphs.Length - 1; // bir �nceki konuya ge�ince paragraf indeksini de en sondan ba�lat

                Debug.LogWarning("�lk paragraflar ibtti");

                if (currentTopicIndex < 0)
                {
                    currentUnitIndex--; // Bir �nceki �niteye ge�mek i�in indeksi azalt
                    currentTopicIndex = curriculum.unitesData[0].topicsData.Length - 1; // bir �nceki �niteye ge�ince konu indeksini de en sondan ba�lat

                    Debug.LogWarning("�lk konu bitti");

                    if (currentUnitIndex < 0)
                    {
                        currentUnitIndex = curriculum.unitesData.Length - 1; // �nite indeksi 0 olursa en son �niteye g�nder
                        Debug.LogWarning("�lk �nite bitti");
                    }
                }
            }
            paragraphTexts[i].text = curriculum.unitesData[currentUnitIndex].topicsData[currentTopicIndex].paragraphs[currentParagraphIndex];

        }
        topicNameText.text = $"{curriculum.unitesData[currentUnitIndex].topicsData[currentTopicIndex].topicName}:";
        unitNameText.text = $"{curriculum.unitesData[currentUnitIndex].uniteName}:";
    }
}
