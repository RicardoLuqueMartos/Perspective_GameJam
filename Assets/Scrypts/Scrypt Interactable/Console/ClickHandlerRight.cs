using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickHandlerRight : MonoBehaviour, IUpdateSelectedHandler, IPointerDownHandler, IPointerUpHandler
{
    public bool isPressed;

    // Start is called before the first frame update
    public void OnUpdateSelected(BaseEventData data)
    {
        if (isPressed)
        {
            CameraConsole.instance.consoleInteract.LinkedReflectorsList[CameraConsole.instance.consoleInteract.ReflectorIndex]
            .RotateRight();
        }
    }
    public void OnPointerDown(PointerEventData data)
    {
        isPressed = true;
    }
    public void OnPointerUp(PointerEventData data)
    {
        isPressed = false;
    }
}
