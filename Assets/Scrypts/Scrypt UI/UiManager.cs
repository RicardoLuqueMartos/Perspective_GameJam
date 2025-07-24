using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] GameSettingsData gameSettingsData;

    public static UiManager instance;
    public Image centerCursorImage;
    public CameraConsole cameraConsole;
    public ReflecteurMovable selectedReflector;

    public GameObject GameMenuObject;

    public TMP_Text contextuelInteract;
    public GameObject RefectorQuitPanel;

    [SerializeField] Image tesseractImage;
    [SerializeField] public Image fadingPanel;
    public TMP_Text tesseractAmount;

    [SerializeField] GameObject BaseControlsInfo;
    [SerializeField] GameObject ReflectorsControlsInfo;

    public GameObject WinPanel;

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

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }


    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.E) && selectedReflector != null)
        {
            selectedReflector.LeaveInteract();
        }*/

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (CameraConsole.instance == null || !CameraConsole.instance.gameObject.activeInHierarchy)
            {
                if (GameMenuObject.activeInHierarchy) HideMenu();
                else DisplayMenu();
                
            }
            else
            {
                CameraConsole.instance.ExitConsole();
            }

        }
    }


    public void OpenCameraConsole(ConsoleInteract consoleInteract)
    {
        Hidecontectuel();
        HideAllControlsInfos();
        cameraConsole.OpenCameraConsole(consoleInteract);
    }

    public void DisplayTesseract(bool value, int amount)
    {
        tesseractAmount.text = amount.ToString();
        if (amount > 1) tesseractAmount.transform.parent.gameObject.SetActive(true);
        else tesseractAmount.transform.parent.gameObject.SetActive(false);
        tesseractImage.sprite = gameSettingsData.tesseractSprite;
        tesseractImage.transform.parent.gameObject.SetActive(value);
        Hidecontectuel();
    }

    #region Controls Infos
    public void HideAllControlsInfos()
    {
        BaseControlsInfo.SetActive(false);
        ReflectorsControlsInfo.SetActive(false);
    }

    public void DisplayBaseControlsInfo()
    {
        BaseControlsInfo.SetActive(true);
    }

    public void DisplayReflectorsControlsInfo()
    {
        ReflectorsControlsInfo.SetActive(true);
    }
    #endregion Controls Infos

    #region Contextual info
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
    #endregion Contextual info

    #region Menu & quit restart
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
        centerCursorImage.gameObject.SetActive(false);  
        HideAllControlsInfos();
        RBPlayer.instance.canRotate = false;
        Cursor.lockState = CursorLockMode.None;
        GameMenuObject.SetActive(true);
        if (SoundLauncher.instance != null)
            SoundLauncher.instance.PlayClickButton();
    }

    public void DisplayWinPanel()
    {
        centerCursorImage.gameObject.SetActive(false);
        HideAllControlsInfos();
        Cursor.lockState = CursorLockMode.None;
        if (SoundLauncher.instance != null)
            SoundLauncher.instance.PlayRecepteurPowerOn();
        WinPanel.SetActive(true);
    }

    public void HideMenu()
    {
        centerCursorImage.gameObject.SetActive(true);
        DisplayBaseControlsInfo();
        RBPlayer.instance.canRotate = true;
        Cursor.lockState = CursorLockMode.Locked;
        GameMenuObject.SetActive(false);
        if (SoundLauncher.instance != null)
            SoundLauncher.instance.PlayClickButtonFail();
    }
    #endregion Menu & quit restart
}
