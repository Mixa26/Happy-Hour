using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenager : MonoBehaviour
{
    public Transform playerPosition;

    public Vector3 playerStartLevelPosition;

    public int playerLives;
    public int hammers;
    [HideInInspector]
    public bool collisionDelay;

    //player 
    private GameObject player;
    private GameObject life1;
    private GameObject life2;
    private GameObject life3;
    private GameObject Hammers;
    [HideInInspector]
    public GameObject numberOfHammers;
    PlayerMovement playerMovement;

    private int activeScene;

    private void Awake()
    {     
        collisionDelay = false;
        //player
        player = GameObject.Find("Player");
        life1 = GameObject.Find("life1");
        life2 = GameObject.Find("life2");
        life3 = GameObject.Find("life3");
        playerMovement = player.GetComponent<PlayerMovement>();
        if (activeScene != 1)
        {
            Hammers = GameObject.Find("Hammers");
            numberOfHammers = GameObject.Find("numberOfHammers");
            Hammers.SetActive(false);
            numberOfHammers.SetActive(false);
        }

        activeScene = SceneManager.GetActiveScene().buildIndex;

    }

    void CollisionDelay()
    {
        collisionDelay = false;
        playerMovement.enemyHit = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerPosition.position.y < -2 || playerLives <= 0)
        {
            playerMovement.dieAudio.Play();
        }
        if (playerPosition.position.y < -5 || playerLives <= 0)
        {
            KillPlayer();
        }

        if (playerMovement.enemyHit && !collisionDelay)
        {
            playerLives--;
            playerMovement.oohAudio.Play();
            playerMovement.enemyHit = false;
            collisionDelay = true;
            Invoke("CollisionDelay", 1f);
        }

        if (playerMovement.foodAte)
        {
            if (playerLives < 3)
            {
                playerLives++;
            }       
            playerMovement.foodAte = false;
        }

        if (playerLives == 3)
        {
            life3.SetActive(true);
            life2.SetActive(true);
            life1.SetActive(true);
        }
        else if(playerLives == 2)
        {
            life3.SetActive(false);
            life2.SetActive(true);
            life1.SetActive(true);
        }
        else if (playerLives == 1)
        {
            life3.SetActive(false);
            life2.SetActive(false);
            life1.SetActive(true);

        }
        else
        {
            life3.SetActive(false);
            life2.SetActive(false);
            life1.SetActive(false);
        }

        if (playerMovement.endLevel)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("loadStartLevelLocation", 1);
            playerMovement.endLevel = false;
        }

        if (activeScene != 1)
        {
            if (hammers > 0 && !Hammers.activeInHierarchy)
            {
                Hammers.SetActive(true);
                numberOfHammers.SetActive(true);
            }

            if (hammers <= 0)
            {
                Hammers.SetActive(false);
                numberOfHammers.SetActive(false);
            }
        }
    }

    private void KillPlayer()
    {
        playerPosition.position = playerStartLevelPosition;     
        playerLives = 3;
    }

}
