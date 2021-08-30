using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenager : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    private int continueGameSceneIndex;

    //canvas
    GameObject Menu;
    GameObject AudioMenu;

    private void Start()
    {
        Menu = GameObject.Find("Menu");
        AudioMenu = GameObject.Find("AudioMenu");

        if (SceneManager.GetActiveScene().buildIndex.Equals(0))
        {
            Menu.SetActive(true);
        }
        else
        {
           Menu.SetActive(false);
        }
        AudioMenu.SetActive(false);

        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
        }
        Load();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!Menu.activeInHierarchy && !AudioMenu.activeInHierarchy)
            {
                Menu.SetActive(true);
                AudioMenu.SetActive(false);
                Time.timeScale = 0f;
            }
            else
            {
                Menu.SetActive(false);
                AudioMenu.SetActive(false);
                Time.timeScale = 1f;
            }
        }
    }
    public void Resume()
    {
        Menu.SetActive(false);
        AudioMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    //play button in menu
    public void ContinueGame()
    {
        SceneManager.LoadScene(continueGameSceneIndex);
    }
    //audio button in menu
    public void switchBetweenMenus()
    {
        if (Menu.activeSelf)
        {
            Menu.SetActive(false);
            AudioMenu.SetActive(true);
        }
        else if (AudioMenu.activeSelf)
        {
            Menu.SetActive(true);
            AudioMenu.SetActive(false);
        }
    }

    public void quitToMainMenu()
    {
        Save();
        SceneManager.LoadScene(0);
        Menu.SetActive(true);
    }

    public void Exit()
    {
        Save();
        Application.Quit();
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    private void Load()
    {
        if (volumeSlider != null)
        {
            volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
        }      
        //TODO izmeni ovo
        continueGameSceneIndex = 1;
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }
}
