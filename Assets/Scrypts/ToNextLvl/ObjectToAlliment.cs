using DG.Tweening;
using System.Linq;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class ObjectToAlliment : MonoBehaviour
{
    bool powered = false;

    [SerializeField] Transform transformOn;
    [SerializeField] Transform transformOff;
    [SerializeField] float timeToMove = 1;
    [SerializeField] AudioSource audioSource;

    bool moving;

    [SerializeField] ReccepteurRayon[] reccepteurRayons;
    // Start is called once before the first execution of Update after the MonoBehaviour is created  
    void Start()
    {

    }

    // Update is called once per frame  
    void Update()
    {
    }

    public void SetPositionOn()
    {
        if (!audioSource.isPlaying)
            SoundLauncher.instance.PlayStructureMove(audioSource);
        transform.DOMove(transformOn.position, timeToMove)
            .OnComplete(() => audioSource.Stop());
        transform.DORotate(transformOn.rotation.eulerAngles, timeToMove)
            .OnComplete(() => audioSource.Stop());
    }

    public void SetPositionOff()
    {
        if (!audioSource.isPlaying)
            SoundLauncher.instance.PlayStructureMove(audioSource);
        transform.DOMove(transformOff.position, timeToMove)
            .OnComplete(() => audioSource.Stop());
        transform.DORotate(transformOff.rotation.eulerAngles, timeToMove)
            .OnComplete(() => audioSource.Stop());
    }

    public void CheckAllimentation()
    {
        powered = true;

        for (int i = 0; i < reccepteurRayons.Length; i++)
        {
            if (!reccepteurRayons[i].powered)
            {
                powered = false;
                break;
            }
        }

        if (powered)
        {
            SetPositionOn();
        }
        else
        {
            SetPositionOff();
        }
    }
}
