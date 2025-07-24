using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using DG.Tweening;

public class Deadzone : MonoBehaviour
{
    public bool StartAlimented = true;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetAlimented(StartAlimented);
    }

    public void SetAlimented(bool alimented)
    {
        gameObject.SetActive(alimented);
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
