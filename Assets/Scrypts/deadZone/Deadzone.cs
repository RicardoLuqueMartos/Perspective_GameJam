using UnityEngine;

public class Deadzone : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Teserac"))
        {
            collision.gameObject.transform.position = GameProgressManager.instance.respawnTransform[GameProgressManager.instance.currentLevel].position;
        }
    }
}
