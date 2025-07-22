using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "GameSettingsData", menuName = "Scriptable Objects/GameSettingsData")]
public class GameSettingsData : ScriptableObject
{
    public Sprite tesseractSprite;

    public int ReflectorMoveForce = 10;
}
