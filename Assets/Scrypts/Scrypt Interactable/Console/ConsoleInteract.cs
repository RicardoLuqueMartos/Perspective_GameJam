using UnityEngine;

public class ConsoleInteract : MonoBehaviour, IInteractable
{
    [SerializeField] ConsoleControle consoleControle;
    [SerializeField] GameObject consoleUI;
    [SerializeField] string contextuelTXT;
    public void IsInteractable(RaycastHit hit)
    {
        if (hit.collider != null)
        {
            UiManager.Instance.contectuelInteracted(contextuelTXT);
        }
    }

    public void Interact()
    {
        consoleUI.SetActive(true);
    }

    public void LeaveInteract();
    {
        consoleUI.SetActive(false);
    }
    
}
