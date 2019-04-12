using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlatformTileSet", menuName = "TileSets/PlatformTileSet", order = 1)]
public class PlatformTileSet : ScriptableObject
{
    public float spriteSize;

    public Sprite leftCapSprite;
    public Sprite rightCapSprite;
    public List<Sprite> middleSprite = new List<Sprite>();
}
