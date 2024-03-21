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

    [Header("For UI")]
    [SerializeField] private GameObject infoPressE;


    private void OnEnable()
    {
        PlayerHomeworkCheckingManager.stopCheckingHomework += StopInteracting;
    }
    private void OnDisable()
    {
        PlayerHomeworkCheckingManager.stopCheckingHomework -= StopInteracting;
    }
    public void StopInteracting()
    {
        homeworkCheckingSystem.SetActive(false);
        homeworkCheckingCamera.SetActive(false);
        playerFollowCamera.SetActive(true);
    }

    public override void Interact()
    {
        base.Interact();

        // Ýlk gün deðilse ve haftanýn 1. günü ise ödev kontrol sistemini aktifleþtir
        if (inGameTimeManage.CurrentNumberOfDay != 0 && ((inGameTimeManage.CurrentNumberOfDay + 1) % 4) == 1)
        {
            homeworkCheckingSystem.SetActive(true);
            homeworkCheckingCamera.SetActive(true);
            playerFollowCamera.SetActive(false);

            Debug.Log("Kitap inceleniyor");

            playerHomeworkCheckingManager.StudentId = studentId;
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
}
