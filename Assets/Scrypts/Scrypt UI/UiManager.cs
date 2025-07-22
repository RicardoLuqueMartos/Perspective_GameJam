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
    public TMP_Text tesseractAmount;


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
        /*if (Input.GetKeyDown(KeyCode.E) && selectedReflector != null)
        {
            selectedReflector.LeaveInteract();
        }*/

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (!CameraConsole.instance.gameObject.activeInHierarchy)
            {
                if (GameMenuObject.activeInHierarchy)
                {
                    HideMenu();

                }
                else
                {
                    DisplayMenu();

                }
            }
            else
            {
                CameraConsole.instance.ExitConsole();
            }
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
        Hidecontectuel();
        cameraConsole.OpenCameraConsole(consoleInteract);
    }

    public void DisplayTesseract(bool value, int amount)
    {
        tesseractAmount.text = amount.ToString();
        if(amount > 1) tesseractAmount.transform.parent.gameObject.SetActive(true); 
        else tesseractAmount.transform.parent.gameObject.SetActive(false);
        tesseractImage.sprite = gameSettingsData.tesseractSprite;
        tesseractImage.transform.parent.gameObject.SetActive(value);
        Hidecontectuel();
    }

    bool CanDisplayContextuel()
    {
        bool response = true;
        if (cameraConsole.gameObject.activeInHierarchy
            || selectedReflector != null) response = false;

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
        SoundLauncher.instance.PlayClickButton();
        Cursor.lockState = CursorLockMode.None;
        GameMenuObject.SetActive(true);
    }

    public void HideMenu()
    {
        SoundLauncher.instance.PlayClickButtonFail();
        Cursor.lockState = CursorLockMode.Locked;
        GameMenuObject.SetActive(false);
    }
}
