using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class NejikoController : MonoBehaviour
{
    const int MinLane = -2; //レーンのサイズ
    const int MaxLane = 2;
    const float LaneWidth = 1.0f;

    CharacterController controller;
    Animator animator;

    Vector3 moveDirection = Vector3.zero; //
    int targetLane;

    public float gravity;
    public float speedz;
    public float speedx;
    public float speedJump;
    public float accelerationZ; //加速値

    // Start is called before the first frame update
    void Start()
    {
        //必要なコンポーネントを自動取得
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //デバッグ用
        if (Input.GetKeyDown("left")) MoveToLeft();
        if (Input.GetKeyDown("right")) MoveToRight();
        if (Input.GetKeyDown("space")) Jump();

        //徐々に加速しZ方向に常に前進させる
        float acceleratedZ = moveDirection.z + (accelerationZ * Time.deltaTime);
        moveDirection.z = Mathf.Clamp(acceleratedZ, 0, speedz); //clampで加速時の速度制限を追加、mathminとmathmaxを同時にやっている感じ

        //x方向は目標のポジションまでの差分の割合で速度を計算
        float ratioX = (targetLane * LaneWidth - transform.position.x) / LaneWidth;
        moveDirection.x = ratioX * speedx;

        if (controller.isGrounded) //地面に接しているか判定するisGrounded
        {
            if (Input.GetAxis("Vertical") > 0.0f) //つまりバックなし前進のみ,verticalは上下キーのこと
            {
                moveDirection.z = Input.GetAxis("Vertical") * speedz;
            }
            else
            {
                moveDirection.z = 0;
            }

            transform.Rotate(0, Input.GetAxis("Horizontal") * 3, 0);

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = speedJump;
                animator.SetTrigger("jump"); //アニメーション発動
            }
        }

        //ジャンプしたときの動きを表現するため、重力分の力を毎フレーム追加
        moveDirection.y -= gravity * Time.deltaTime; //timedeltatimeでゆるやかに減算していく
        //ジャンプしてすぐが一番重力がかかってなくてだんだん重力がかかっておちる

        //移動実行　これをやらないとねじこの向きにかかわらず移動が実行されてしまう
        Vector3 globalDirection = transform.TransformDirection(moveDirection); //transformDirectionでねじこの向きを考慮したベクトルに変換している
        controller.Move(globalDirection * Time.deltaTime);

        //移動後接地してたらY方向の速度はリセットする　これをやらないと床がない部分にいったときにものすごいスピードでおちていってしまう
        if (controller.isGrounded) moveDirection.y = 0;

        //速度が０以上なら走っているフラグをtrueにする
        animator.SetBool("run", moveDirection.z > 0.0f);
    }

    //左のレーンにいどうを開始
    public void MoveToLeft()
    {
        if (controller.isGrounded && targetLane > MinLane) targetLane--;
    }

    //右のレーンに移動を開始
    public void MoveToRight()
    {
        if (controller.isGrounded && targetLane < MaxLane) targetLane++;
    }

    public void Jump()
    {
        if (controller.isGrounded)
        {
            moveDirection.y = speedJump;

            //ジャンプトリガーを設定
            animator.SetTrigger("jump");
        }
    }
}
