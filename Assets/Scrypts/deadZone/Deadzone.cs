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

    public void KillPlayer()
    {
        StartCoroutine(FadeOutToRespawn());
    }
    public IEnumerator FadeOutToRespawn()
    {
        UiManager.instance.fadingPanel.enabled = true;
        UiManager.instance.fadingPanel.DOFade(1, 1);
        RBPlayer.instance.material.DOFloat(1.1f, "_dissolveAmount", 2f).SetEase(Ease.InOutQuad); // Start with dissolve effect
        SoundLauncher.instance.PlayDissolve();


        yield return new WaitForSeconds(2);

        // Désactivation du CharacterController avant de déplacer le joueur
        var controller = RBPlayer.instance;
        if (controller != null) controller.enabled = false;

        RBPlayer.instance.gameObject.transform.position = GameProgressManager.instance.currentRespawnTransform.position;

        // Réactivation du CharacterController après le déplacement
        if (controller != null) controller.enabled = true;

        UiManager.instance.fadingPanel.DOFade(0, 2);
        RBPlayer.instance.material.DOFloat(0.1f, "_dissolveAmount", 2f).SetEase(Ease.InOutQuad); // resummoning disolve
        SoundLauncher.instance.PlayDissolve();
        yield return new WaitForSeconds(2);
        UiManager.instance.fadingPanel.enabled = false;
        yield break;
    }


}
