using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float interactDistance = 1f;
    [SerializeField] private float interactDistanceMax = 1f;
    [SerializeField] private LayerMask interactable;
    private RaycastHit hit;
    private IInteractable currentTarget;
    public IInteractable interactingTarget;

    public bool isInteracting = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    // Update is called once per frame
    void Update()
    {
        //hit = new RaycastHit();
        //Physics.Raycast(transform.position , transform.forward, out hit, interactDistance, interactable);
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Physics.Raycast(ray, out hit, interactDistance, interactable);
        if (hit.collider == null && interactingTarget != null && Input.GetKeyDown(KeyCode.E))
        {
            interactingTarget.LeaveInteract();
            interactingTarget = null;
            return;
        }
        else if (hit.collider != null)
        {
            
                currentTarget = hit.collider.GetComponent<IInteractable>();
            
            if (currentTarget != null)
            {
                //Debug.Log(hit.transform.name);
                currentTarget.IsInteractable(hit);
                if (Input.GetKeyDown(KeyCode.E))
                {


                    if (!currentTarget.isControlled())
                    {
                        if (interactingTarget != null)
                        {
                            interactingTarget.LeaveInteract();
                        }
                        currentTarget.Interact(this);
                    }
                    else
                    {
                        currentTarget.LeaveInteract();
                    }
                }
            }
            else UiManager.instance.Hidecontectuel();
        }
        else if (UiManager.instance != null && !isInteracting)
        {
            currentTarget = null;
            UiManager.instance.contectuelInteracted("");
            UiManager.instance.Hidecontectuel();
        }

        if (isInteracting && interactingTarget != null &&
            Vector3.Distance(this.transform.position, ((MonoBehaviour)interactingTarget).transform.position) > interactDistanceMax)
        {
            interactingTarget.LeaveInteract();
            interactingTarget = null;
        }
    }
}
