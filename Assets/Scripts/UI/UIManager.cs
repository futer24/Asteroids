using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DentedPixel;
using Coffee.UIExtensions;
using System.Linq;
using System;

public class UIManager : MonoBehaviour
{
    public enum GamePanel
    {
        STARTPANEL = 0,
        WAVEPANEL,
        PLAYINGPANEL,
        GAMEOVERPANEL,
    }

    [Header("UI Panels")]
    public GameObject[] panels;
    public GameObject loadingPanel;

    [Header("UI Texts")]
    public TMP_Text scoreText;
    public TMP_Text hiScoreText;
    public TMP_Text livesText;
    public TMP_Text gameOverText;
    public TMP_Text waveText;

    [SerializeField] private ScoreScriptableObject scoreDataModel;

    [SerializeField] private GameObject floatingTextUI;



    private void Awake()
    {
        Enemy.OnEnemyDamage += ShowFloatingScoreText;
        GameManager.instance.OnGameDataUpdate += UpdateUI;
    }

    private void UpdateUI(GameDataScriptableObject data)
    {
        waveText.text = $"WAVE {data.Wave}";
        livesText.text = $"LIVES: {data.Lives}";
        UpdateScoreUI();
    }

    private void OnDestroy()
    {
        Enemy.OnEnemyDamage -= ShowFloatingScoreText;
        GameManager.instance.OnGameDataUpdate -= UpdateUI;
    }

    public void ShowFloatingScoreText(Vector3 pos, int score)
    {
        GameObject floatingTextInstance = Instantiate(floatingTextUI, panels[(int)GamePanel.PLAYINGPANEL].transform);
        TMP_Text  tmpText = floatingTextInstance.GetComponent<TMP_Text>();
        tmpText.text = score.ToString();
        tmpText.LeanFloatingDamageText(pos, 0f, 1f);

        AddScore(score);
   
    }

    public void SetActivePanel(GamePanel activePanel)
    {
        if((int)activePanel >= panels.Length) return;

        if(activePanel == GamePanel.WAVEPANEL || activePanel == GamePanel.GAMEOVERPANEL)
        {
            loadingPanel.GetComponent<Animator>().SetTrigger("FadeIn");
            StartCoroutine(Utils.DelayedAction(() =>
            {
                for (int i = 0; i < panels.Length; i++)
                {
                    panels[i].SetActive(panels[i] == panels[(int)activePanel]);
                }
            }, 1f
            ));
        } else
            {
                for (int i = 0; i < panels.Length; i++)
                {
                    panels[i].SetActive(panels[i] == panels[(int)activePanel]);
                }
            }
      
     
    }

    public void UpdateScoreUI()
    {
        scoreText.text = $"SCORE {scoreDataModel.Score}";
        hiScoreText.text = scoreDataModel.HighScore.ToString();
        gameOverText.text = $"GAME OVER \n\n Score {scoreDataModel.Score}";
    }

   

    public void AddScore(int score)
    {
        scoreDataModel.AddScore(score);
        UpdateScoreUI();
    }
   

    public void ResetScore()
    {
        scoreDataModel.InitValues();
    }
 
}
