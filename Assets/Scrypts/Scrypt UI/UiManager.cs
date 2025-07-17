using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    [SerializeField] private CameraConsole cameraConsole;

    public TMP_Text contextuelInteract;
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
        cameraConsole.consoleInteract = consoleInteract;
        cameraConsole.gameObject.SetActive(true);
    }
}
