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
        // karakter uyuyabilir durumduysa, uyumasý gereken alandaysa ve e tuþuna basarsa uyusun
        if (canSleep && isSleepingArea && Input.GetKeyDown("e"))
        {
            Debug.Log("E ye bastý ve Uyudu.");
            gameTimeManage.IncreaseNumberOfDays();
            canSleep = false;
        }
        // Hafta sonlarý ders anlatma sistemi olmadýðý için canSleep deðeri false olarak takýlý kalýyor. Bunun için hafta sonlarýnda canSleep deðiþkenini burada true yapmamýz gerekiyor.
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
