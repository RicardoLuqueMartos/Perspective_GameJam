using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject GameMenuObject;

    public static UIManager instance;

    void Awake()
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

    private void Start()
    {
        HideMenu();
    }

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape)) {
            if (GameMenuObject.activeInHierarchy) HideMenu();
            else DisplayMenu();
        }
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
