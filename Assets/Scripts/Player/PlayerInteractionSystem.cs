using UnityEngine;

public class PlayerInteractionSystem : MonoBehaviour
{
    [SerializeField] private float rayDistance;
    private IInteractable currentInteractable; // Þu an etkileþimde olduðumuz nesne
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        CheckRaycast();
        CheckInput();
    }

    private void CheckRaycast()
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                if (interactable != currentInteractable)
                {
                    HideCurrentInteractableInfo();
                    currentInteractable = interactable;
                    currentInteractable.ShowInfo();
                }
            }
            else
            {
                HideCurrentInteractableInfo();
            }
        }
        else
        {
            HideCurrentInteractableInfo();
        }
    }

    private void HideCurrentInteractableInfo()
    {
        if (currentInteractable != null)
        {
            currentInteractable.HideInfo();
            currentInteractable = null;
        }
    }

    private void CheckInput()
    {
        if (currentInteractable != null && Input.GetKeyDown(KeyCode.E))
        {
            currentInteractable.Interact();
        }
    }
}
