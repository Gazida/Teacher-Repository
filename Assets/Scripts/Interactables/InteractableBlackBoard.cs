using TMPro;
using UnityEngine;

public class InteractableBlackBoard : InteractableObject
{
    [Header("Referances")]
    [SerializeField] private AttendanceManager attendanceManager;
    [SerializeField] private TeachingManager teachingManager;

    [Header("Set Active Objects")]
    [SerializeField] private TextMeshProUGUI uiInfoText;
    [SerializeField] private GameObject startLessonButton;

    public override void ShowInfo()
    {
        base.ShowInfo();

        if (attendanceManager.IsAttendanceCompleted)
        {
            uiInfoText.text = "Dersi Baþlat [E]";
        }
        if (!teachingManager.IsStartLesson)
        {
            uiInfoText.gameObject.SetActive(true);
        }
        teachingManager.IsTeacherLectureArea = true;
    }
    public override void HideInfo()
    {
        base.HideInfo();

        teachingManager.IsTeacherLectureArea = false;
        uiInfoText.gameObject.SetActive(false);
    }
    public override void Interact()
    {
        if (attendanceManager.IsAttendanceCompleted)
        {
            startLessonButton.SetActive(true);
            uiInfoText.gameObject.SetActive(false);
        }
        else
        {
            uiInfoText.text = "Yoklamayý Almadýn!";
        }
    }
}
