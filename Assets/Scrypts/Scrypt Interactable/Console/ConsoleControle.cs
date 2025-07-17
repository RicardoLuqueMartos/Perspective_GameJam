using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ConsoleControle : MonoBehaviour
{
    [SerializeField] ConsoleInteract consoleInteract;
    [SerializeField] List<Camera> cameras = new List<Camera>;
    [SerializeField] Button nextCam;
    [SerializeField] Button previousCam;
    [SerializeField] RawImage camDisplay;
    private Camera currentCam;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void SetCameraDisplay(Camera cam)
    {
               if (cam != null)
        {
            //TODO set the camera to render to the RawImage
        }
        else
        {
            camDisplay.enabled = false;
        }
    }
    public void NextCam()
    {
        int currentIndex = System.Array.IndexOf(cameras, currentCam);
        currentIndex = (currentIndex + 1) % cameras.Length;
        SetCameraDisplay(cameras[currentIndex]);
    }

    public void PreviousCam()
    {
        int currentIndex = System.Array.IndexOf(cameras, currentCam);
        currentIndex = (currentIndex - 1 + cameras.Length) % cameras.Length;
        SetCameraDisplay(cameras[currentIndex]);
    }
}
