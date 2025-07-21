using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] GameSettingsData gameSettingsData;

    public static UiManager instance;

    public CameraConsole cameraConsole;
    public ReflecteurMovable selectedReflector;


    public GameObject GameMenuObject;

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

        HideMenu();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && selectedReflector != null)
        {
            selectedReflector.LeaveInteract();
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (GameMenuObject.activeInHierarchy) HideMenu();
            else DisplayMenu();
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



    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void DisplayMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        GameMenuObject.SetActive(true);
    }

    public void HideMenu()
    {
        Cursor.lockState = CursorLockMode.Locked;
        GameMenuObject.SetActive(false);
    }
}
