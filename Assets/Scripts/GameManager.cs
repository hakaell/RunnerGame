using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;
    public CameraManager _cameraManager;
    private TextMeshProUGUI _scoreTextIntro;
    private TextMeshProUGUI _scoreTextMain;
    private TextMeshProUGUI _scoreTextEnd;
    private TextMeshProUGUI _scoreTextDead;
    private TextMeshProUGUI _highScoreTextDead;
    private TextMeshProUGUI _highScoreTextEnd;
    
    public GameObject introCanvas;
    public GameObject inGameCanvas;
    public GameObject deadGameCanvas;
    public GameObject endGameCanvas;
    
    public GameObject collectablePrefab;
    public GameObject obstacklePrefab;

    public List<GameObject> collectables;
    public List<GameObject> obstackles;
    public PlayerController playerController;
    public Transform player;
    public int health = 3;
    public int score=0;
    public int highScore=0;
    public enum GameState
    {
        Prepare,
        MainGame,
        DeadGame,
        EndGame
    }

    private GameState _currentGameState;
    public GameState CurrentGameState
    {
        get { return _currentGameState; }
        set
        {
            switch (value)
            {
                case GameState.Prepare:
                    break;
                case GameState.MainGame:
                    break;
                case GameState.DeadGame:
                    break;
                case GameState.EndGame:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }

            _currentGameState = value;
        }
    }
    private void Awake()
    {
        manager = this;
        _scoreTextIntro = introCanvas.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        _scoreTextMain = inGameCanvas.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        _scoreTextEnd = endGameCanvas.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        _scoreTextDead = deadGameCanvas.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        _highScoreTextDead = deadGameCanvas.transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        _highScoreTextEnd = endGameCanvas.transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        SpawnCoins();
        SpawnObstackle();
    }

    private void Update()
    {
        UpScore();
        switch (GameManager.manager.CurrentGameState)
        {
            case GameManager.GameState.Prepare:
                print("prepare state");
                //resetleme ve
                //level yükleme aşaması - instantiate
               playerController.IdleAnimation();
                break;
            case GameManager.GameState.MainGame:
                print("main state");
                if (playerController.isInjured)
                {
                    playerController.RunningAnimation();
                }
                playerController.RunningAnimation();
                playerController.PlayerMove();
                playerController.SliderMove();
                break;
            case GameManager.GameState.DeadGame:
                playerController.DeadAnimation();
                
                print("deadgame state");
                break;
            case GameManager.GameState.EndGame:
                playerController.DanceAnimation();
                print("endgame state");
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void playerResetPos()
    {
        player.localPosition = Vector3.zero;
        playerController.speed = 4;
        print("playerpos reset");
    }
     public void UpScore()
     {
         if (score<0)
         {
             score = 0;
         }
         _scoreTextIntro.text = "" + score;
         _scoreTextMain.text = "" + score;
         _scoreTextEnd.text = "" + score;
         _scoreTextDead.text = "" + score;
         PlayerPrefs.SetInt("Score",score);
         if (score>highScore)
         {
             highScore = score;
             PlayerPrefs.SetInt("highScore",highScore);
         }

         _highScoreTextDead.text = "" + highScore;
         _highScoreTextEnd.text = "" + highScore;
     }
    public void TaptoPlay()
    {
        playerResetPos();
        _currentGameState = GameState.MainGame;
        CanvasSelect();
    }
    public void TryAgain()
    {
        score = 0;
        PlayerPrefs.GetInt("highScore");
        playerResetPos();
        DestroyObject();
        //camera reset
        //camPos reset 
        var temp = _cameraManager.camPos;
        temp.position = Vector3.zero;
        
        //main cam resetlenecek !!!!
        var temp2 = _cameraManager._camera;
        temp2.transform.position = Vector3.zero;
        temp2.transform.rotation = Quaternion.identity;
        
        SpawnCoins();
        SpawnObstackle();
        _currentGameState = GameState.Prepare;
        CanvasSelect();
    }
    public void NextLevel()
    {
        score = 0;
        PlayerPrefs.GetInt("highScore");
        playerResetPos();
        DestroyObject();
        var temp = _cameraManager.camPos;
        temp.position = Vector3.zero;
        
        //main cam resetlenecek !!!!
        var temp2 = _cameraManager._camera;
        temp2.transform.position = Vector3.zero;
        temp2.transform.rotation = Quaternion.identity;
        //skor kaydedilecek
        //skor devri olacak
        //skor , high skora aktarılacak
        //level texti upgradelenecek
        
        SpawnCoins();
        SpawnObstackle();
        _currentGameState = GameState.Prepare;
        CanvasSelect();
    }
    public void CanvasSelect()
    {
        if (introCanvas.gameObject.activeInHierarchy == false)
        {
            introCanvas.gameObject.SetActive(true);
            inGameCanvas.gameObject.SetActive(false);
            endGameCanvas.gameObject.SetActive(false);
            deadGameCanvas.gameObject.SetActive(false);
            Time.timeScale = 0;
        }
        else
        {
            introCanvas.gameObject.SetActive(false);
            inGameCanvas.gameObject.SetActive(true);
            endGameCanvas.gameObject.SetActive(false);
            deadGameCanvas.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void EndCanvas()
    {
        introCanvas.gameObject.SetActive(false);
        inGameCanvas.gameObject.SetActive(false);
        endGameCanvas.gameObject.SetActive(true);
        deadGameCanvas.gameObject.SetActive(false);
    }
  
    public void SpawnCoins()
    {
        
        for (int i = 0; i < 5; i++)
        {
            //orta
            var temp =Instantiate(collectablePrefab, new Vector3(0, 0, i), Quaternion.identity);
            collectables.Add(temp);
            collectables.Add(Instantiate(collectablePrefab, new Vector3(0, 0, i + 40f), Quaternion.identity));
            collectables.Add(Instantiate(collectablePrefab, new Vector3(0, 0, i + 70f), Quaternion.identity));
        }
        for (int i = 0; i < 5; i++)
        {
            //ensol
            collectables.Add(Instantiate(collectablePrefab, new Vector3(-2f, 0, i + 10f), Quaternion.identity));
            collectables.Add( Instantiate(collectablePrefab, new Vector3(-2f, 0, i + 25f), Quaternion.identity));
            collectables.Add(Instantiate(collectablePrefab, new Vector3(-2f, 0, i + 50f), Quaternion.identity));
            collectables.Add(Instantiate(collectablePrefab, new Vector3(-2f, 0, i + 80f), Quaternion.identity));
        }
        for (int i = 0; i < 5; i++)
        {
            //ensağ
            collectables.Add(Instantiate(collectablePrefab, new Vector3(1.5f, 0, i + 18.22f), Quaternion.identity));
            collectables.Add(Instantiate(collectablePrefab, new Vector3(1.5f, 0, i + 33.22f), Quaternion.identity));
            collectables.Add(Instantiate(collectablePrefab, new Vector3(1.5f, 0, i + 58.22f), Quaternion.identity));

        }
        
        
        print("coin oluşturuldu");
        
    }
    
    public void SpawnObstackle()
    {
        //orta
        obstackles.Add(Instantiate(obstacklePrefab, new Vector3(0.04f, 0.5f, 8f), Quaternion.identity));
        obstackles.Add(Instantiate(obstacklePrefab, new Vector3(0.04f, 0.5f, 47.5f), Quaternion.identity));
        obstackles.Add(Instantiate(obstacklePrefab, new Vector3(0.04f, 0.5f, 77.5f), Quaternion.identity));
        //en sol
        obstackles.Add(Instantiate(obstacklePrefab, new Vector3(-2.015f, 0.5f, 18.5f), Quaternion.identity));
        obstackles.Add(Instantiate(obstacklePrefab, new Vector3(-2.015f, 0.5f, 33.5f), Quaternion.identity));
        obstackles.Add(Instantiate(obstacklePrefab, new Vector3(-2.015f, 0.5f, 58.5f), Quaternion.identity));
        obstackles.Add(Instantiate(obstacklePrefab, new Vector3(-2.015f, 0.5f, 88.5f), Quaternion.identity));
        
        //ensağ
        obstackles.Add(Instantiate(obstacklePrefab, new Vector3(1.72f, 0.5f, 26.75f), Quaternion.identity));
        obstackles.Add(Instantiate(obstacklePrefab, new Vector3(1.5f, 0.5f, 41.75f), Quaternion.identity));
        obstackles.Add(Instantiate(obstacklePrefab, new Vector3(1.27f, 0.5f, 66.75f), Quaternion.identity));
        print("obstackle oluşturuldu");
        
    }

    public void DestroyObject()
    {
        foreach (var item in collectables.ToList())
        {
            collectables.Remove(item);
            Destroy(item);
        }
        print("coins destroy edildi");
        foreach (var item in obstackles.ToList())
        {
            obstackles.Remove(item);
            Destroy(item);
        }
        print("obstacles destroy edildi");
    }
    
}
