using TMPro;
using UnityEngine;

public class CalendarUI : MonoBehaviour, IGameTimeObserver
{
    [SerializeField] InGameTimeManage inGameTimeManage;

    [Space(10)]
    [SerializeField] private TextMeshProUGUI dayText; 
    [SerializeField] private TextMeshProUGUI weekText; 
    [SerializeField] private TextMeshProUGUI monthText; 
    [SerializeField] private TextMeshProUGUI yearText;

    private void Awake()
    {
        inGameTimeManage.Attach(this); // Bu sýnýf IGameTimeObserver interface'ini içeriyor. Bu metot'a "this" göndererek bu class'ý gözlemci yapýyoruz.(Observer Pattern)

        dayText.text = "01";
        weekText.text = "01";
        monthText.text = "01";
        yearText.text = "2023";
    }

    public void NextDay()
    {
        dayText.text = inGameTimeManage.CurrentNumberOfDay.ToString();
    }

    public void NextMonth()
    {
        monthText.text = inGameTimeManage.CurrentNumberOfMonth.ToString();
    }

    public void NextWeek()
    {
        weekText.text = inGameTimeManage.CurrentNumberOfWeek.ToString();
    }

    public void NextYear()
    {
        yearText.text = inGameTimeManage.CurrentNumberOfYear.ToString();
    }
}
