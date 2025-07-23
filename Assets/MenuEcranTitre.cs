using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuEcranTitre : MonoBehaviour
{
    public string levelToLoad = "Level1";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void QuitGame()
    {
               Application.Quit();
    }
}
