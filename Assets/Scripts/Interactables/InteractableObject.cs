using StarterAssets;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour, IInteractable
{
    [SerializeField] private Outline outline;
    public virtual void Interact()
    {
        // Nesneyle etkileþim iþlemleri burada gerçekleþtirilir
        Debug.Log("Nesneyle etkileþime geçildi!");
    }

    public virtual void ShowInfo()
    {
        outline.enabled = true;
    }
    public virtual void HideInfo()
    {
        outline.enabled = false;
    }
}
