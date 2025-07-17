using UnityEngine;

public interface IInteractable
{

    //method de detection par raycast
    public void IsInteractable(RaycastHit hit);

    public void Interact();

    public void LeaveInteract();
}