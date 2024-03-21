using StarterAssets;
using TMPro;
using UnityEngine;

public class InteractableBlackBoard : InteractableObject
{
    [Header("Referances")]
    [SerializeField] private FirstPersonController firstPersonController;
    [SerializeField] private InGameTimeManage inGameTimeManage;
    [SerializeField] private AttendanceManager attendanceManager;
    [SerializeField] private TeachingManager teachingManager;

    [Header("Set Active Objects")]
    [SerializeField] private GameObject teachCamera;
    [SerializeField] private GameObject playerFollowCamera;
    [SerializeField] private TextMeshProUGUI uiInfoText;

    public override void ShowInfo()
    {
        base.ShowInfo();

        if (teachingManager.IsLessonsFinished)
        {
            uiInfoText.text = "Ders Bitti!";
        }
        else
        {
            if (teachingManager.CanTeachLesson)
            {
                uiInfoText.text = "Dersi Baþlat [E]";
            }
            else
            {
                uiInfoText.text = "Teneffüstesin!";
            }
        }

        if (!firstPersonController.GetInteractSiuation())
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
        // Eðer gün sayýsý 4'ün katlarýna denk gelmiyorsa yani hafta sonu deðilse ders anlatabilsin
        if (((inGameTimeManage.CurrentNumberOfDay + 1) % 4) != 0)
        {
            if (attendanceManager.IsAttendanceCompleted)
            {
                if (teachingManager.CanTeachLesson)
                {
                    teachingManager.StartLesson();

                    uiInfoText.gameObject.SetActive(false);

                    teachCamera.SetActive(true);
                    playerFollowCamera.SetActive(false);

                    firstPersonController.SetInteractSituation(true);
                }
            }
            else
            {
                uiInfoText.text = "Yoklamayý Almadýn!";
            }
        }
        else
        {
            uiInfoText.text = "Hafta Sonu Ders Anlatamazsýn!";
        }
    }
    public void StopLesson()
    {
        teachCamera.SetActive(false);
        playerFollowCamera.SetActive(true);

        firstPersonController.SetInteractSituation(false);
    }
}
