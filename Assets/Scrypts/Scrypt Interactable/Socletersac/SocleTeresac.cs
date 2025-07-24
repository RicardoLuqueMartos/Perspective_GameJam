using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class SocleTeresac : MonoBehaviour, IInteractable
{
    [SerializeField] string contextuelTXT_needTeserac;
    [SerializeField] string contextuelTXT_empty;
    [SerializeField] string contextuelTXT_full;
    [SerializeField] RayonEmission rayon;
    [SerializeField] List<RayonEmission> rayonsList = new();

    [SerializeField] Transform socleTeresac;

    [SerializeField] public GameObject teresac;

    PlayerInteract _playerInteract;

    public bool _isControlled = false;

    void OnEnable()
    {
        InvokeRepeating("UpdateRayon", .1f, .1f);
    }

    public bool isControlled()
    {
        return rayon.powered;
    }
    public void IsInteractable(RaycastHit hit)
    {
        if (hit.collider != null && !rayon.powered)
        {
            if (!rayon.powered && GameProgressManager.instance.HaveTesseract())
            {
                UiManager.instance.contectuelInteracted(contextuelTXT_empty);
            }
            else if (!rayon.powered && !GameProgressManager.instance.HaveTesseract())
            {
                UiManager.instance.contectuelInteracted(contextuelTXT_needTeserac);
            }
        }
    }
    public void Interact(PlayerInteract player)
    {
        Debug.Log("Interact SocleTeresac");
        RayonEmission rayonEmission = rayon;

        bool isOn = false;

        if (!rayon.powered && GameProgressManager.instance.HaveTesseract())
        {
            GameProgressManager.instance.PlayerUseTesseract();

            teresac = Instantiate(GameProgressManager.instance.prefabTeresac, socleTeresac.position, Quaternion.identity);
            teresac.transform.SetParent(socleTeresac);
            isOn = true;
        }
        else if (rayon.powered)
        {
            GameProgressManager.instance.PlayerGetTesseract();
            Destroy(teresac);
        }

        InteractRayon(player, rayonEmission, isOn);
        for (int i = 0; i < rayonsList.Count; i++)
        {
            InteractRayon(player, rayonsList[i], isOn);
        }
    }

    public void InteractRayon(PlayerInteract player, RayonEmission rayonEmission, bool isOn) 
    { 
        if (isOn)
            rayonEmission.TurnOn();
         else       
            rayonEmission.TurnOff();
    }

    void TurnRayonOff(RayonEmission rayonEmission)
    {
        rayonEmission.powered = false;
        //    GameProgressManager.instance.PlayerGetTesseract();
        rayonEmission.TurnOff();
    }

    public void LeaveInteract()
    {
        
    }

    void UpdateRayon()
    {
        if (teresac == null && rayon.powered)
        {
            RayonEmission rayonEmission = rayon;
            TurnRayonOff(rayonEmission);
            for (int i = 0; i < rayonsList.Count; i++)
            {
                if (rayonEmission.powered) TurnRayonOff(rayonEmission);
            }            
        }
    }
}
