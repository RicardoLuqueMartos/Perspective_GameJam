using UnityEngine;

public class ConsoleInteract : MonoBehaviour, IInteractable
{
    [SerializeField] ConsoleControle consoleControle;
    [SerializeField] GameObject consoleUI;
    public void IsInteractable(RaycastHit hit)
    {
        if (hit.collider != null)
        {

        }
    }

    public void Interact()
    {
        consoleUI.SetActive(!consoleUI.activeSelf);
    }
    
}
