using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    mainManager mainManager;
    grannyManager grannyManager;
    public GameObject GrannyManager;
    public GameObject playerOne;
    public GameObject playerTwo;
    private InputManager inputOne;
    private InputManager inputTwo;
    public ghettoBlasterSpawner gbSpawner;


    //Scenes
    public GameObject SDScene1;
    public GameObject SDScene2;
    public GameObject Start_three;
    public GameObject Start_two;
    public GameObject Start_one;
    public GameObject Start_go;

    public GameObject firstWin1;
    public GameObject firstWin2;

    public Text timerText;
    int countDownTime = 60;

    private AudioSource audioPlayer;
    public AudioSource finalKiss;
    public AudioClip soundscape;
    public AudioSource suddenDeathSound;


    private void Awake()
    {
        grannyManager = GrannyManager.GetComponent<grannyManager>();
    }

    public void Start()
    {
        Application.targetFrameRate = 60;
        mainManager = GameObject.Find("mainManager").GetComponent<mainManager>();
        //playerOne.SetActive(false);
        //playerTwo.SetActive(false);
        audioPlayer = GetComponent<AudioSource>();
        playerOne.GetComponent<InputManager>().waitFor(240);
        playerTwo.GetComponent<InputManager>().waitFor(240);
        if (mainManager.getWinsForOne() != 0) { firstWin2.SetActive(true); }
        if (mainManager.getWinsForTwo() != 0) { firstWin1.SetActive(true); }
        StartCoroutine(StartCountDown());
        StartCoroutine(grannyManager.activateStartingGrannys());
        timerText.text = "1:00";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            togglePauseGame();
        }

    }

    bool running = true;
    void togglePauseGame()
    {
        if (running) { Time.timeScale = 0f; running = false; }
        else { Time.timeScale = 1f; running = true; }
        playerOne.SetActive(running);
        playerTwo.SetActive(running);
        GrannyManager.SetActive(running);
    }

    public GameObject gameOverOne;
    public GameObject gameOverTwo;
    public GameObject gameOverp1;
    public GameObject gameOverp2;

    public IEnumerator gameOver(bool p1)
    {
        gameOverOne.SetActive(true);
        yield return new WaitForSeconds(2);
        gameOverOne.SetActive(false);
        gameOverTwo.SetActive(true);
        finalKiss.Play();
        yield return new WaitForSeconds(2);
        gameOverTwo.SetActive(false);

        if (p1) 
        {
            if (mainManager.getWinsForOne() == 0) 
            {
                Debug.Log("First round goes to " + p1);
                mainManager.win(p1);
                yield return new WaitForSeconds(2);
                rematch();
            }
            else
            {
                gameOverp1.SetActive(true);
                activateBtns(true);
                mainManager.resetWins();
            }
        }

        else 
        {
            if (mainManager.getWinsForTwo() == 0)
            {
                Debug.Log("First round goes to " + p1);
                mainManager.win(p1);
                yield return new WaitForSeconds(2);
                rematch();
            }
            else
            {
                gameOverp2.SetActive(true);
                activateBtns(true);
                mainManager.resetWins();
            }
        }
    }

    public IEnumerator StartCountDown()
    {
        Start_three.SetActive(true);
        yield return new WaitForSeconds(1.2f);
        audioPlayer.Play();
        audioPlayer.loop = true;
        Start_three.SetActive(false);
        Start_two.SetActive(true);
        yield return new WaitForSeconds(1.2f);
        Start_two.SetActive(false);
        Start_one.SetActive(true);
        yield return new WaitForSeconds(1.2f);
        Start_one.SetActive(false);
        Start_go.SetActive(true);
        yield return new WaitForSeconds(1.2f);
        Start_go.SetActive(false);

        Debug.Log("Start countdown");

        StartCoroutine(timer());
    }

    public IEnumerator timer()
    {
        grannyManager.spawnGranny();

        for (int j = 0; j < 3; j++)
        {
            for (int i = 0; i < 10; i++)
            {
                yield return new WaitForSeconds(1);
                countDown();
            }
            grannyManager.spawnGranny();
            gbSpawner.spawn();
        }
        //one minute passed

        for (int j = 0; j < 6; j++)
        {
            for (int i = 0; i < 5; i++)
            {
                yield return new WaitForSeconds(1);
                countDown();
            }
            grannyManager.spawnGranny();
            gbSpawner.spawn();
        }

        //one miute passed. Sudden Death!

        StartCoroutine(suddenDeath());
    }
    public IEnumerator suddenDeath()
    {
        StartCoroutine(suddenDeathScene());
        while (true)
        {
            yield return new WaitForSeconds(3);
            grannyManager.spawnGranny();
            gbSpawner.spawn();
        }
    }

    public IEnumerator suddenDeathScene()
    {
        bool change = true;
        suddenDeathSound.Play();
        for (int i = 0; i < 5; i++)
        {
            SDScene1.SetActive(change);
            SDScene2.SetActive(!change);
            yield return new WaitForSeconds(.5f);
            change = !change;
        }
        SDScene1.SetActive(false);
        SDScene2.SetActive(false);
    }

    void countDown()
    {
        countDownTime--;
        getTime();
    }

    void getTime()
    {
        string displayTime = (countDownTime / 60 + ":" + (countDownTime % 60));
        timerText.text = displayTime;
    }

    public GameObject backBtn;
    public GameObject rematchBtn;

    void activateBtns(bool enable)
    {
        backBtn.SetActive(enable);
        rematchBtn.SetActive(enable);
    }
    public void backToMain()
    {
        SceneManager.LoadScene("Startscreen");
    }
    public void rematch()
    {
        SceneManager.LoadScene("Ingame");
    }
}
