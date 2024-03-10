using UnityEngine;

public class PlayerSleepManager : MonoBehaviour
{
    [Header("Referances Scripts")]
    [SerializeField] private InGameTimeManage gameTimeManage;

    private bool canSleep = true; // ba�lang�� de�eri deneme ama�l� true verildi. daha sonra de�i�

    private void Update()
    {
        // Bu sadece �al��t���n� denemek i�in. As�l i�lem ba�ka scriptlerden bu scripte eri�ilerek yap�lacak.
        if (canSleep && Input.GetKeyDown("e"))
        {
            Debug.Log("E ye bast� ve Uyudu.");
            gameTimeManage.IncreaseNumberOfDays();
            //canSleep = false;
        }
        // Hafta sonlar� ders anlatma sistemi olmad��� i�in canSleep de�eri false olarak tak�l� kal�yor. Bunun i�in hafta sonlar�nda canSleep de�i�kenini burada true yapmam�z gerekiyor.
        if(gameTimeManage.CurrentNumberOfDay % 4 == 0)
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
