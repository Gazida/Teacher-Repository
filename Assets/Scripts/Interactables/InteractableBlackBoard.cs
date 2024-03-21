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
                uiInfoText.text = "Dersi Ba�lat [E]";
            }
            else
            {
                uiInfoText.text = "Teneff�stesin!";
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
        // E�er g�n say�s� 4'�n katlar�na denk gelmiyorsa yani hafta sonu de�ilse ders anlatabilsin
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
                uiInfoText.text = "Yoklamay� Almad�n!";
            }
        }
        else
        {
            uiInfoText.text = "Hafta Sonu Ders Anlatamazs�n!";
        }
    }
    public void StopLesson()
    {
        teachCamera.SetActive(false);
        playerFollowCamera.SetActive(true);

        firstPersonController.SetInteractSituation(false);
    }
}
