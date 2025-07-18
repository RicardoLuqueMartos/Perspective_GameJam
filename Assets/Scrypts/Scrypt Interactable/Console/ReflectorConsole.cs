using TMPro;
using UnityEngine;

public class ReflectorConsole : MonoBehaviour
{
    public TMP_Text indexText;

    public void RotateLeft()
    {
     //   CameraConsole.instance.consoleInteract.LinkedReflectorsList[CameraConsole.instance.consoleInteract.ReflectorIndex].
    }

    public void RotateRight()
    {

    }
    public void RotateUp()
    {

    }

    public void RotateDown()
    {

    }

    public void NextReflector()
    {
        CameraConsole.instance.consoleInteract.ReflectorIndex = (CameraConsole.instance.consoleInteract.ReflectorIndex + 1) 
            % CameraConsole.instance.consoleInteract.LinkedReflectorsList.Count;
        DisplayIndex();
    }

    public void PreviousReflector()
    {
        CameraConsole.instance.consoleInteract.ReflectorIndex = (CameraConsole.instance.consoleInteract.ReflectorIndex - 1)
            % CameraConsole.instance.consoleInteract.LinkedReflectorsList.Count;
        DisplayIndex();
    }

    public void DisplayIndex()
    {
        indexText.text = (CameraConsole.instance.consoleInteract.ReflectorIndex+1).ToString();
    }
}
