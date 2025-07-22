using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "GameSettingsData", menuName = "Scriptable Objects/GameSettingsData")]
public class GameSettingsData : ScriptableObject
{
    public Sprite tesseractSprite;

    public int ReflectorMoveForce = 10;

    [Header("RenderTexture Settings")]
    public int renderTextureCurrentSizeX = 854;
    public int renderTextureCurrentSizeY = 480;
    public FilterMode m_filterMode = FilterMode.Point;
    public GraphicsFormat format = GraphicsFormat.R8G8B8A8_UNorm;
    public GraphicsFormat depthFormat = GraphicsFormat.D32_SFloat_S8_UInt;
}
