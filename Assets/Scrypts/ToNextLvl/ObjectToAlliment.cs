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
        if (audioSourceOFF.isPlaying)
            {
            audioSourceOFF.Stop();
            }
        if (!audioSourceON.isPlaying)
        {
            SoundLauncher.instance.PlayStructureMove(audioSourceON);

        }
        transform.DOMove(transformOn.position, timeToMove)
            .OnComplete(() => audioSourceON.Stop());
        transform.DORotate(transformOn.rotation.eulerAngles, timeToMove)
            .OnComplete(() => audioSourceON.Stop());
    }

    public void SetPositionOff()
    {
        if (audioSourceON.isPlaying)
        {
            audioSourceON.Stop();
        }
        if (!audioSourceOFF.isPlaying)
        { 
            SoundLauncher.instance.PlayStructureMove(audioSourceOFF); 
        
        }
            
        transform.DOMove(transformOff.position, timeToMove)
            .OnComplete(() => audioSourceOFF.Stop());
        transform.DORotate(transformOff.rotation.eulerAngles, timeToMove)
            .OnComplete(() => audioSourceOFF.Stop());
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
