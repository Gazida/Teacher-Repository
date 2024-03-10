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
