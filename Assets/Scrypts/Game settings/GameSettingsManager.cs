using UnityEngine;

public class GameSettingsManager : MonoBehaviour
{
    public GameSettingsData gameSettingsData;

    public static GameSettingsManager instance;

    private void Start()
    {
        CreateInstance();
    }

    void CreateInstance()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
}
