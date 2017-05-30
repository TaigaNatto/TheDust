using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMakeDust : MonoBehaviour {

    public GameObject[] dusts;

    int cntAll=0;
    int cnt = 0;

    int num = 100;

    //システムオブジェクト
    GameObject engineObj;

    // Use this for initialization
    void Start () {
        engineObj = GameObject.Find("engine");
    }
	
	// Update is called once per frame
	void Update () {

        GameEngine gE = engineObj.GetComponent<GameEngine>();
        if (gE.getMode() != 0&&!gE.gameJudge)
        {

            if (cntAll > 750)
            {
                num = num / 2; 
                cntAll = 0;
            }
            else
            {
                cntAll++;
            }

            if (cnt >= num)
            {
                //生成
                //オブジェクトの指定
                GameObject makeObj = dusts[Random.Range(0, dusts.Length)];
                //オブジェクトの大きさ設定
                makeObj.transform.localScale = new Vector3(Random.Range(0.5f, 2.5f), Random.Range(0.5f, 2.5f), Random.Range(0.5f, 2.5f));
                //生成開始
                Instantiate(makeObj, new Vector3(Random.Range(-100, 100f), Random.Range(-100, 100), Random.Range(-100, 100)), makeObj.transform.rotation);
                cnt = 0;
            }
            else
            {
                cnt++;
            }
        }
        else
        {
            cntAll = 0;
            cnt = 0;
            num = 100;
        }
	}
}
