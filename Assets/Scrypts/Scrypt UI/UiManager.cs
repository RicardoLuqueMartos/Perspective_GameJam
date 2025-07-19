using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] GameSettingsData gameSettingsData;

    public static UiManager instance;

    [SerializeField] private CameraConsole cameraConsole;
    public ReflecteurMovable selectedReflector;


    public TMP_Text contextuelInteract;
    public GameObject RefectorQuitPanel;

    [SerializeField] Image tesseractImage;
    [SerializeField] public Image fadingPanel;
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

    private void Update()
    {
        if (Input.GetKey(KeyCode.E) && selectedReflector != null)
        {
            selectedReflector.LeaveInteract();
        }
    }

    public void contectuelInteracted(string interactTxt)
    {
        contextuelInteract.text = interactTxt;
        Displaycontectuel();
    }
    public void Displaycontectuel()
    {
        if (CanDisplayContextuel())    
            contextuelInteract.transform.parent.gameObject.SetActive(true);
    }
    public void Hidecontectuel()
    {
        contextuelInteract.transform.parent.gameObject.SetActive(false);
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

    bool CanDisplayContextuel()
    {
        bool response = true;
        if (cameraConsole.gameObject.activeInHierarchy
            && selectedReflector != null) response = false;

        return response;
    }

    public void DisplayReflectorQuitText(bool value)
    {
        RefectorQuitPanel.SetActive(value);
    }

}
