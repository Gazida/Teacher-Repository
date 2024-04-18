using UnityEngine;

public class InteractableAttendanceBook : InteractableObject
{
    [Header("Rferances Scripts")]
    [SerializeField] private AttendanceManager attendanceManager;
    [SerializeField] private InGameTimeManage inGameTimeManage;

    [Header("Cameras")]
    [SerializeField] private GameObject playerFollowCamera;
    [SerializeField] private GameObject attendanceFollowCamera;

    [Header("For UI")]
    [SerializeField] private GameObject infoPressE;

    private void Update()
    {
        if (attendanceManager.IsAttendanceCompleted)
        {
            playerFollowCamera.SetActive(true);
            attendanceFollowCamera.SetActive(false);
        }
    }
    public override void ShowInfo()
    {
        base.ShowInfo();
        infoPressE.SetActive(true);
    }
    public override void HideInfo()
    {
        base.HideInfo();
        infoPressE.SetActive(false);
    }
    public override void Interact()
    {
        if((inGameTimeManage.CurrentNumberOfDay + 1) % 4 != 0)
        {
            playerFollowCamera.SetActive(false);
            attendanceFollowCamera.SetActive(true);

            attendanceManager.StartAttendance();
        }
    }
}
