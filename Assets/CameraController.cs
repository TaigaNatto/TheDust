using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float spinSpeed = 1.0f;

    public float startDistance;

    Vector3 nowPos;
    Vector3 pos = Vector3.zero;
    Vector2 mouse = Vector2.zero;

    int mouse_yFlag=0;

    bool mouseFlag=false;

    // Use this for initialization
    void Start()
    {

        // Canera get Start Position from Player
        //カメラの初期位置
        nowPos = transform.position;

        //ターゲットが無ければ自動でPlayerタグをフォーカス(今回は必要なし)
        if (target == null)
        {
            target = GameObject.FindWithTag("Player").transform;
            Debug.Log("player didn't setting. Auto search 'Player' tag.");
        }

        //初めのマウス位置(カメラの初期視点)
        mouse.y = 0.5f; // start mouse y pos ,0.5f is half
    }

    // Update is called once per frame
    void Update()
    {
        //Y軸の制限設定
        float minY = 0.01f;
        float maxY = 3.0f;

        float temp=0;

        /*
        //カメラが制限値までいったら
        if (mouse.y == maxY||mouse.y==minY)
        {
            if (!mouseFlag)
            {
                mouseFlag = true;
            }
            else
            {
                mouseFlag = false;
            }
        }
        // Get MouseMove
        if (!mouseFlag)
        {
            //マウスの移動を取得
            mouse += new Vector2(0, Input.GetAxis("Mouse Y")) * Time.deltaTime * spinSpeed;
        }
        else
        {
            //マウスの移動を取得
            mouse -= new Vector2(0, Input.GetAxis("Mouse Y")) * Time.deltaTime * spinSpeed;
        }
        mouse += new Vector2(Input.GetAxis("Mouse X"),0) * Time.deltaTime * spinSpeed;
        */
        mouse += new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * Time.deltaTime * spinSpeed;
        // Clamp mouseY move
        //mouse.y = Mathf.Clamp(mouse.y, -0.3f + 0.5f, 0.3f + 0.5f);

        //マウスのy座標の値を0～πの間に制限する
        mouse.y = Mathf.Clamp(mouse.y, minY, maxY);

        if (mouse.y < 0.5f)
        {
            mouse_yFlag = 1;
            temp = mouse.y - 0.5f;
        }
        else if (mouse.y > 2.5f)
        {
            mouse_yFlag = 2;
            temp = mouse.y - 2.5f;
        }
        else
        {
            temp = 0;
        }

        if (temp < 0)
        {
            temp = temp * -1f;
        }
        temp = temp * 1.1f;
        float distance = startDistance * (1f - temp);

        if (mouse.y >= 0.5f && mouse.y <= 2.5f) {
            //↑の場合
            if (mouse_yFlag == 1)
            {
                mouse.y = 2.5f;
            }
            //↓の場合
            if (mouse_yFlag == 2)
            {
                mouse.y = 0.5f;
            }
        }


        //float distance = startDistance;

        // sphere coordinates

        pos.y = -distance * Mathf.Cos(mouse.y * Mathf.PI);
        pos.x =-distance * Mathf.Cos(mouse.y+0.5f * Mathf.PI) * Mathf.Sin(mouse.x * Mathf.PI);
        pos.z = -distance * Mathf.Cos(mouse.y+0.5f * Mathf.PI) * Mathf.Cos(mouse.x * Mathf.PI);
        // r and upper
        pos *= nowPos.z;

        pos.y += nowPos.y;
        //pos.x += nowPos.x; // if u need a formula,pls remove comment tag.

        transform.position = pos + target.position;
        transform.LookAt(target.position);

		print (mouse.x+","+mouse.y);
    }
}