using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.Rendering.DebugUI;
using System.Collections;
using Unity.VisualScripting;

public class GameProgressManager : MonoBehaviour
{
    #region Variables
    [SerializeField] public Transform currentRespawnTransform;

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
        Debug.Log("PlayerGetTesseract " + tesseractAmount);
        tesseractAmount++;
        ManageTesseractDisplay();
        SoundLauncher.instance.PlayPickUpItem();
    }

    public void PlayerUseTesseract()
    {
        Debug.Log("PlayerUseTesseract "+ tesseractAmount);
        tesseractAmount--;
        ManageTesseractDisplay();
    }

    public void ManageTesseractDisplay()
    {
        Debug.Log("ManageTesseractDisplay "+ tesseractAmount);
        UiManager.instance.DisplayTesseract(HaveTesseract(), tesseractAmount);
    }

    public bool HaveTesseract()
    {
        bool response = false;
        if (tesseractAmount > 0) response = true;
        return response;
    }
    
}
