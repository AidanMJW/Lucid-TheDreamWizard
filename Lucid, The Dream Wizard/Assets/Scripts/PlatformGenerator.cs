using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public PlatformTileSet tileSet;

    [Space(10)]
    public int platformLength;
    public bool hasLeftCap;
    public bool hasRightCap;

    [Space(10)]
    public Vector3 spawnPosition;

    [Space(10)]
    public GameObject platformColider;

    public void buildPlatform()
    {
        GameObject platform = new GameObject("platform");
        platform.transform.position = spawnPosition;

        Vector2 newsize = new Vector2(tileSet.spriteSize * platformLength, tileSet.spriteSize);
        Vector2 offset = new Vector2((tileSet.spriteSize * (platformLength -1)) / 2, 0);

        GameObject oCollider = Instantiate(platformColider, platform.transform);
        oCollider.GetComponent<BoxCollider2D>().size = newsize;
        oCollider.GetComponent<BoxCollider2D>().offset = offset;

        for (int i = 0; i < platformLength; i++)
        {
            if (i == 0 && hasLeftCap)
            {
                createPlatformTile(i, tileSet.leftCapSprite, platform);
            }
            else if (i == platformLength -1 && hasRightCap)
            {
                createPlatformTile(i, tileSet.rightCapSprite, platform);
            }
            else
            {
                Sprite s = tileSet.middleSprite[Random.Range(0, tileSet.middleSprite.Count)];
                createPlatformTile(i, s, platform);
            }
        }
    }

    void createPlatformTile(int multipler,Sprite s, GameObject platform)
    {
        GameObject tile = new GameObject("tile");
        tile.transform.parent = platform.transform;
        tile.AddComponent<SpriteRenderer>();
        tile.GetComponent<SpriteRenderer>().sprite = s;

        Vector3 pos = platform.transform.position;
        pos.x = tileSet.spriteSize * multipler;
        tile.transform.position = pos;
    }
}
