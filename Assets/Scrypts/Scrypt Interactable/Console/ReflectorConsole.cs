using UnityEngine;

public class ReflectorConsole : MonoBehaviour
{
    public Transform reflectorHeadTransform;

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
        RBPlayer.instance.LockMovements();
    }
    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        RBPlayer.instance.UnlockMovements();
    }

    public void RotateLeft()
    {
    //    reflectorHeadTransform.Rotate = 
    }

    public void RotateRight()
    {

    }
    public void RotateUp()
    {

    }

    public void RotateDown()
    {

    }



    public void ExitConsole()
    {
        gameObject.SetActive(false);
    }
}
