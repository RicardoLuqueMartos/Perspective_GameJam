using UnityEngine;

public class TriggerEndGame : MonoBehaviour
{   
    [SerializeField]
    private UIManager uIManager;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger");
        if (other.GetComponent<PlayerController>() != null)
        {
            DisplayEndGameUI();
        }
    }

    void DisplayEndGameUI()
    {
        uIManager.DisplayMenu();
    }

    
    
}
