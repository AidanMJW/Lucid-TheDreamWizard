using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BackgroundScroller : MonoBehaviour
{
    public Transform bgBottomLeft;
    public Transform bgTopLeft;
    public Transform bgBottomRight;
    public Transform bgTopRight;
    public Transform cam;

    private bool swapY = true;
    private bool swapX = true;

    private const float bgHeight = 6.38f;
    private const float bgWidth = 9.6f;
    private float currentYPos = bgHeight;
    private float currentXPos = bgWidth;

    private void Start()
    {
       
    }
    // Update is called once per frame
    void Update()
    {
       
        if (currentYPos < cam.position.y)
        {//we going up
            
            //move lowest row up 2 places
            if (swapY)
            {
                 bgBottomLeft.localPosition = new Vector3(bgBottomLeft.localPosition.x, bgBottomLeft.localPosition.y + bgHeight *2 , 10);
                 bgBottomRight.localPosition = new Vector3(bgBottomRight.localPosition.x, bgBottomRight.localPosition.y + bgHeight *2, 10);
                //Debug.Log("going up swap bgBottomLeft & bgBottomRight " + " cam.position.y=" + cam.position.y + " currentYPos=" + currentYPos);
            }
            else
            {
                 bgTopLeft.localPosition = new Vector3(bgTopLeft.localPosition.x, bgTopLeft.localPosition.y + bgHeight *2 , 10);
                 bgTopRight.localPosition = new Vector3(bgTopRight.localPosition.x, bgTopRight.localPosition.y + bgHeight *2, 10);
               // Debug.Log("going up !swap bgTopRight & bgTopLeft " + " cam.position.y=" + cam.position.y + " currentYPos=" + currentYPos);
            }

            currentYPos += bgHeight;
            swapY = !swapY;


        } else if (currentYPos > cam.position.y + bgHeight)
        {//we going down

            //move highest row down 2 places
            if (swapY)
            {
                //Debug.Log("going down !swap bgTopRight & bgTopLeft " + " cam.position.y=" + cam.position.y + " currentYPos=" + currentYPos);
                bgTopLeft.localPosition = new Vector3(bgTopLeft.localPosition.x, bgTopLeft.localPosition.y - bgHeight *2 , 10);
                bgTopRight.localPosition = new Vector3(bgTopRight.localPosition.x, bgTopRight.localPosition.y - bgHeight *2, 10);
            }
            else
            {
                //Debug.Log("going down !swap bgBottomLeft & bgBottomRight " + " cam.position.y=" + cam.position.y + " currentYPos=" + currentYPos);
                bgBottomLeft.localPosition = new Vector3(bgBottomLeft.localPosition.x, bgBottomLeft.localPosition.y - bgHeight *2, 10);
                bgBottomRight.localPosition = new Vector3(bgBottomRight.localPosition.x, bgBottomRight.localPosition.y - bgHeight *2, 10);
            }

            currentYPos -= bgHeight;
            swapY = !swapY;

        }

        if (currentXPos < cam.position.x)
        {//we going right
          
           
            //move left most column right 2 places
            if (swapX)
            {
                
              //Debug.Log("going right swap bgBottomLeft & bgTopLeft " + " cam.position.x=" + cam.position.x + " currentXPos=" + currentXPos);
                bgBottomLeft.localPosition = new Vector3(bgBottomLeft.localPosition.x + bgWidth *2 , bgBottomLeft.localPosition.y, 10);
                bgTopLeft.localPosition = new Vector3(bgTopLeft.localPosition.x + bgWidth *2, bgTopLeft.localPosition.y, 10);
            }
            else
            {
                bgBottomRight.localPosition = new Vector3(bgBottomRight.localPosition.x + bgWidth*2 , bgBottomRight.localPosition.y, 10);
                bgTopRight.localPosition = new Vector3(bgTopRight.localPosition.x + bgWidth*2 , bgTopRight.localPosition.y, 10);
            }

            currentXPos += bgWidth;
            swapX = !swapX;

        } else if(currentXPos > cam.position.x + bgWidth)
        {//we going left
           
            //move right most column left 2 places
            if (swapX)
            {
                bgTopRight.localPosition = new Vector3(bgTopRight.localPosition.x - bgWidth*2 , bgTopRight.localPosition.y, 10);
                bgBottomRight.localPosition = new Vector3(bgBottomRight.localPosition.x - bgWidth * 2, bgBottomRight.localPosition.y, 10);
            }
            else
            {
               bgBottomLeft.localPosition = new Vector3(bgBottomLeft.localPosition.x - bgWidth*2 , bgBottomLeft.localPosition.y, 10);
               bgTopLeft.localPosition = new Vector3(bgTopLeft.localPosition.x - bgWidth*2, bgTopLeft.localPosition.y, 10);
            }
            currentXPos -= bgWidth;
            swapX = !swapX;
        }
    }
}
