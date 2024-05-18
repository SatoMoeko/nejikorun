using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;



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
    }

    int CalcScore()
    {
        //ねじこの走行距離をscoreとする
        return (int)nejiko.transform.position.z;
    }
}
