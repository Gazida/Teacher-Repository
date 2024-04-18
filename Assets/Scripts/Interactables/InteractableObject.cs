using StarterAssets;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour, IInteractable
{
    [SerializeField] private Outline outline;
    public virtual void Interact()
    {
        // Nesneyle etkile�im i�lemleri burada ger�ekle�tirilir
        Debug.Log("Nesneyle etkile�ime ge�ildi!");
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
