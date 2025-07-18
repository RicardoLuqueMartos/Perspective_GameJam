using System;
using UnityEngine;


public enum ItemTypeEnum { None, Tesseract }
public class Item : MonoBehaviour, IInteractable
{
    public ItemTypeEnum Type;
    [SerializeField] string contextuelTXT; 
    bool _isControlled = false;

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
        GameProgressManager.instance.PlayerGetTesseract();
        LeaveInteract();
    }

    public void LeaveInteract()
    {
        Destroy(gameObject);
    }
}
