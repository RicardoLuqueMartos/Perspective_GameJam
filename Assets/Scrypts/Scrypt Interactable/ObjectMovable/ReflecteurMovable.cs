using TMPro;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour, IInteractable
{
    [SerializeField] string contextuelTXT;
    [SerializeField] GameObject reflecteur;
    public bool isControlled;
    public int moveForce;
    public void IsInteractable(RaycastHit hit)
    {
        if (hit.collider != null)
        {
            UiManager.instance.contectuelInteracted(contextuelTXT);

            if (Input.GetKeyDown(KeyCode.E) && !isControlled)
                
            {
                Interact();
            }

            if (Input.GetKeyDown(KeyCode.E) && isControlled)
            {
                LeaveInteract();
            }
        }
    }
    public void Interact()
    {
        isControlled = true;
    }

    public void LeaveInteract()
    {
        isControlled = false;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isControlled)
        {
            if ( Input.GetKey(KeyCode.T) )
            {
                reflecteur.gameObject.transform.Rotate(0, 0, moveForce * Time.deltaTime);
                if (reflecteur.gameObject.transform.rotation.z < 90)
                {
                                       reflecteur.gameObject.transform.rotation = Quaternion.Euler(0, 0, 90);
                }
            }
            if ( Input.GetKey(KeyCode.G))
            {
                reflecteur.gameObject.transform.Rotate(0, 0, -moveForce * Time.deltaTime);
                if (reflecteur.gameObject.transform.rotation.z > -90)
                {
                    reflecteur.gameObject.transform.rotation = Quaternion.Euler(0, 0, -90);
                }
            }

            if (Input.GetKey(KeyCode.F))
            {
                reflecteur.gameObject.transform.Rotate(moveForce * Time.deltaTime, 0, 0);
            }
            if (Input.GetKey(KeyCode.H))
            {
                reflecteur.gameObject.transform.Rotate(-moveForce * Time.deltaTime, 0, 0);
            }

        }
    }

}
