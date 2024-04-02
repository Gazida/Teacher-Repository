using UnityEngine;
using DG.Tweening;

public class InteractableDoor : InteractableObject, IInteractable
{
    [SerializeField] private Transform pivot;
    [Header("For UI")]
    [SerializeField] private GameObject infoPressE;

    private bool isOpen;

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
        base.Interact();
        
        if(!isOpen)
        {
            pivot.DORotate(new Vector3(0, 120, 0), 1.5f);
        }
        else
        {
            pivot.DORotate(Vector3.zero, 1.5f);
        }
        isOpen = !isOpen;
    }
}
