using UnityEngine;

public class SleepingAreaTrigger : MonoBehaviour
{
    [Header("Referances Scripts")]
    [SerializeField] private PlayerSleepManager playerSleepManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerSleepManager.IsSleepingArea = true;
            Debug.Log("Uyuma alanýndasýn");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerSleepManager.IsSleepingArea = false;
        }
    }
}
