using UnityEngine;

public class ConsoleInteract : MonoBehaviour, IInteractable
{
    [SerializeField] CameraConsole consoleControle;
    [SerializeField] GameObject consoleUI;
    [SerializeField] string contextuelTXT;
    public void IsInteractable(RaycastHit hit)
    {
        if (hit.collider != null)
        {
            UiManager.instance.contectuelInteracted(contextuelTXT);
        }
    }

    public void Interact()
    {
        consoleUI.SetActive(true);
    }

    public void LeaveInteract()
    {
        consoleUI.SetActive(false);
    }
    
}
