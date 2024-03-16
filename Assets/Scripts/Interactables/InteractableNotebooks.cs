using System;
using UnityEngine;
using UnityEngine.UI;

public class InteractableNotebooks : InteractableObject
{
    [Header("Other")]
    public int studentId;

    [Header("Referances Scripts")]
    [SerializeField] private InGameTimeManage inGameTimeManage;
    [SerializeField] private PlayerHomeworkCheckingManager playerHomeworkCheckingManager;

    [Header("Set Active Objects")]
    [SerializeField] private GameObject homeworkCheckingSystem;
    [SerializeField] private GameObject homeworkCheckingCamera;
    [SerializeField] private GameObject playerFollowCamera;


    public override void Interact()
    {
        base.Interact();

        // Ýlk gün deðilse ve haftanýn 1. günü ise ödev kontrol sistemini aktifleþtir
        if(inGameTimeManage.CurrentNumberOfDay != 0 && ((inGameTimeManage.CurrentNumberOfDay + 1) % 4) == 1)
        {
            homeworkCheckingSystem.SetActive(true);
            homeworkCheckingCamera.SetActive(true);
            playerFollowCamera.SetActive(false);

            playerHomeworkCheckingManager.StudentId = studentId;
        }
    }

    public override void ShowInfo()
    {
        base.ShowInfo();
    }
    public override void HideInfo()
    {
        base.HideInfo();
    }
}
