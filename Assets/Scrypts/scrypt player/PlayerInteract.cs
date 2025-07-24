using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float interactDistance = 1f; 
    [SerializeField] private float SphereDiameter = .1f;
    [SerializeField] private float interactDistanceMax = 1f;
    [SerializeField] private LayerMask interactable;

    private RaycastHit hit;
    private IInteractable currentTarget;
    public IInteractable interactingTarget;

    public bool isInteracting = false;
    Ray ray;

    private void Update()
    {
     //   ray = mainCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
    ////    ray.direction = transform.forward;
        RayDetectInteractable();
    }
    void FixedUpdate()
    {

        LeaveInteractByDistance();
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(ray);
    }

    void RayDetectInteractable()
    {
        if (interactingTarget != null && Input.GetKeyDown(KeyCode.E) /*PlayerController.instance.IsInteracting*/)
        {
            LeaveInteractByKey();            
            return;
        }
        RaycastHit hit;
        if (Physics.SphereCast(transform.position,
            SphereDiameter, transform.forward, out hit, interactDistance, interactable))
            {
             if (hit.collider != null)
            {
                currentTarget = hit.collider.GetComponent<IInteractable>();

                if (currentTarget != null)
                {
                 //   Debug.Log(hit.transform.name);
                    currentTarget.IsInteractable(hit);
                    if (Input.GetKeyDown(KeyCode.E)/* PlayerController.instance.IsInteracting*/)
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
                else
                {
                    Debug.Log("Hidecontectuel " + hit.transform.name);
                    UiManager.instance.Hidecontectuel();
                }
            }
        }
        
        else if (UiManager.instance != null && !isInteracting)
        {
            currentTarget = null;
            UiManager.instance.contectuelInteracted("");
            UiManager.instance.Hidecontectuel();
        }
    }

    void LeaveInteractByKey()
    {
        interactingTarget.LeaveInteract();
        interactingTarget = null;
    }

    void LeaveInteractByDistance()
    {
        if (isInteracting && interactingTarget != null &&
            Vector3.Distance(this.transform.position, ((MonoBehaviour)interactingTarget).transform.position) > interactDistanceMax)
        {
            Debug.Log("Distance LeaveInteract");
            interactingTarget.LeaveInteract();
            interactingTarget = null;
        }
    }
}
