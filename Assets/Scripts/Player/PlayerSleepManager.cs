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
        // Hafta sonlarý ders anlatma sistemi olmadýðý için canSleep deðeri false olarak takýlý kalýyor. Bunun için hafta sonlarýnda canSleep deðiþkenini burada true yapmamýz gerekiyor.
        if (gameTimeManage.CurrentNumberOfDay != 0 && ((gameTimeManage.CurrentNumberOfDay + 1) % 4) == 0)
        {
            canSleep = true;
        }
    }

    /*
    public override void Interact()
    {
        // karakter uyuyabilir durumduysa, uyumasý gereken alandaysa ve e tuþuna basarsa uyusun
        if (canSleep && isSleepingArea)
        {
            Debug.Log("E ye bastý ve Uyudu.");
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
