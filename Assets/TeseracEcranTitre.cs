using UnityEngine;
using DG.Tweening;  
public class TeseracEcranTitre : MonoBehaviour
{
    Tween rotationTween;

    void Start()
    {
        // Fait tourner l'objet de 360� sur Y en 10 secondes, en boucle infinie
        rotationTween = transform.DORotate(
            new Vector3(0, 360, 0), // Rotation de 360� sur Y
            30f, // Dur�e
            RotateMode.LocalAxisAdd // Permet de d�passer 360� pour une rotation continue
        )
        .SetRelative(true) // Applique la rotation de fa�on relative � la rotation actuelle
        .SetLoops(-1, LoopType.Incremental); // Boucle infinie, incr�mentale
    }
}
