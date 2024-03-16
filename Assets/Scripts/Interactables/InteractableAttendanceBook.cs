using UnityEngine;

public class InteractableAttendanceBook : InteractableObject
{
    [Header("Rferances Scripts")]
    [SerializeField] private AttendanceManager attendanceManager;

    [Header("Cameras")]
    [SerializeField] private GameObject playerFollowCamera;
    [SerializeField] private GameObject attendanceFollowCamera;

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

        Debug.Log("Yoklama alanýndasýn");
    }
    public override void HideInfo()
    {
        base.HideInfo();

        Debug.Log("Yoklama alanýnda deðilsin");
    }
    public override void Interact()
    {
        playerFollowCamera.SetActive(false);
        attendanceFollowCamera.SetActive(true);
    }
}
