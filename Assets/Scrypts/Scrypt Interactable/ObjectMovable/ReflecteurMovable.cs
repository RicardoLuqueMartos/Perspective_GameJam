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
        RBPlayer.instance.LockMovements();
        UiManager.instance.selectedReflector = this;
        UiManager.instance.Displaycontectuel();
        UiManager.instance.DisplayReflectorQuitText(true);
    }

    public void LeaveInteract()
    {
        _isControlled = false;
        UiManager.instance.selectedReflector = null;
        RBPlayer.instance.UnlockMovements();
        UiManager.instance.Hidecontectuel();
        UiManager.instance.DisplayReflectorQuitText(false);
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
            
            if ( Input.GetKey(KeyCode.T) )
            {

                RotateUp();
                Debug.Log("RotateUp");
            }
            if ( Input.GetKey(KeyCode.G))
            {
                RotateDown();
                Debug.Log("RotateDown");
            }

            if (Input.GetKey(KeyCode.F))
            {
                RotateLeft();
                Debug.Log("RotateLeft");
            }
            if (Input.GetKey(KeyCode.H))
            {
                RotateRight();
                Debug.Log("RotateRight");
            }

        }
    }

    public void RotateLeft()
    {
        cylindrePivot.gameObject.transform.Rotate(0, -moveForce * Time.deltaTime, 0);
    }

    public void RotateRight()
    {
        cylindrePivot.gameObject.transform.Rotate(0, moveForce * Time.deltaTime, 0);
    }
    public void RotateUp()
    {
        // Pour g�rer les angles n�gatifs

        float currentZ = sphereInclinaison.transform.localEulerAngles.z;
        if (currentZ > 180) currentZ -= 360;
        float newZ = Mathf.Clamp(currentZ + moveForce * Time.deltaTime, -90, 90);
        sphereInclinaison.transform.localEulerAngles = new Vector3(
            sphereInclinaison.transform.localEulerAngles.x,
            sphereInclinaison.transform.localEulerAngles.y,
            newZ
        );
    }

    public void RotateDown()
    {
        // Pour g�rer les angles n�gatifs

        float currentZ = sphereInclinaison.transform.localEulerAngles.z;
        if (currentZ > 180) currentZ -= 360;
        float newZ = Mathf.Clamp(currentZ - moveForce * Time.deltaTime, -90, 90);
        sphereInclinaison.transform.localEulerAngles = new Vector3(
            sphereInclinaison.transform.localEulerAngles.x,
            sphereInclinaison.transform.localEulerAngles.y,
            newZ
        );
    }
}
