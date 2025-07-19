using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class GameProgressManager : MonoBehaviour
{
    #region Variables
    [SerializeField] public int currentLevel = 0;
    [SerializeField] public Transform[] respawnTransform;
    [SerializeField] public ReccepteurRayon[] lvlReccepteur;
    [SerializeField] public GameObject[] bridgeToNextLvl;
    



    [SerializeField] private int tesseractAmount = 0;
    public GameObject prefabTeresac;


    public static GameProgressManager instance;

#endregion Variables

    #region Init
    private void Awake()
    {
        CreateInstance();
    }

    void CreateInstance()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    #endregion Init

    private void OnEnable()
    {
    //    ManageTesseractDisplay();
    }

    public void PlayerGetTesseract()
    {
        tesseractAmount++;
        ManageTesseractDisplay();
    }

    public void PlayerUseTesseract()
    {
        tesseractAmount--;
        ManageTesseractDisplay();
    }

    public void ManageTesseractDisplay()
    {
        UiManager.instance.DisplayTesseract(HaveTesseract());
    }

    public bool HaveTesseract()
    {
        bool response = false;
        if (tesseractAmount > 0) response = true;
        return response;
    }
}
