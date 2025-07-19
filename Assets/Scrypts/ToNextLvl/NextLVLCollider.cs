using UnityEngine;

public class NextLVLCollider : MonoBehaviour
{

    [SerializeField] Transform newRespawn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameProgressManager.instance.currentRespawnTransform = newRespawn;
        }
    }
}
