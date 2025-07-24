using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class TeleporterWinGame : MonoBehaviour
{


    public GameObject WinPanel ;




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
        if (other.gameObject.CompareTag("Player"))

            UiManager.instance.DisplayWinPanel();



    }

}
