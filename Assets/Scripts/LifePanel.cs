using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePanel : MonoBehaviour
{
    public GameObject[] icons;

    //ライフに応じてスプライトを出しわける
    public void UpdateLife(int life)
    {
        for (int i = 0; i < icons.Length; i++)
        {
            if (i < life) icons[i].SetActive(true); //そのゲームオブジェクトを表示するか否かを指示するsetActive
            else icons[i].SetActive(false);
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
