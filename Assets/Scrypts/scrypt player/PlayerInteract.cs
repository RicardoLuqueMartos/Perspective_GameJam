using UnityEngine;
using UnityEngine.UIElements;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float interactDistance = 1f;
    [SerializeField] private LayerMask interactable;
    private RaycastHit hit;
    private IInteractable currentTarget;
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    // Update is called once per frame
    void Update()
    {
        //hit = new RaycastHit();
        Physics.Raycast(transform.position , transform.forward, out hit, interactDistance, interactable);
        if (hit.collider != null)
        {

                currentTarget = hit.collider.GetComponent<IInteractable>();
                currentTarget.IsInteractable(hit);
                if (Input.GetKeyDown(KeyCode.E) )

                {
                    if (!currentTarget.isControlled())
                    { 
                        currentTarget.Interact();
                    }
                    else 
                    {
                    currentTarget.LeaveInteract();
                    }
                }

        }
        else if (UiManager.instance != null)
        {
            currentTarget = null;
            UiManager.instance.contectuelInteracted("");
        }
    }
}
