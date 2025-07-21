using UnityEngine;

public class TriggerMoveDoor : MonoBehaviour
{
    [SerializeField] private Transform objectToMove;
    [SerializeField] private float moveSpeed = 0;
    [SerializeField] private float moveAmplitude = 0;
    [SerializeField] private bool moving = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger");
        if (other.GetComponent<PlayerController>() != null)
        {
            if (!moving) LaunchMove();
        }
    }

    void LaunchMove()
    {
        moving = true;
    }

    private void Update()
    {
        if (moving)
        {
            if (objectToMove.localPosition.y > moveAmplitude)
            {
                objectToMove.localPosition = new Vector3(objectToMove.localPosition.x,
                    objectToMove.localPosition.y - moveSpeed,
                    objectToMove.localPosition.z);
            }
            else
            {
                moving = false;
                Destroy(this);
            }
        }
    }
    
}
