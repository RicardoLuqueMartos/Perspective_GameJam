using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] GameSettingsData gameSettingsData;

    public static UiManager instance;

    [SerializeField] private CameraConsole cameraConsole;

    public TMP_Text contextuelInteract;

    [SerializeField] Image tesseractImage;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void contectuelInteracted(string interactTxt)
    {
        contextuelInteract.text = interactTxt;
    }

    public void OpenCameraConsole(ConsoleInteract consoleInteract)
    {
        cameraConsole.OpenCameraConsole(consoleInteract);
    }

    public void DisplayTesseract(bool value)
    {
        tesseractImage.sprite = gameSettingsData.tesseractSprite;
        tesseractImage.gameObject.SetActive(value);
    }

}
