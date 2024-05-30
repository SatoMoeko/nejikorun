using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

public class TitleController : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;

    public void OnStartButtonClicked()
    {
        SceneManager.LoadScene("Main");
    }

    // Start is called before the first frame update
    void Start()
    {
        //ハイスコアを表示

        highScoreText.text = $"High Score : {PlayerPrefs.GetInt("HighScore")}m";

    }

    // Update is called once per frame
    void Update()
    {

    }
}
