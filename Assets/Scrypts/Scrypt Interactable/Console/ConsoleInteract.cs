using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleInteract : MonoBehaviour, IInteractable
{
    public List<Camera> camerasList = new List<Camera>();
    public List<ReflecteurMovable> LinkedReflectorsList = new List<ReflecteurMovable>();

    [SerializeField] GameObject consoleUI;
    [SerializeField] string contextuelTXT;
    public int cameraIndex = 0;
    public int ReflectorIndex = 0;

    public bool _isControlled = false;

    PlayerInteract _playerInteract;
    public RawImage ConsoleCamImage;

    void OnEnable()
    {
        Invoke("GenerateFirstCameraView", 1);
    }

    public bool isControlled()
    {
        return _isControlled;
    }

    public void IsInteractable(RaycastHit hit)
    {
        if (hit.collider != null)
        {
            UiManager.instance.contectuelInteracted(contextuelTXT);
        }
    }

    public void Interact(PlayerInteract player)
    {
        UiManager.instance.OpenCameraConsole(this);
        _isControlled = true;
        player.isInteracting = true;
        _playerInteract = player;
        _playerInteract.interactingTarget = this;
    }

    public void LeaveInteract()
    {
        _isControlled = false;
        _playerInteract.isInteracting = false;
        _playerInteract.interactingTarget = null;
        _playerInteract = null;
    }

    void GenerateFirstCameraView()
    {
        GenerateCameraView(camerasList[0]);
    }
    void GenerateCameraView(Camera cam)
    {
        RenderTexture newRT = CreateRenderTexture(camerasList[0]);
        camerasList[0].targetTexture = newRT;
        ConsoleCamImage.texture = newRT;
    }
    RenderTexture CreateRenderTexture(Camera cam)
    {
        if (GameSettingsManager.instance.gameSettingsData.renderTextureCurrentSizeX == 0 || GameSettingsManager.instance.gameSettingsData.renderTextureCurrentSizeY == 0)
            return null;

        // The RenderTextureReadWrite setting is purposely omitted in order to get the "Default" behavior.
        return new RenderTexture(GameSettingsManager.instance.gameSettingsData.renderTextureCurrentSizeX, GameSettingsManager.instance.gameSettingsData.renderTextureCurrentSizeY
            , 0, GameSettingsManager.instance.gameSettingsData.format)
        {
            hideFlags = HideFlags.HideAndDontSave,
            name = cam.name,
            filterMode = GameSettingsManager.instance.gameSettingsData.m_filterMode
        };
    }
}
