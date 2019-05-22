using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{
    bool menuOpen = false;
    static bool gamePaused = false;
    public GameObject mainMenu;
    public Button mainMenuStartButton;
    public GameObject SettingsMenu;
    public Button settingsStartButton;
    public DifficultyManager difficultyManager;
    public Text difficultyText;
    public EventSystem ES;

    public List<Difficulty> difficulties = new List<Difficulty>();
    bool difficultyChanged = false;
    public List<Difficulty> _dif = new List<Difficulty>();

    private void Start()
    {
        difficultyText.text = difficulties[0].difficultyName;
        toggleMenu();
    }

    void Update()
    {
        if(Input.GetButtonDown("Start"))
        {
            toggleMenu();
        }
    }

    void toggleMenu()
    {
        if (menuOpen == false)
        {
            mainMenu.SetActive(true);
            //ES.SetSelectedGameObject(mainMenuStartButton.gameObject);
            gamePaused = true;
            Time.timeScale = 0f;
            menuOpen = true;
        }
           
        else
        {
            closeMenu();
        }
                  
    }

    public void closeMenu()
    {
        mainMenu.SetActive(false);
        SettingsMenu.SetActive(false);
        gamePaused = false;
        Time.timeScale = 1f;
        menuOpen = false;
    }

    public void exitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    public static bool getPauseState()
    {
        return gamePaused;
    }
    
    public void switchToMainMenu()
    {
        SettingsMenu.SetActive(false);
        mainMenu.SetActive(true);
       // ES.SetSelectedGameObject(mainMenuStartButton.gameObject);
    }

    public void switchToSettingsMenu()
    {
         _dif.Clear();

        for (int i = 0; i < difficulties.Count; i++)
        {
            _dif.Add(difficulties[i]);
        }

        difficultyText.text = difficulties[0].difficultyName;

        mainMenu.SetActive(false);
        SettingsMenu.SetActive(true);
       // ES.SetSelectedGameObject(settingsStartButton.gameObject);
    }

    public void toggleDifficultyRight()
    {

        if(difficulties.Count>1)
        {
            difficultyChanged = true;
            _dif.Add(_dif[0]);
            _dif.RemoveAt(0);
            difficultyText.text = _dif[0].difficultyName;
        }
    }


    public void toggleDifficultyLeft()
    {
        if (difficulties.Count > 1)
        {
            difficultyChanged = true;
            _dif.Insert(0, _dif[_dif.Count - 1]);
            _dif.RemoveAt(_dif.Count - 1);
            difficultyText.text = _dif[0].difficultyName;
        }
    }

    public void confirmSettings()
    {
        if(difficultyChanged)
        {
            difficulties.Clear();
            for(int i = 0; i < _dif.Count; i++)
            {
                difficulties.Add(_dif[i]);
            }

            difficultyManager.difficulty = difficulties[0];
            difficultyChanged = false;
        }
    }
}
