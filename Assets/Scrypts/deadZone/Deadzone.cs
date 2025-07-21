using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using DG.Tweening;

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

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Teserac"))
        {
            other.gameObject.transform.position = GameProgressManager.instance.currentRespawnTransform.position;
        }
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController.instance.KillPlayer();
        }
    }

    
}
