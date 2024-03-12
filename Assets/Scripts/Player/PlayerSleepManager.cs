using UnityEngine;

public class PlayerSleepManager : MonoBehaviour
{
    [Header("Referances Scripts")]
    [SerializeField] private InGameTimeManage gameTimeManage;

    private bool canSleep = false;
    private bool isSleepingArea;
    public bool IsSleepingArea
    {
        get { return isSleepingArea; }
        set { isSleepingArea = value; }
    }

    private void Update()
    {
        // karakter uyuyabilir durumduysa, uyumas� gereken alandaysa ve e tu�una basarsa uyusun
        if (canSleep && isSleepingArea && Input.GetKeyDown("e"))
        {
            Debug.Log("E ye bast� ve Uyudu.");
            gameTimeManage.IncreaseNumberOfDays();
            canSleep = false;
        }
        // Hafta sonlar� ders anlatma sistemi olmad��� i�in canSleep de�eri false olarak tak�l� kal�yor. Bunun i�in hafta sonlar�nda canSleep de�i�kenini burada true yapmam�z gerekiyor.
        if (gameTimeManage.CurrentNumberOfDay % 4 == 0 && gameTimeManage.CurrentNumberOfDay != 0)
        {
            canSleep = true;
        }
    }

    public bool GetCanSleep()
    {
        return canSleep;
    }
    public void SetCanSleep(bool newValue)
    {
        canSleep = newValue;
    }
}
