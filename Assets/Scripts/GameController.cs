using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class GameController : MonoBehaviour
{
    public NejikoController nejiko;
    public TextMeshProUGUI scoreText;
    public LifePanel lifePanel;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //スコアを更新
        int score = CalcScore();
        scoreText.text = $"Score : {score}m";

        //ライフパネルを更新
        lifePanel.UpdateLife(nejiko.Life()); //lifepanelスクリプトのなかのlifeメソッドを実行

        //ねじこのライフが０になったらゲームオーバー
        if (nejiko.Life() <= 0 || nejiko.transform.position.y < 0)
        {
            //これ以降のUpdateは止める
            enabled = false;

            //ハイスコアを更新
            if (PlayerPrefs.GetInt("HighScore") < score)
            {
                PlayerPrefs.SetInt("HighScore", score);
            }

            //2秒後にReturnToTitleを呼びだす
            Invoke("ReturnToTitle", 2.0f);
        }
    }

    int CalcScore()
    {
        //ねじこの走行距離をscoreとする
        return (int)nejiko.transform.position.z;
    }

    void ReturnToTitle()
    {
        //タイトルシーンに切り替え
        SceneManager.LoadScene("Title");
    }
}
