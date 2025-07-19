using DG.Tweening;
using System.Linq;
using UnityEngine;

public class ObjectToAlliment : MonoBehaviour
{
    bool powered = false;

    [SerializeField] Transform transformOn;
    [SerializeField] Transform transformOff;

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
        transform.DOMove(transformOn.position, 3);
    }

    public void SetPositionOff()
    {
        transform.DOMove(transformOff.position, 3);
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
