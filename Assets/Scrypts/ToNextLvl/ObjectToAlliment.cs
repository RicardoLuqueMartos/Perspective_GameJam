using DG.Tweening;
using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Unity.VisualScripting.Member;

public enum ObjectToAllimentTypeEnum
{
    Move, TurnOnConsole, TurnOffConsole, TurnOnRayon, TurnOffRayon, Appear, Hide
}

public class ObjectToAlliment : MonoBehaviour
{

    [SerializeField] ObjectToAllimentTypeEnum type = ObjectToAllimentTypeEnum.Move;
    bool powered = false;
    [Header("console type")]
    [SerializeField] List<ConsoleInteract> consoleInteractsList = new();
    [Header("deadZone type")]
    [SerializeField] List<Deadzone> deadZonesList = new();
    [Header("Appear type")]
    [SerializeField] List<ObjectToAlliment> AppearsList = new();

    [Header("Move type")]
    [SerializeField] Transform transformOn;
    [SerializeField] Transform transformOff;
    [SerializeField] float timeToMove = 1;

    [Header("audio")]
    [SerializeField] AudioSource audioSourceON;
    [SerializeField] AudioSource audioSourceOFF;

    bool moving;

    [SerializeField] ReccepteurRayon[] reccepteurRayons;
    

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
            if (type == ObjectToAllimentTypeEnum.Move)
                SetPositionOn();
        }
        else
        {
            if (type == ObjectToAllimentTypeEnum.Move)
                SetPositionOff();
        }

        if (type == ObjectToAllimentTypeEnum.TurnOnConsole)
            SetConsoles(powered);
        else if (type == ObjectToAllimentTypeEnum.TurnOffConsole)
            SetConsoles(!powered);
        else if (type == ObjectToAllimentTypeEnum.TurnOnRayon)
            SetRayons(powered);
        else if (type == ObjectToAllimentTypeEnum.TurnOffRayon)
            SetRayons(!powered);
        else if (type == ObjectToAllimentTypeEnum.Appear)
            SetAppear(powered);
        else if (type == ObjectToAllimentTypeEnum.Hide)
            SetAppear(!powered);
    }

    public void SetConsoles(bool setToOn)
    {
        for (int i = 0; i< consoleInteractsList.Count; i++)
        {
            if (consoleInteractsList[i] != null)
                consoleInteractsList[i].SetAlimented(setToOn);
        }
    }

    public void SetRayons(bool setToOn)
    {
        for (int i = 0; i < deadZonesList.Count; i++)
        {
            if (deadZonesList[i] != null)
                deadZonesList[i].SetAlimented(setToOn);
        }
    }

    public void SetAppear(bool setToOn)
    {
        for (int i = 0; i < AppearsList.Count; i++)
        {
            if (AppearsList[i] != null)
                AppearsList[i].gameObject.SetActive(setToOn);
        }
    }

    #region Move functions
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
    #endregion Move functions

}
