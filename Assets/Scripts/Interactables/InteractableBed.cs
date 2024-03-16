using UnityEngine;

public class InteractableBed : InteractableObject
{

    [Header("Referances")]
    [SerializeField] private PlayerSleepManager _sleepManager;
    [SerializeField] private InGameTimeManage inGameTimeManage;

    public override void ShowInfo()
    {
        base.ShowInfo();

        _sleepManager.IsSleepingArea = true;
        Debug.Log("Uyuma alanýndasýn");
    }
    public override void HideInfo()
    {
        base.HideInfo();

        _sleepManager.IsSleepingArea = false;
        Debug.Log("Uyuma alanýnda deðilsin");
    }
    public override void Interact()
    {
        // karakter uyuyabilir durumduysa, uyumasý gereken alandaysa ve e tuþuna basarsa uyusun
        if (_sleepManager.GetCanSleep() && _sleepManager.IsSleepingArea)
        {
            Debug.Log("E ye bastý ve Uyudu.");
            inGameTimeManage.IncreaseNumberOfDays();
            _sleepManager.SetCanSleep(false);
        }
    }
}
