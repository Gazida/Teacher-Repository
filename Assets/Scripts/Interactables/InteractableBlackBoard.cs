using UnityEngine;

public class InteractableBlackBoard : InteractableObject
{
    [Header("Referances")]
    [SerializeField] private TeachingManager teachingManager;

    public override void ShowInfo()
    {
        base.ShowInfo();

        teachingManager.IsTeacherLectureArea = true;
        Debug.Log("Ders anlatma alan�ndas�n");
    }
    public override void HideInfo()
    {
        base.HideInfo();

        teachingManager.IsTeacherLectureArea = false;
        Debug.Log("Ders anlatma alan�nda de�ilsin");
    }
    public override void Interact()
    {
        base.Interact();
    }
}
