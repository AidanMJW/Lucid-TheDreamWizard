using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject settingsMenu;
    public GameObject settingsStartButton;
    public GameObject mainMenu;
    public GameObject mainMenuStartButton;
    public GameObject controlsSettings;
    public GameObject controlsStartButton;
    public EventSystem ES;

    public List<Difficulty> difficulties = new List<Difficulty>();
    bool difficultyChanged = false;
    public List<Difficulty> _dif = new List<Difficulty>();
    public DifficultyManager difficultyManager;
    public Text difficultyText;

    void Start()
    {
        Time.timeScale = 1;
        for (int i = 0; i < difficulties.Count; i++)
        {
            _dif.Add(difficulties[i]);
        }
        difficultyText.text = _dif[0].difficultyName;

        if(DifficultyManager.difficulties.Count == 0)
        {
            for (int i = 0; i < difficulties.Count; i++)
            {
                DifficultyManager.difficulties.Add(difficulties[i]);
            }
        }
    }


    void Update()
    {

    }

    public void switchToMain()
    {
        settingsMenu.SetActive(false);
        controlsSettings.SetActive(false);
        mainMenu.SetActive(true);
        if (Input.GetJoystickNames().Length > 0)
            ES.SetSelectedGameObject(mainMenuStartButton);
        
    }
    public void switchToSettings()
    {
        mainMenu.SetActive(false);
        controlsSettings.SetActive(false);
        settingsMenu.SetActive(true);
        if (Input.GetJoystickNames().Length > 0)
            ES.SetSelectedGameObject(settingsStartButton);
    }
    public void switchToControls()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(false);
        controlsSettings.SetActive(true);
        if (Input.GetJoystickNames().Length > 0)
            ES.SetSelectedGameObject(controlsStartButton);
    }

    public void goToScene(int sceneIndex)
    {
        difficultyChanged = true;
        confirmSettings();
        SceneManager.LoadScene(sceneIndex);
    }

    public void toggleDifficultyRight()
    {

        if (difficulties.Count > 1)
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
        if (difficultyChanged)
        {
            difficulties.Clear();
            for (int i = 0; i < _dif.Count; i++)
            {
                difficulties.Add(_dif[i]);
            }

            DifficultyManager.difficulties.Clear();
            for (int i = 0; i < _dif.Count; i++)
            {
                DifficultyManager.difficulties.Add(_dif[i]);
            }

            DifficultyManager.difficulty = difficulties[0];
            difficultyChanged = false;
        }
    }

    public void exitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
