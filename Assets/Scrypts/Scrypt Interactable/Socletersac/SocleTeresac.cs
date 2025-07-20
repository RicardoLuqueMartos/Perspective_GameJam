using UnityEngine;

public class SocleTeresac : MonoBehaviour, IInteractable
{
    [SerializeField] string contextuelTXT_needTeserac;
    [SerializeField] string contextuelTXT_empty;
    [SerializeField] string contextuelTXT_full;
    [SerializeField] RayonEmission rayon;
    [SerializeField] Transform socleTeresac;

    [SerializeField] public GameObject teresac;

    PlayerInteract _playerInteract;

    public bool _isControlled = false;


    public bool isControlled()
    {
        return rayon.powered;
    }
    public void IsInteractable(RaycastHit hit)
    {
        if (hit.collider != null && !rayon.powered)
        {
            if (!rayon.powered && GameProgressManager.instance.HaveTesseract())
            {
                UiManager.instance.contectuelInteracted(contextuelTXT_empty);
            }
            else if (!rayon.powered && !GameProgressManager.instance.HaveTesseract())
            {
                UiManager.instance.contectuelInteracted(contextuelTXT_needTeserac);
            }
        }
    }
    public void Interact(PlayerInteract player)
    {
        Debug.Log("Interact SocleTeresac");
        if (!rayon.powered && GameProgressManager.instance.HaveTesseract())
        {
            GameProgressManager.instance.PlayerUseTesseract();
 
                teresac = Instantiate(GameProgressManager.instance.prefabTeresac, socleTeresac.position, Quaternion.identity);
                teresac.transform.SetParent(socleTeresac);

            rayon.TurnOn();
         }
        else if (rayon.powered)
        {
            GameProgressManager.instance.PlayerGetTesseract();
            Destroy(teresac);
            rayon.TurnOff();
        }

    }

    public void LeaveInteract()
    {
        
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (teresac == null && rayon.powered)
        {
            rayon.powered = false;
            GameProgressManager.instance.PlayerGetTesseract();
            rayon.TurnOff();
        }

    }
}
