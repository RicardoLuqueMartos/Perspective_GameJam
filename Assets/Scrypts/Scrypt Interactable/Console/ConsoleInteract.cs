using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleInteract : MonoBehaviour, IInteractable
{
    public List<Camera> camerasList = new List<Camera>();
    public List<ReflecteurMovable> LinkedReflectorsList = new List<ReflecteurMovable>();

    [SerializeField] GameObject consoleUI;
    [SerializeField] string contextuelTXT;
    public int cameraIndex = 0;
    public int ReflectorIndex = 0;

    public bool _isControlled = false;
    public bool isControlled()
    {
        return _isControlled;
    }



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
        _isControlled = true;
    }

    public void LeaveInteract()
    {
    //    UiManager.instance.con.SetActive(false);
        _isControlled = false;
    }
    
}
