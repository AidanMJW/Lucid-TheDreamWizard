using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    /*
     * Attach this script to an empty gameobject in the scene
     * */


    public int numberOfBats = 3;

    // Reference to the overlay Text to display winning text, etc
    public Text m_MessageText;

    // Reference to the overlay Text to displaying game time
    public Text m_TimerText;

    //GUI component that holds buttons and highscores
    public GameObject m_HighScorePanel;

    //GUI text for display of formatted list of high scores
   // public Text m_HighScoresText;

    public Button m_NewGameButton;

   //tally for score
    private int score = 0;

    //Reference to the Highscores script - so we can write/read highscores
   // public HighScores m_HighScores;

    //our single dynamic collection of bats - grows and shrinks depending on level of play or keep same size and deactivate bats less work for garbage collector
    private static ArrayList _EnemyBatList;

    //a temp variable for creation of bat clones
    public GameObject m_Bat;

    
    //a reference to our player 
    public GameObject m_Player;

    //a reference to the bat Spawning point - may want to add a graphic to the scene dorrway/arch/cave
    public GameObject m_batCave;

    //a varible to track game duration - if we need it?
    private float m_gameTime = 0;

    //a getter property for gametime
    public float GameTime { get { return m_gameTime; } }



    // an enumaration for referring to  gamestate
    public enum GameState
    {
        Start,
        Playing,
        GameOver
    };

    //a reference to current gamestate
    private GameState m_GameState;

    //property gamestate - so we can refer to current gamestate using dot notation
    public GameState State { get { return m_GameState; } }


    private void Awake()
    {

        m_GameState = GameState.Start;
        _EnemyBatList = new ArrayList();

    }




    // Start is called before the first frame update
    void Start()
    {
        //set up listener for when bats are killed
        //BatHealth.onUnitDestroy += this.UpdateScore;

        //m_batCave = GameObject.FindGameObjectWithTag("BatCave");
        //creates our initial bats in at the BatCave and add to our arraylist
        //m_Bat = Instantiate(m_Bat, new Vector3(0, 0, 0), Quaternion.identity);
       // m_Bat = Instantiate(m_Bat, m_batCave.transform.position, Quaternion.identity);
        
       // _EnemyBatList.Add(m_Bat);

        //assigns values to our player and spawnpoint references
        m_Player = GameObject.FindGameObjectWithTag("Player");
       

        //sets each of the enemy  inactive ie invisible and not shooting/moving etc
        foreach (GameObject g in _EnemyBatList)
        {
            g.SetActive(false);
        }

        //provide start instruction
        m_TimerText.gameObject.SetActive(false);
        m_MessageText.text = "Enter to start";

        //hide the GUI elements we don't want visible during game play       
        m_NewGameButton.gameObject.SetActive(false);
       
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_GameState)
        {
            case GameState.Start:
                if (Input.GetKeyUp(KeyCode.Return) == true || Input.GetKeyUp(KeyCode.Mouse0) == true)
            {
                //At this point user has pressed enter to start game
                //we update the gamestate accordingly and set enemies active
               // m_TimerText.gameObject.SetActive(true);
                m_MessageText.text = "";
                m_GameState = GameState.Playing;

                m_Player.SetActive(true);
                foreach (GameObject g in _EnemyBatList)
                {
                    g.SetActive(true);
                }

            }
            break;
            case GameState.Playing:

                //update the game time and format it for GUI
                m_gameTime += Time.deltaTime;
                int seconds = Mathf.RoundToInt(m_gameTime);
                m_TimerText.text = string.Format("{0:D2}:{1:D2}", (seconds / 60), (seconds % 60));

                //check if player dead
                if (IsPlayerDead())
                {
                    //display score in GUI             
                    m_MessageText.text = "LOSE Score " + score;
                    //m_HighScores.AddScore(score);

                    //since player died set their score back to zero - ready for if they try again
                    score = 0;
                    m_GameState = GameState.GameOver;
                    m_TimerText.gameObject.SetActive(false);

            
                    //makes buttons visible and sets text
                    m_NewGameButton.gameObject.SetActive(true);               
                    m_NewGameButton.GetComponentInChildren<Text>().text = "Try again?";
                }
               /* else if (!AnyActiveEnemy())
                {
                    //if no active enemies player has finished level - may need to change this to check is Boss dead if enemies respawn
                    //display score in GUI 
                    m_MessageText.text = "Level Cleared Score " + score;                   
                    m_GameState = GameState.GameOver;
                    m_TimerText.gameObject.SetActive(false);
                                    

                    //make buttons visible and sets text
                    m_NewGameButton.gameObject.SetActive(true);                   
                    m_NewGameButton.GetComponentInChildren<Text>().text = "Next level?";

                }*/

                break;
                case GameState.GameOver:
                    //nothing to do here since button clicked events now handle restart of game
                break;
        }
    }

    private void UpdateScore(GameObject unit)
    {
        switch(unit.tag)
        {
            case "Bat":
                score++;
                break;

            case "Ghost":

            break;
        }
       
    }

    //a check to see if any enemy still active - note player tank no longer in this
    private bool AnyActiveEnemy()
    {
        int numEnemyLeft = 0;
        foreach (GameObject g in _EnemyBatList)
        {
            if (g.activeSelf == true)
            {
                numEnemyLeft++;
            }
        }
        Debug.Log("AnyActiveEnemy=" + numEnemyLeft);
        return numEnemyLeft > 0;
    }

    //a check to see if player is active and therefore still alive
    private bool IsPlayerDead()
    {
        if (m_Player.activeSelf == false)
        {
            Debug.Log("IsPlayerDead- true");
            return true;
        }
        else
        {
           // Debug.Log("IsPlayerDead- false");
            return false;
        }
    }

    //hooked up to our new game button onclick event. Puts us in playing gamestate
    public void OnNewGame()
    {
        Debug.Log("OnNewGame- PLAYING");
        //re-enabling the player  causes its health to be reset
        // m_Player.GetComponent<Health>().enabled = false;
        // m_Player.GetComponent<Health>().enabled = true;

        //set gamestate and set up GUI to suite playing gamestate
        m_NewGameButton.gameObject.SetActive(false);       
        m_HighScorePanel.SetActive(false);
        m_gameTime = 0;
        m_GameState = GameState.Playing;
        m_TimerText.gameObject.SetActive(true);
        m_MessageText.text = "";

        //set all enemy active to recommence gameplay
        m_Player.SetActive(true);
        foreach (GameObject g in _EnemyBatList)
        {
            g.SetActive(true);
        }
    }
}
