using UnityEngine;
using UnityEngine.UIElements;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float interactDistance = 1f;
    [SerializeField] private LayerMask interactable;
    private RaycastHit hit;
    [SerializeField] private IInteractable currentTarget;

    public bool isInteracting = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    // Update is called once per frame
    void Update()
    {
        //hit = new RaycastHit();
        //Physics.Raycast(transform.position , transform.forward, out hit, interactDistance, interactable);
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Physics.Raycast(ray, out hit, interactDistance, interactable);
        if (hit.collider != null)
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
    }
}
