using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandChunkGenerator : MonoBehaviour
{
    public LandChunkTileSet tileSet;

    [Space(10)]
    public int landChunkLength;
    public int landChunkHeight;

    [Space(10)]
    public bool hasTop;
    public bool hasBottom;
    public bool hasRight;
    public bool hasLeft;

    [Space(10)]
    public bool hasTopLeftCorner;
    public bool hasBottomLeftCorner;
    public bool hasTopRightCorner;
    public bool hasBottomRightCorner;

    [Space(10)]
    public Vector3 spawnPosition;

    [Space(10)]
    public GameObject landCollider;

    public void buildLandChunk()
    {
        GameObject landChunk = new GameObject("landChunk");
        landChunk.transform.position = spawnPosition;

        Vector2 newsize = new Vector2(tileSet.spriteSize * landChunkLength, tileSet.spriteSize * landChunkHeight);
        Vector2 offset = new Vector2((tileSet.spriteSize * (landChunkLength - 1)) / 2, (tileSet.spriteSize * (landChunkHeight - 1)) / 2);

        GameObject oCollider = Instantiate(landCollider, landChunk.transform);
        oCollider.GetComponent<BoxCollider2D>().size = newsize;
        oCollider.GetComponent<BoxCollider2D>().offset = offset;

        for(int x = 0; x < landChunkLength; x++)
        {
            for(int y = 0; y < landChunkHeight; y++)
            {
                if(x == 0 && y == 0 && hasBottomLeftCorner)
                {
                    createTile(x, y, tileSet.leftBottomSprite, landChunk);
                }
                else if (x == 0 && y == landChunkHeight -1 && hasTopLeftCorner)
                {
                    createTile(x, y, tileSet.leftTopSprite, landChunk);
                }
                else if (x == landChunkLength - 1 && y == 0 && hasBottomRightCorner)
                {
                    createTile(x, y, tileSet.rightBottomSprite, landChunk);
                }
                else if (x == landChunkLength -1 && y == landChunkHeight -1 && hasTopRightCorner)
                {
                    createTile(x, y, tileSet.rightTopSprite, landChunk);
                }
                else if (x == 0 && y != 0 && hasLeft)
                {
                    createTile(x, y, tileSet.leftSprite, landChunk);
                }
                else if (x != 0 && y == 0 && hasBottom)
                {
                    createTile(x, y, tileSet.BottomSprite, landChunk);
                }
                else if (x == landChunkLength -1 && y != 0 && hasRight)
                {
                    createTile(x, y, tileSet.rightSprite, landChunk);
                }
                else if (x != 0 && y == landChunkHeight -1 && hasTop)
                {
                    createTile(x, y, tileSet.TopSprite, landChunk);
                }
                else
                {
                    createTile(x, y, tileSet.middleSprite[Random.Range(0,tileSet.middleSprite.Count)], landChunk);
                }

            }
        }
    }

    void createTile(int x, int y, Sprite s, GameObject platform)
    {
        GameObject tile = new GameObject("tile");
        tile.transform.parent = platform.transform;
        tile.AddComponent<SpriteRenderer>();
        tile.GetComponent<SpriteRenderer>().sprite = s;

        Vector3 pos = platform.transform.position;
        pos.x += tileSet.spriteSize * x;
        pos.y += tileSet.spriteSize * y;
        tile.transform.position = pos;
    }
}
