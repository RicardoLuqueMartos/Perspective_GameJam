using TMPro;
using UnityEngine;

public class ReflecteurMovable : MonoBehaviour, IInteractable
{
    [SerializeField] string contextuelTXT;
    [SerializeField] GameObject sphereInclinaison;
    [SerializeField] GameObject cylindrePivot;
    public bool _isControlled;
    public int moveForce;

    PlayerInteract _playerInteract;

    public bool isControlled()
    {
        return _isControlled;
    }
    public void IsInteractable(RaycastHit hit)
    {
        if (hit.collider != null && !_isControlled)
        {
            UiManager.instance.contectuelInteracted(contextuelTXT);

        }
    }
    public void Interact(PlayerInteract player)
    {
       Debug.Log("Interact ReflecteurMovable");
        _playerInteract = player;
        _playerInteract.isInteracting = true;
        _isControlled = true;
        Debug.Log(_isControlled);
    //    RBPlayer.instance.LockMovements();

        UiManager.instance.selectedReflector = this;
        UiManager.instance.Hidecontectuel();
        UiManager.instance.DisplayReflectorQuitText(true);
    }

    public void LeaveInteract()
    {
        Debug.Log("Leave Interact ReflecteurMovable");
        _playerInteract.isInteracting = false;
        _playerInteract = null;
        _isControlled = false;
        UiManager.instance.selectedReflector = null;

     //   RBPlayer.instance.UnlockMovements();
        UiManager.instance.Displaycontectuel();

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
        SoundLauncher.instance.PlayTurrentMove();
    }

    public void RotateRight()
    {
        cylindrePivot.gameObject.transform.Rotate(0, moveForce * Time.deltaTime, 0);
        SoundLauncher.instance.PlayTurrentMove();
    }
    public void RotateUp()
    {
        // Pour gérer les angles négatifs

        float currentZ = sphereInclinaison.transform.localEulerAngles.z;
        if (currentZ > 180) currentZ -= 360;
        float newZ = Mathf.Clamp(currentZ + moveForce * Time.deltaTime, -90, 90);
        sphereInclinaison.transform.localEulerAngles = new Vector3(
            sphereInclinaison.transform.localEulerAngles.x,
            sphereInclinaison.transform.localEulerAngles.y,
            newZ
        );
        SoundLauncher.instance.PlayTurrentMove();
    }

    public void RotateDown()
    {
        // Pour gérer les angles négatifs

        float currentZ = sphereInclinaison.transform.localEulerAngles.z;
        if (currentZ > 180) currentZ -= 360;
        float newZ = Mathf.Clamp(currentZ - moveForce * Time.deltaTime, -90, 90);
        sphereInclinaison.transform.localEulerAngles = new Vector3(
            sphereInclinaison.transform.localEulerAngles.x,
            sphereInclinaison.transform.localEulerAngles.y,
            newZ
        );
        SoundLauncher.instance.PlayTurrentMove();
    }
}
