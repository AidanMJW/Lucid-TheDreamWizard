using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LandChunkTileSet", menuName = "TileSets/LandChunkTileSet", order = 2)]
public class LandChunkTileSet : ScriptableObject
{
    public float spriteSize;


    [Space(10)]
    [Header("Corners")]
    public Sprite leftTopSprite;
    public Sprite leftBottomSprite;
    public Sprite rightTopSprite;
    public Sprite rightBottomSprite;

    [Space(10)]
    [Header("Sides")]
    public Sprite TopSprite;
    public Sprite BottomSprite;
    public Sprite rightSprite;
    public Sprite leftSprite;


    public List<Sprite> middleSprite = new List<Sprite>();
}
