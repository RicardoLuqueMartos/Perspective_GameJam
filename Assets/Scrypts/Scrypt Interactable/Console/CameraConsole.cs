using ConAirGames;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UI;

public class CameraConsole : MonoBehaviour
{
    [SerializeField] public ConsoleInteract consoleInteract;
    [SerializeField] List<Camera> camerasList = new List<Camera>();
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

    void Start()
    {
        InitRenderTexturesList();
        SetCameraDisplay(0);
    }

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
        RBPlayer.instance.LockMovements();
     //   RBPlayer.ins
    }
    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        
    }

    void InitRenderTexturesList()
    {
        renderTexturesList.Clear();
        for (int i = 0; i < camerasList.Count; i++)
        {
            Camera cam = camerasList[i];
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
    private void SetCameraDisplay(int cameraIndex)
    {
        Camera cam = camerasList[cameraIndex];
        currentCam = cam;
        indexText.text = (cameraIndex+1).ToString();
        if (cam != null)
        {
            camDisplay.texture = renderTexturesList[cameraIndex];
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
        int currentIndex = camerasList.IndexOf(currentCam);
        currentIndex = (currentIndex + 1) % camerasList.Count;
        SetCameraDisplay(currentIndex);
    }

    public void PreviousCam()
    {
        int currentIndex = camerasList.IndexOf(currentCam);
        currentIndex = (currentIndex - 1 + camerasList.Count) % camerasList.Count;
        SetCameraDisplay(currentIndex);
    }
    #endregion Camera selection

}
