using UnityEngine;

public class PlayerSleepManager : MonoBehaviour
{
    [Header("Referances Scripts")]
    [SerializeField] private InGameTimeManage gameTimeManage;

    private bool canSleep = true; // baþlangýç deðeri deneme amaçlý true verildi. daha sonra deðiþ

    private void Update()
    {
        // Bu sadece çalýþtýðýný denemek için. Asýl iþlem baþka scriptlerden bu scripte eriþilerek yapýlacak.
        if (canSleep && Input.GetKeyDown("e"))
        {
            Debug.Log("E ye bastý ve Uyudu.");
            gameTimeManage.IncreaseNumberOfDays();
            //canSleep = false;
        }
        // Hafta sonlarý ders anlatma sistemi olmadýðý için canSleep deðeri false olarak takýlý kalýyor. Bunun için hafta sonlarýnda canSleep deðiþkenini burada true yapmamýz gerekiyor.
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
