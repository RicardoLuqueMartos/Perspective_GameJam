using TMPro;
using UnityEngine;

public class ReflectorConsole : MonoBehaviour
{
    public TMP_Text indexText;

    public void RotateLeft()
    {
        
    }

    public void RotateRight()
    {
        
    }
    public void RotateUp()
    {
        CameraConsole.instance.consoleInteract.LinkedReflectorsList[CameraConsole.instance.consoleInteract.ReflectorIndex]
            .RotateUp();
    }

    public void RotateDown()
    {
        CameraConsole.instance.consoleInteract.LinkedReflectorsList[CameraConsole.instance.consoleInteract.ReflectorIndex]
            .RotateDown();
    }

    public void NextReflector()
    {
        if (CameraConsole.instance.consoleInteract.ReflectorIndex == CameraConsole.instance.consoleInteract.LinkedReflectorsList.Count - 1)
            CameraConsole.instance.consoleInteract.ReflectorIndex = 0;
        else CameraConsole.instance.consoleInteract.ReflectorIndex++;
        
        DisplayIndex();
        AssignSelectedReflector();
    }

    public void PreviousReflector()
    {
        if (CameraConsole.instance.consoleInteract.ReflectorIndex == 0)
            CameraConsole.instance.consoleInteract.ReflectorIndex = CameraConsole.instance.consoleInteract.LinkedReflectorsList.Count - 1;
        else CameraConsole.instance.consoleInteract.ReflectorIndex--;
       
        DisplayIndex();
        AssignSelectedReflector();
    }

    public void DisplayIndex()
    {
        indexText.text = (CameraConsole.instance.consoleInteract.ReflectorIndex+1).ToString();
    }

    void AssignSelectedReflector()
    {
        UiManager.instance.selectedReflector = CameraConsole.instance.consoleInteract.LinkedReflectorsList[CameraConsole.instance.consoleInteract.ReflectorIndex];        
    }
}
