using ConAirGames;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UI;

public class CameraConsole : MonoBehaviour
{
   public ConsoleInteract consoleInteract;
  //  public List<Camera> camerasList = new List<Camera>();
    [SerializeField] List<RenderTexture> renderTexturesList = new List<RenderTexture>();
    [SerializeField] Button nextCam;
    [SerializeField] Button previousCam;
    [SerializeField] RawImage camDisplay;
    [SerializeField] TMP_Text indexText;
    [SerializeField] int renderTextureCurrentSizeX = 854;
    [SerializeField] int renderTextureCurrentSizeY = 480;
    [SerializeField] FilterMode m_filterMode = FilterMode.Point;
    [SerializeField] GraphicsFormat format;
    private Camera currentCam;
    public int currentCameraIndex;

    public static CameraConsole instance;

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

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
        RBPlayer.instance.LockMovements();

        InitRenderTexturesList();
        SetCameraDisplay();
    }
    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        RBPlayer.instance.UnlockMovements();
    }

    public void OpenCameraConsole(ConsoleInteract consoleInteract)
    {
     //   consoleInteract = consoleInteract;
    //    camerasList = consoleInteract.camerasList;
        currentCameraIndex = consoleInteract.cameraIndex;
        gameObject.SetActive(true);
    }

    void InitRenderTexturesList()
    {
        renderTexturesList.Clear();
        for (int i = 0; i < consoleInteract.camerasList.Count; i++)
        {
            Camera cam = consoleInteract.camerasList[i];
            RenderTexture newRT = CreateRenderTexture(cam);
            renderTexturesList.Add(newRT);
            cam.targetTexture = newRT;
        }
    }

    RenderTexture CreateRenderTexture(Camera cam)
    {
        if (renderTextureCurrentSizeX == 0 || renderTextureCurrentSizeY == 0)
            return null;

        // The RenderTextureReadWrite setting is purposely omitted in order to get the "Default" behavior.
        return new RenderTexture(renderTextureCurrentSizeX, renderTextureCurrentSizeY, 0, format)
        {
            hideFlags = HideFlags.HideAndDontSave,
            name = cam.name,
            filterMode = m_filterMode
        };
    }

    #region Camera Display
    private void SetCameraDisplay()
    {
        Camera cam = consoleInteract.camerasList[currentCameraIndex];
        currentCam = cam;
        indexText.text = (currentCameraIndex + 1).ToString();
        if (cam != null)
        {
            camDisplay.texture = renderTexturesList[currentCameraIndex];
            consoleInteract.cameraIndex = currentCameraIndex;
        }
        else
        {
            camDisplay.enabled = false;
        }
    }

    #endregion Camera Display

    #region Camera selection
    public void NextCam()
    {
        currentCameraIndex = (currentCameraIndex + 1) % consoleInteract.camerasList.Count;
        SetCameraDisplay();
    }

    public void PreviousCam()
    {
        currentCameraIndex = (currentCameraIndex - 1 + consoleInteract.camerasList.Count) % consoleInteract.camerasList.Count;
        SetCameraDisplay();
    }
    #endregion Camera selection

    public void ExitConsole()
    {
        gameObject.SetActive(false);
    }
}
