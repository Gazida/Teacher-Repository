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
        // Hafta sonlar� ders anlatma sistemi olmad��� i�in canSleep de�eri false olarak tak�l� kal�yor. Bunun i�in hafta sonlar�nda canSleep de�i�kenini burada true yapmam�z gerekiyor.
        if (gameTimeManage.CurrentNumberOfDay != 0 && ((gameTimeManage.CurrentNumberOfDay + 1) % 4) == 0)
        {
            canSleep = true;
        }
    }

    /*
    public override void Interact()
    {
        // karakter uyuyabilir durumduysa, uyumas� gereken alandaysa ve e tu�una basarsa uyusun
        if (canSleep && isSleepingArea)
        {
            Debug.Log("E ye bast� ve Uyudu.");
            gameTimeManage.IncreaseNumberOfDays();
            canSleep = false;
        }
    }
    public override void ShowInfo()
    {
        base.ShowInfo();
        isSleepingArea = true;
    }
    public override void HideInfo()
    {
        base.HideInfo();
        isSleepingArea = false;
    }
    */

    public bool GetCanSleep()
    {
        return canSleep;
    }
    public void SetCanSleep(bool newValue)
    {
        canSleep = newValue;
    }
}
