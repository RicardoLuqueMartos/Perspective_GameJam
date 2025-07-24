using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;

[RequireComponent(typeof(LineRenderer))]
public class RayonEmission : MonoBehaviour
{
    [SerializeField] int maxRebonds = 50;
    private LineRenderer rayonRenderer;
    private List<RaycastHit> hits = new List<RaycastHit>();
    bool endWithOutHit;
    public bool powered;
    Vector3 directionWithOutHit;

    [SerializeField] private ReccepteurRayon currentReccepteurRayon;

    // Start is called once before the first execution of Update after the MonoBehaviour is created  
    void Start()
    {

        rayonRenderer = GetComponent<LineRenderer>();

        if (powered)
        {
            rayonRenderer.enabled = true;
        }
        else
        {
            rayonRenderer.enabled = false; // Désactive le LineRenderer si le rayon n'est pas alimenté
        }
    }

    // Update is called once per frame  
    void FixedUpdate()
    {
        if (powered)
        {
            hits.Clear(); // Réinitialise la liste à chaque frame
            endWithOutHit = false;

            Physics.Raycast(transform.position, transform.forward, out RaycastHit hit);
            if (hit.collider == null)
            {
                rayonRenderer.positionCount = 2;
                rayonRenderer.SetPosition(0, transform.position);
                rayonRenderer.SetPosition(1, transform.position + transform.forward * GameSettingsManager.instance.gameSettingsData.RayLengthNoHit); // Default length if no hit  
            }
            else
            {
                if (hit.collider.CompareTag("Player"))
                {
                    Debug.Log("Rayon hit & kill Player");
                    RBPlayer.instance.KillPlayer();
                    return;
                }
                else if(!hit.collider.CompareTag("Reflecteur"))
                {
                    rayonRenderer.positionCount = 2;
                    rayonRenderer.SetPosition(0, transform.position);
                    rayonRenderer.SetPosition(1, hit.point);
                 
                    if (hit.collider != null && hit.collider.GetComponent<ReccepteurRayon>())
                        EnpowerReceptor(hit);
                }
               
                else
                {
                    hits.Add(hit);
                    endWithOutHit = false;
                    Vector3 currentDirection = transform.forward;
                    Vector3 currentHit = hit.point;
                    RaycastHit lastHit = hit;

                    while (true)
                    {
                        Vector3 newDirection = Vector3.Reflect(currentDirection, lastHit.normal);
                        //Debug.Log("Rayon Direction: " + newDirection);
                        Vector3 startPoint = lastHit.point + newDirection * 0.01f; // Offset pour éviter le décalage
                        Physics.Raycast(startPoint, newDirection, out RaycastHit newhit);
                        //Debug.DrawRay(lastHit.point, lastHit.normal, Color.red, 2f);
                        if (newhit.collider != null && !newhit.collider.CompareTag("Reflecteur"))
                        {
                            hits.Add(newhit);
                            break;
                        }
                        else if (newhit.collider != null && newhit.collider.CompareTag("Reflecteur"))
                        {
                            //    Debug.Log("Rayon hit reflector: " + newhit.collider.name);
                            hits.Add(newhit);
                            currentDirection = newDirection;
                            lastHit = newhit;
                        }
                        else if (newhit.collider == null)
                        {
                            directionWithOutHit = newDirection;
                            endWithOutHit = true;
                            break;
                        }

                        if (hits.Count >= maxRebonds) // Limite de rebonds pour éviter les boucles infinies
                        {
                            Debug.LogWarning("Rayon has hit too many reflectors, stopping to prevent infinite loop.");
                            break;
                        }
                    }

                 //   Debug.Log("Rayon has hit " + hits.Count + " reflectors.");
                    // Calcul du nombre total de points
                    int totalPoints = hits.Count + 1; // +1 pour le point de départ
                    if (endWithOutHit)
                        totalPoints++; // +1 pour le point final extrapolé

                    rayonRenderer.positionCount = totalPoints;

                    // Point de départ
                    rayonRenderer.SetPosition(0, transform.position);

                    // Points d'impact
                    for (int i = 0; i < hits.Count; i++)
                    {
                        rayonRenderer.SetPosition(i + 1, hits[i].point);
                    }

                    // Si le rayon finit sans impact
                    if (endWithOutHit)
                    {
                        Vector3 lastPoint = hits[hits.Count - 1].point;
                        rayonRenderer.SetPosition(hits.Count + 1, lastPoint + directionWithOutHit * 100f);
                        if (currentReccepteurRayon != null)
                        currentReccepteurRayon.SetPowered(false);
                        currentReccepteurRayon = null;
                    }

                    if (!endWithOutHit && hits.Count > 0)
                    {
                        RaycastHit lastHitOnSurface = hits[hits.Count - 1];

                        EnpowerReceptor(lastHitOnSurface);
                   /*     if (lastHitOnSurface.collider.GetComponent<ReccepteurRayon>())
                        { 
                            currentReccepteurRayon = lastHitOnSurface.collider.GetComponent<ReccepteurRayon>();
                            if (!currentReccepteurRayon.powered)
                            currentReccepteurRayon.SetPowered(true);
                        }
                        else if (currentReccepteurRayon != null)
                        {
                            currentReccepteurRayon.SetPowered(false);
                            currentReccepteurRayon = null;
                        }*/
                    }
                }
            }
            if (!endWithOutHit && hits.Count > 0) {
                if (hits[hits.Count - 1].collider.CompareTag("Player"))
                {
                    Debug.Log("Rayon hits & kills Player");
                    RBPlayer.instance.KillPlayer();
                    return;
                }
            //    else Debug.Log("Rayon ending on " + hits[hits.Count - 1].collider.name);
            }
        //    else Debug.Log("RayonRenderer has no hits, setting to default length.");
        }
    }

    void EnpowerReceptor(RaycastHit lastHitOnSurface)
    {
        if (lastHitOnSurface.collider.GetComponent<ReccepteurRayon>())
        {
            currentReccepteurRayon = lastHitOnSurface.collider.GetComponent<ReccepteurRayon>();
            if (!currentReccepteurRayon.powered)
                currentReccepteurRayon.SetPowered(true);
        }
        else if (currentReccepteurRayon != null)
        {
            currentReccepteurRayon.SetPowered(false);
            currentReccepteurRayon = null;
        }
    }

    public void TurnOn()
    {
        rayonRenderer = GetComponent<LineRenderer>();
        powered = true ;
        rayonRenderer.enabled = true;
        SoundLauncher.instance.PlayAltarPowerOn();
    }

    public void TurnOff()
    {
        powered = false;
        if (currentReccepteurRayon != null)
        {
            currentReccepteurRayon.SetPowered(false);
            currentReccepteurRayon = null;
        }
        // Pour désactiver le LineRenderer, il faut utiliser la propriété 'enabled' (avec un 'd' minuscule) :
        rayonRenderer.enabled = false; // Réinitialise le LineRenderer
        SoundLauncher.instance.PlayAltarPowerOff();
    }
}
