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
        Debug.Log("Uyuma alan�ndas�n");
    }
    public override void HideInfo()
    {
        base.HideInfo();

        _sleepManager.IsSleepingArea = false;
        Debug.Log("Uyuma alan�nda de�ilsin");
    }
    public override void Interact()
    {
        // karakter uyuyabilir durumduysa, uyumas� gereken alandaysa ve e tu�una basarsa uyusun
        if (_sleepManager.GetCanSleep() && _sleepManager.IsSleepingArea)
        {
            Debug.Log("E ye bast� ve Uyudu.");
            inGameTimeManage.IncreaseNumberOfDays();
            _sleepManager.SetCanSleep(false);
        }
    }
}
