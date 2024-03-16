using TMPro;
using UnityEngine;

public class CurriculumBook : MonoBehaviour
{
    [Header("Referances Scripts")]
    [SerializeField] private Curriculum curriculum;

    [Header("UI Elements")]
    [Tooltip("Müfredattaki paragraflar bu textlere atanacak")][SerializeField] private TextMeshProUGUI[] paragraphTexts;
    [Tooltip("Müfredattaki konu baþlýklarý bu texte atanacak")][SerializeField] private TextMeshProUGUI topicNameText;
    [Tooltip("Müfredattaki ünite baþlýklarý bu texte atanacak")][SerializeField] private TextMeshProUGUI unitNameText;

    // anlýk tutulan deðerler
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

    // Sonraki sayfaya geç
    public void NextPage()
    {
        // Paragraflarý ekranda göstermek için döngüyü baþlatýn
        for (int i = 0; i < paragraphTexts.Length; i++)
        {
            // Þu anda görüntülenen paragrafý ekranda gösterin
            paragraphTexts[i].text = curriculum.unitesData[currentUnitIndex].topicsData[currentTopicIndex].paragraphs[currentParagraphIndex];
            currentParagraphIndex++; // Bir sonraki paragrafa geçmek için indeksi artýrýn

            // Eðer currentParagraphIndex, paragraf sayýsýna eþitse, bu konu tamamlandý demektir
            if (currentParagraphIndex == paragraphCount)
            {
                currentTopicIndex++; // Bir sonraki konuya geçmek için indeksi artýrýn
                currentParagraphIndex = 0; // Paragraf indeksini sýfýrlayýn

                Debug.LogWarning("Ýlk konu tamamlandý"); // Uyarý: Ýlk konu tamamlandý

                // Eðer currentTopicIndex, konu sayýsýna eþitse, bu ünite tamamlandý demektir
                if (currentTopicIndex == topicCount)
                {
                    currentUnitIndex++; // Bir sonraki üniteye geçmek için indeksi artýrýn
                    currentTopicIndex = 0; // Konu indeksini sýfýrlayýn

                    Debug.LogWarning("Ýlk ünite tamamlandý"); // Uyarý: Ýlk ünite tamamlandý

                    // Eðer currentUnitIndex, ünite sayýsýna eþitse, müfredat tamamlandý demektir
                    if (currentUnitIndex == unitCount)
                    {
                        currentUnitIndex = 0; // Ünite indeksini sýfýrlayýn
                        Debug.Log("Müfredat tamamlandý"); // Bilgi: Müfredat tamamlandý
                    }
                }
            }
            Debug.Log("index: " + currentParagraphIndex);
        }
        topicNameText.text = $"{curriculum.unitesData[currentUnitIndex].topicsData[currentTopicIndex].topicName}:";
        unitNameText.text = $"{curriculum.unitesData[currentUnitIndex].uniteName}:";
    }
    // Önceki sayfaya geç
    public void PrevPage()
    {
        for (int i = paragraphTexts.Length - 1; i >= 0; i--)
        {
            currentParagraphIndex--; // Bir önceki paragrafa geçmek için indeksi azalt

            if (currentParagraphIndex < 0)
            {
                currentTopicIndex--; // Bir önceki konuya geçmek için indeksi azalt
                currentParagraphIndex = curriculum.unitesData[0].topicsData[0].paragraphs.Length - 1; // bir önceki konuya geçince paragraf indeksini de en sondan baþlat

                Debug.LogWarning("Ýlk paragraflar ibtti");

                if (currentTopicIndex < 0)
                {
                    currentUnitIndex--; // Bir önceki üniteye geçmek için indeksi azalt
                    currentTopicIndex = curriculum.unitesData[0].topicsData.Length - 1; // bir önceki üniteye geçince konu indeksini de en sondan baþlat

                    Debug.LogWarning("Ýlk konu bitti");

                    if (currentUnitIndex < 0)
                    {
                        currentUnitIndex = curriculum.unitesData.Length - 1; // ünite indeksi 0 olursa en son üniteye gönder
                        Debug.LogWarning("Ýlk ünite bitti");
                    }
                }
            }
            paragraphTexts[i].text = curriculum.unitesData[currentUnitIndex].topicsData[currentTopicIndex].paragraphs[currentParagraphIndex];

        }
        topicNameText.text = $"{curriculum.unitesData[currentUnitIndex].topicsData[currentTopicIndex].topicName}:";
        unitNameText.text = $"{curriculum.unitesData[currentUnitIndex].uniteName}:";
    }
}
