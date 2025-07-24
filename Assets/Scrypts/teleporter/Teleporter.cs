using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public bool powered;

    [SerializeField] ReccepteurRayon[] reccepteurRayons;

    public Transform destination;

    public Material materialOFF;
    public Material materialON;

    [SerializeField] private GameObject cristaux1;
    [SerializeField] private GameObject cristaux2;
    [SerializeField] private GameObject cristaux3;
    [SerializeField] private GameObject cristaux4;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckAllimentation()
    {
        powered = true;

        for (int i = 0; i < reccepteurRayons.Length; i++)
        {
            if (!reccepteurRayons[i].powered)
            {
                powered = false;
                break;
            }
        }

            SetPowered(powered);
        
    }

    private void SetPowered(bool powered)
    {
        if (powered)
        {
            cristaux1.GetComponent<MeshRenderer>().material = materialON;
            cristaux2.GetComponent<MeshRenderer>().material = materialON;
            cristaux3.GetComponent<MeshRenderer>().material = materialON;
            cristaux4.GetComponent<MeshRenderer>().material = materialON;
        }
        else
        {
            cristaux1.GetComponent<MeshRenderer>().material = materialOFF;
            cristaux2.GetComponent<MeshRenderer>().material = materialOFF;
            cristaux3.GetComponent<MeshRenderer>().material = materialOFF;
            cristaux4.GetComponent<MeshRenderer>().material = materialOFF;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player entered the teleporter trigger zone.");
        if (powered)
        {
            StartCoroutine(TeleportPlayer());
        }
    }


    public IEnumerator TeleportPlayer()
    {
        Debug.Log("Teleporting player to destination: " + destination.position);
        UiManager.instance.fadingPanel.enabled = true;
        UiManager.instance.fadingPanel.DOFade(1, 1);
        SoundLauncher.instance.PlayDissolve();
        var controller = PlayerController.instance;
        if (controller != null) controller.enabled = false;
        RBPlayer.instance.material.DOFloat(1.1f, "_dissolveAmount", 2f).SetEase(Ease.InOutQuad); // Start with dissolve effect
        yield return new WaitForSeconds(2);

        // Désactivation du CharacterController avant de déplacer le joueur


        RBPlayer.instance.gameObject.transform.position = destination.position;
        GameProgressManager.instance.currentRespawnTransform = destination;

        // Réactivation du CharacterController après le déplacement

        RBPlayer.instance.material.DOFloat(0.1f, "_dissolveAmount", 2f).SetEase(Ease.InOutQuad); // resummoning disolve
        UiManager.instance.fadingPanel.DOFade(0, 2);
        SoundLauncher.instance.PlayDissolve();
        yield return new WaitForSeconds(2);
        if (controller != null) controller.enabled = true;
        UiManager.instance.fadingPanel.enabled = false;
        yield break;
    }   
}
