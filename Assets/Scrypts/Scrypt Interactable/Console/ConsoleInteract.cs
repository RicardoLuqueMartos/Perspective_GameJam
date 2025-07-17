using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleInteract : MonoBehaviour, IInteractable
{
    public List<Camera> camerasList = new List<Camera>();
    [SerializeField] CameraConsole consoleControle;
    [SerializeField] GameObject consoleUI;
    [SerializeField] string contextuelTXT;
    public int cameraIndex = 0;
    public void IsInteractable(RaycastHit hit)
    {
        if (hit.collider != null)
        {
            UiManager.instance.contectuelInteracted(contextuelTXT);
        }
    }

    public void Interact()
    {
     //   consoleUI.SetActive(true);
        UiManager.instance.OpenCameraConsole(this);
    }

    public void LeaveInteract()
    {
        consoleUI.SetActive(false);
    }
    
}
