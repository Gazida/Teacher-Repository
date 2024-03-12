using UnityEngine;

public class LectureAreaTrigger : MonoBehaviour
{
    [Header("Referances")]
    [SerializeField] private TeachingManager teachingManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            teachingManager.IsTeacherLectureArea = true;
            Debug.Log("Ders anlatma alanýndasýn");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            teachingManager.IsTeacherLectureArea = false;
        }
    }
}
