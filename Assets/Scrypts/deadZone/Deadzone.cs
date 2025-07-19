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
            StartCoroutine(FadeOutToRespawn());
        }
    }

    public IEnumerator FadeOutToRespawn()
    {
        UiManager.instance.fadingPanel.DOFade(1, 1);
        yield return new WaitForSeconds(2);
        RBPlayer.instance.transform.position = GameProgressManager.instance.currentRespawnTransform.position;
        UiManager.instance.fadingPanel.DOFade(0, 2);
        yield break;
    }
}
