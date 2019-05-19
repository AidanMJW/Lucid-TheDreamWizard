using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    //win text

    //lose text

    //Game states - won, lost, playing, paused 
    //start: we haven't started menu displayed
    //won: display menu and win text. Condition > 0 lives and boss defeated
    //lost: display menu and lost text. Condition: 0 lives
    //playing: hide menu and remove text
    //paused: display menu and pause game

    public MenuManager menu;


    // Start is called before the first frame update
    void Start()
    {
        menu = GetComponent<MenuManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
