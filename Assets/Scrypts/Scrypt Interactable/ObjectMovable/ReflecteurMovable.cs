using TMPro;
using UnityEngine;

public class ReflecteurMovable : MonoBehaviour, IInteractable
{
    [SerializeField] string contextuelTXT;
    [SerializeField] GameObject sphereInclinaison;
    [SerializeField] GameObject cylindrePivot;
    public bool _isControlled;
    public int moveForce;

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
        _isControlled = true;
    }

    public void LeaveInteract()
    {
        _isControlled = false;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_isControlled)
        {
            float currentZ = sphereInclinaison.transform.localEulerAngles.z;
            if (currentZ > 180) currentZ -= 360;
            if ( Input.GetKey(KeyCode.T) )
            {
                 // Pour gérer les angles négatifs

                float newZ = Mathf.Clamp(currentZ + moveForce * Time.deltaTime, -90, 90);
                sphereInclinaison.transform.localEulerAngles = new Vector3(
                    sphereInclinaison.transform.localEulerAngles.x,
                    sphereInclinaison.transform.localEulerAngles.y,
                    newZ
                );

            }
            if ( Input.GetKey(KeyCode.G))
            {
                float newZ = Mathf.Clamp(currentZ  -moveForce * Time.deltaTime, -90, 90);
                sphereInclinaison.transform.localEulerAngles = new Vector3(
                    sphereInclinaison.transform.localEulerAngles.x,
                    sphereInclinaison.transform.localEulerAngles.y,
                    newZ
                );
            }

            if (Input.GetKey(KeyCode.F))
            {
                cylindrePivot.gameObject.transform.Rotate(0, -moveForce * Time.deltaTime, 0);
            }
            if (Input.GetKey(KeyCode.H))
            {
                cylindrePivot.gameObject.transform.Rotate(0, moveForce * Time.deltaTime, 0);
            }

        }
    }

}
