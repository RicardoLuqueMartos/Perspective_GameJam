using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

[RequireComponent(typeof(LineRenderer))]
public class RayonEmission : MonoBehaviour
{
    private LineRenderer rayonRenderer;
    private List<RaycastHit> hits = new List<RaycastHit>();
    bool hasHitNotReflector = true;
    bool endWithOutHit;
    public bool powered;
    Vector3 directionWithOutHit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created  
    void Start()
    {

        rayonRenderer = GetComponent<LineRenderer>();
        rayonRenderer.enabled = false;
    }

    // Update is called once per frame  
    void FixedUpdate()
    {
        if (powered)
        {
            hits.Clear(); // R�initialise la liste � chaque frame
            hasHitNotReflector = true;
            endWithOutHit = false;

            Physics.Raycast(transform.position, transform.forward, out RaycastHit hit);
            if (hit.collider == null)
            {
                rayonRenderer.positionCount = 2;
                rayonRenderer.SetPosition(0, transform.position);
                rayonRenderer.SetPosition(1, transform.position + transform.forward * 100f); // Default length if no hit  
            }
            else
            {
                if (!hit.collider.CompareTag("Reflecteur"))
                {
                    rayonRenderer.positionCount = 2;
                    hasHitNotReflector = true;
                    rayonRenderer.SetPosition(0, transform.position);
                    rayonRenderer.SetPosition(1, hit.point);
                }
                else
                {
                    hits.Add(hit);
                    hasHitNotReflector = false;
                    endWithOutHit = false;
                    Vector3 currentDirection = transform.forward;
                    Vector3 currentHit = hit.point;
                    RaycastHit lastHit = hit;

                    while (true)
                    {
                        Vector3 newDirection = Vector3.Reflect(currentDirection, lastHit.normal);
                        Debug.Log("Rayon Direction: " + newDirection);
                        Physics.Raycast(lastHit.point, newDirection, out RaycastHit newhit);
                        Debug.DrawRay(lastHit.point, lastHit.normal, Color.red, 2f);
                        if (newhit.collider != null && !newhit.collider.CompareTag("Reflecteur"))
                        {
                            hits.Add(newhit);
                            hasHitNotReflector = true;
                            break;
                        }
                        else if (newhit.collider != null && newhit.collider.CompareTag("Reflecteur"))
                        {
                            Debug.Log("Rayon hit reflector: " + newhit.collider.name);
                            hits.Add(newhit);
                            currentDirection = newDirection;
                            lastHit = newhit;
                        }
                        else if (newhit.collider == null)
                        {
                            directionWithOutHit = newDirection;
                            hasHitNotReflector = true;
                            endWithOutHit = true;
                            break;
                        }
                        else if (hits.Count >= 50) // Limite de rebonds pour �viter les boucles infinies
                        {
                            Debug.LogWarning("Rayon has hit too many reflectors, stopping to prevent infinite loop.");
                            break;
                        }
                    }

                    Debug.Log("Rayon has hit " + hits.Count + " reflectors.");
                    // Calcul du nombre total de points
                    int totalPoints = hits.Count + 1; // +1 pour le point de d�part
                    if (endWithOutHit)
                        totalPoints++; // +1 pour le point final extrapol�

                    rayonRenderer.positionCount = totalPoints;

                    // Point de d�part
                    rayonRenderer.SetPosition(0, transform.position);

                    // Points d'impact
                    for (int i = 0; i < hits.Count; i++)
                    {
                        rayonRenderer.SetPosition(i + 1, hits[i].point);
                    }

                    // Si le rayon finit sans impact
                    if (endWithOutHit)
                    {

                        rayonRenderer.SetPosition(hits.Count + 1, directionWithOutHit * 100f);
                    }
                }
            }
        }
    }

    public void TurnOn()
    {
        powered = true ;
        rayonRenderer.enabled = true;
    }

    public void TurnOff()
    {
        powered = false;
        // Pour d�sactiver le LineRenderer, il faut utiliser la propri�t� 'enabled' (avec un 'd' minuscule) :
        rayonRenderer.enabled = false; // R�initialise le LineRenderer
    }
}
