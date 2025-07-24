using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public bool powered;

    public Transform destination;

    public Material materialOFF;
    public Material materialON;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerStay(Collider other)
    {
        if (powered && other.GetComponent<PlayerController>())
        {
            GameObject player = other.gameObject;
            TeleportPlayer(player);
        }
    }


    public IEnumerator TeleportPlayer(GameObject player)
    {
        UiManager.instance.fadingPanel.enabled = true;
        UiManager.instance.fadingPanel.DOFade(1, 1);
        var controller = PlayerController.instance;
        if (controller != null) controller.enabled = false;
        PlayerController.instance.material.DOFloat(1.1f, "_dissolveAmount", 2f).SetEase(Ease.InOutQuad); // Start with dissolve effect
        yield return new WaitForSeconds(2);

        // Désactivation du CharacterController avant de déplacer le joueur


        PlayerController.instance.gameObject.transform.position = destination.position;
        GameProgressManager.instance.currentRespawnTransform = destination;

        // Réactivation du CharacterController après le déplacement

        PlayerController.instance.material.DOFloat(0.1f, "_dissolveAmount", 2f).SetEase(Ease.InOutQuad); // resummoning disolve
        UiManager.instance.fadingPanel.DOFade(0, 2);
        yield return new WaitForSeconds(2);
        if (controller != null) controller.enabled = true;
        UiManager.instance.fadingPanel.enabled = false;
        yield break;
    }   
}
