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
    [SerializeField] AudioSource audioSourceON;
    [SerializeField] AudioSource audioSourceOFF;

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
        if (audioSourceOFF != null && audioSourceOFF.isPlaying)
            {
            audioSourceOFF.Stop();
            }
        if (audioSourceON != null && !audioSourceON.isPlaying)
        {
            SoundLauncher.instance.PlayStructureMove(audioSourceON);

        }

        if (audioSourceON != null && transformOn != null)
        {
            transform.DOMove(transformOn.position, timeToMove)
            .OnComplete(() => audioSourceON.Stop());
            transform.DORotate(transformOn.rotation.eulerAngles, timeToMove)
            .OnComplete(() => audioSourceON.Stop());
        }
        else if (transformOn != null)
        {
            transform.DOMove(transformOn.position, timeToMove);
            transform.DORotate(transformOn.rotation.eulerAngles, timeToMove);
        }
    }

    public void SetPositionOff()
    {
        if (audioSourceON != null && audioSourceON.isPlaying)
        {
            audioSourceON.Stop();
        }
        if (audioSourceOFF != null && !audioSourceOFF.isPlaying)
        { 
            SoundLauncher.instance.PlayStructureMove(audioSourceOFF); 
        
        }
        if (audioSourceOFF != null && transformOff != null)
        {
            transform.DOMove(transformOff.position, timeToMove)
            .OnComplete(() => audioSourceOFF.Stop());
            transform.DORotate(transformOff.rotation.eulerAngles, timeToMove)
            .OnComplete(() => audioSourceOFF.Stop());
        }
        else if (transformOff != null)
        {
            transform.DOMove(transformOff.position, timeToMove);
            transform.DORotate(transformOff.rotation.eulerAngles, timeToMove);
        }
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
