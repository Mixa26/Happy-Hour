using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenager : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    private int continueGameSceneIndex;
    private PlayerMovement playerMovement;
    private LevelMenager levelMenager;

    //canvas
    GameObject Menu;
    GameObject AudioMenu;

    private int activeSceneIndex;

    private void Awake()
    {
        Menu = GameObject.Find("Menu");
        AudioMenu = GameObject.Find("AudioMenu");
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            levelMenager = GameObject.Find("LevelMenager").GetComponent<LevelMenager>();
        }


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
        activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().buildIndex != 0)
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

    public void StartNewGame()
    {
        SceneManager.LoadScene(1);
        PlayerPrefs.DeleteAll();
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
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
        Menu.SetActive(true);
    }

    public void Exit()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            Save();
        }        
        Application.Quit();
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    private void Load()
    {        
        if (PlayerPrefs.HasKey("playerLives") && SceneManager.GetActiveScene().buildIndex != 0)
        {
            if (PlayerPrefs.HasKey("loadStartLevelLocation") && !(PlayerPrefs.GetInt("loadStartLevelLocation") == 1))
            {
                float x, y, z;             
                x = PlayerPrefs.GetFloat("playerPosX");
                y = PlayerPrefs.GetFloat("playerPosY");
                z = 0;
                Vector3 playerPosition = new Vector3(x, y, z);
                playerMovement.gameObject.transform.position = playerPosition;             
            }
            else
            {
                playerMovement.gameObject.transform.position = levelMenager.playerStartLevelPosition;
                
            }
            levelMenager.playerLives = PlayerPrefs.GetInt("playerLives");
            PlayerPrefs.SetInt("loadStartLevelLocation", 0);
        }
        else if (!PlayerPrefs.HasKey("playerLives") && SceneManager.GetActiveScene().buildIndex != 0)
        {
            levelMenager.playerLives = 3;
        }

        if (PlayerPrefs.HasKey("hammers") && SceneManager.GetActiveScene().buildIndex > 1)
        {
            levelMenager.hammers = PlayerPrefs.GetInt("hammers");
            levelMenager.numberOfHammers.GetComponent<Text>().text = PlayerPrefs.GetInt("hammers").ToString();
        }
        else if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            levelMenager.hammers = 0;
        }

        if (PlayerPrefs.HasKey("continueGameSceneIndex"))
        {
            continueGameSceneIndex = PlayerPrefs.GetInt("continueGameSceneIndex");
        }
        else
        {
            continueGameSceneIndex = 1;
        }

        if (volumeSlider != null)
        {
            volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
        }

    }

    private void Save()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            PlayerPrefs.SetInt("playerLives", levelMenager.playerLives);
            PlayerPrefs.SetInt("hammers", levelMenager.hammers);
            PlayerPrefs.SetFloat("playerPosX", playerMovement.gameObject.transform.position.x);
            PlayerPrefs.SetFloat("playerPosY", playerMovement.gameObject.transform.position.y);
            PlayerPrefs.SetFloat("playerPosZ", playerMovement.gameObject.transform.position.x);
            PlayerPrefs.SetInt("continueGameSceneIndex", SceneManager.GetActiveScene().buildIndex);
        }
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }
}
