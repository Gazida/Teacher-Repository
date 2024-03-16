using UnityEngine;

public class InteractableBlackBoard : InteractableObject
{
    [Header("Referances")]
    [SerializeField] private TeachingManager teachingManager;

    public override void ShowInfo()
    {
        base.ShowInfo();

        teachingManager.IsTeacherLectureArea = true;
        Debug.Log("Ders anlatma alanýndasýn");
    }
    public override void HideInfo()
    {
        base.HideInfo();

        teachingManager.IsTeacherLectureArea = false;
        Debug.Log("Ders anlatma alanýnda deðilsin");
    }
    public override void Interact()
    {
        base.Interact();
    }
}
