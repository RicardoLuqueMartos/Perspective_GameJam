using UnityEngine;

public interface IInteractable
{
    public bool isControlled();
    //method de detection par raycast
    public void IsInteractable(RaycastHit hit);

    public void Interact();

    public void LeaveInteract();
}