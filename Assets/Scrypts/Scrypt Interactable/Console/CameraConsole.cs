using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UI;

public class CameraConsole : MonoBehaviour
{
    #region Variables
    public ConsoleInteract consoleInteract;

    [Header("UI Elements")]
    [SerializeField] Button nextCam;
    [SerializeField] Button previousCam;
    [SerializeField] RawImage camDisplay;
    [SerializeField] TMP_Text indexText;
    [SerializeField] ReflectorConsole reflectorsConsole;
    public List<RenderTexture> renderTexturesList = new List<RenderTexture>();

    //   [SerializeField] depthb format;
    //   public int currentCameraIndex;

    public static CameraConsole instance;

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
        RBPlayer.instance.canRotate = false;
        Cursor.lockState = CursorLockMode.None;
        UiManager.instance.Hidecontectuel();
        InitRenderTexturesList();
        InitReflectorsPanel();
        SetCameraDisplay();
    }
    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.Locked; 
        RBPlayer.instance.canRotate = true;
    }

    public void OpenCameraConsole(ConsoleInteract _consoleInteract)
    {
        UiManager.instance.centerCursorImage.gameObject.SetActive(false);   
        UiManager.instance.HideAllControlsInfos();
        consoleInteract = _consoleInteract;
        gameObject.SetActive(true);
        SoundLauncher.instance.PlayClickButton();
    }

    public void ExitConsole()
    {
        UiManager.instance.centerCursorImage.gameObject.SetActive(true);
        UiManager.instance.selectedReflector = null;
        gameObject.SetActive(false);
        SoundLauncher.instance.PlayClickButtonFail();
        UiManager.instance.DisplayBaseControlsInfo();
    }

    #region Camera view
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
        if (GameSettingsManager.instance.gameSettingsData.renderTextureCurrentSizeX == 0 || GameSettingsManager.instance.gameSettingsData.renderTextureCurrentSizeY == 0)
            return null;

     //   GraphicsFormat gfxFormat = GraphicsFormat.argb

        // The RenderTextureReadWrite setting is purposely omitted in order to get the "Default" behavior.
        RenderTexture texture = new (GameSettingsManager.instance.gameSettingsData.renderTextureCurrentSizeX, GameSettingsManager.instance.gameSettingsData.renderTextureCurrentSizeY
            , 24, GameSettingsManager.instance.gameSettingsData.format)
        {
            hideFlags = HideFlags.HideAndDontSave,
            name = cam.name,
            filterMode = GameSettingsManager.instance.gameSettingsData.m_filterMode
        };

        return texture;
    }
    #endregion Camera view

    #region Reflectors management
    void InitReflectorsPanel()
    {
        if (consoleInteract.LinkedReflectorsList.Count > 0)
        {
            reflectorsConsole.DisplayIndex();
            reflectorsConsole.gameObject.SetActive(true);
        }
        else reflectorsConsole.gameObject.SetActive(false);
    }

    #endregion Reflectors management

    #region Camera Display
    private void SetCameraDisplay()
    {
        Camera cam = consoleInteract.camerasList[consoleInteract.cameraIndex];

        indexText.text = (consoleInteract.cameraIndex + 1).ToString();
        if (cam != null)
        {
            camDisplay.texture = renderTexturesList[consoleInteract.cameraIndex];
            consoleInteract.ConsoleCamImage.texture = renderTexturesList[consoleInteract.cameraIndex];

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
        consoleInteract.cameraIndex = (consoleInteract.cameraIndex + 1) % consoleInteract.camerasList.Count;
        SetCameraDisplay();
    }

    public void PreviousCam()
    {
        consoleInteract.cameraIndex = (consoleInteract.cameraIndex - 1 + consoleInteract.camerasList.Count) % consoleInteract.camerasList.Count;
        SetCameraDisplay();
    }
    #endregion Camera selection

    
}
