using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFollow : MonoBehaviour
{
    Vector3 diff;

    public GameObject target;
    public float followSpeed;

    // Start is called before the first frame update
    void Start()
    {
        diff = target.transform.position - transform.position; //ねじこの位置座標ひくカメラの位置座標＝スタート時のねじこからカメラの距離
    }

    // Update is called once per frame
    void LateUpdate() //lateupdate>ほかのスクリプトのupdateのなかでも一番最後にうごくように指定できる
    {
        transform.position = Vector3.Lerp( //lerp
            transform.position,
            target.transform.position - diff, //ねじこの位置座標からスタート時のねじこ＋かめらの距離をひくことでカメラの位置をわりだす
            Time.deltaTime * followSpeed //すこしおくれてねじこについていく。第三引数を１ｆにすると遅れのないカメラになる
        );
    }
}
