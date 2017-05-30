using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameEngine : MonoBehaviour {

    //0:title
    //1:mainGame
    //2:result
    private int mode = 0;//title
    //score
    private int score=0;
    //HitPoint of star
    private int hp=50;

    private int hitNum=0;
    private int breakNum=0;

    public GameObject player;
    Vector3 playerStartScale;

    public GameObject planet;
    Vector3 planetScale;
    public GameObject planetGround;
    Vector3 planetGroundScale;

    public Text textScore; //score
    public Text textHP; //hp

    public Text textHitNum; //hitNum
    public Text textBreakNum; //breakNum
    public Text textBonusPoint;//bonus
    public Text textResultScore; //resultScore
    public Text textWinBonus; //resultScore

    public Image imageField;

    public Sprite spriteNone;
    public Sprite spriteWin;
    public Sprite spriteLose;

    public Button buttonRank;
    public Button buttonContinue;

    public Image panel;
    float alfa=1;
    bool panelFlag = false;

    bool specialSkillFlag = false;
    int specialSkillNum=0;

    private AudioSource BGM;

    //カメラ
    GameObject cameraObj;

    int resultCnt = 0;

    //カウントダウン
    int cnt = 5000;

    public bool gameJudge = false;

    int resultScore;

    // Use this for initialization
    void Start () {
        BGM = this.gameObject.GetComponent<AudioSource>();
        cameraObj = GameObject.Find("Main Camera");

        textScore.text = "";
        textHP.text = "";

        textHitNum.text = "";
        textBreakNum.text = "";
        textBonusPoint.text = "";
        textResultScore.text = "";
        textWinBonus.text = "";

        buttonRank.gameObject.SetActive(false);
        buttonContinue.gameObject.SetActive(false);

        planetScale = planet.gameObject.transform.localScale;
        planetGroundScale = planetGround.gameObject.transform.localScale;

        playerStartScale = player.gameObject.transform.localScale;
    }
	
	// Update is called once per frame
	void Update () {


        if (hp <= 0||cnt<=0)
        {
            BGM.Stop();
            mode = 2;
            textScore.text = "";
            textHP.text = "";
            if (cnt <= 0)
            {
                gameJudge = true;
            }
            else
            {
                gameJudge = false;
            }
        }

        if (mode == 1)
        {
            imageField.sprite = spriteNone;
            if (!BGM.isPlaying)
            {
                BGM.Play();
            }
            textScore.text = "Score:" + score;
            textHP.text = "残り時間:" + cnt;

            if (score / (1000*(specialSkillNum+1))>specialSkillNum)
            {
                specialSkillFlag = true;
                specialSkillNum++;
            }

            if (specialSkillFlag)
            {
                //分身生成
                Instantiate(player, new Vector3(Random.Range(10f, 100f), Random.Range(10f, 100f), Random.Range(10f, 100f)), player.transform.rotation);
                
            }
            specialSkillFlag = false;

            planet.transform.localScale = planetScale * 0.02f*hp;
            planetGround.transform.localScale = planetGroundScale * 0.02f*hp;

            //planet.transform.localScale = planetScale * 1.02f * hp;
            //planetGround.transform.localScale = planetGroundScale * 1.02f * hp;

            cnt--;
        }
        else if(mode==2)
        {
            if (resultCnt >= 100)
            {
                if (resultCnt == 100)
                {
                    buttonContinue.gameObject.SetActive(true);
                    buttonRank.gameObject.SetActive(true);
                    int bonusPoint;
                    bonusPoint = (hitNum - breakNum) * 500;
                    if (bonusPoint < 0)
                    {
                        bonusPoint = 0;
                    }
                    //時間切れの場合(生存ルート)
                    if (gameJudge)
                    {
                        score = score + 10000;
                        
                        imageField.sprite = spriteWin;
                        textBonusPoint.text = "ボーナスポイント:" + bonusPoint;
                        textWinBonus.text = "生存ボーナス+10000!";
                    }
                    //破滅ルート
                    else
                    {
                        imageField.sprite = spriteLose;
                        textBonusPoint.text = "ボーナスポイント:" + bonusPoint;
                        textWinBonus.text = "残り時間:"+cnt+"!";
                    }
                    textHitNum.text = "無力化数:" + hitNum + "×10=" + (hitNum * 10);
                    textBreakNum.text = "破壊数:" + breakNum + "×40=" + (breakNum * 40);
                    resultScore = score + bonusPoint;
                    textResultScore.text = "最終スコア:" + resultScore;

                    resultCnt = 200;

                }
                
                if (panelFlag)
                {
                    buttonContinue.gameObject.SetActive(false);
                    buttonRank.gameObject.SetActive(false);
                    if (alfa < 1)
                    {
                        alfa = alfa + 0.005f;
                        panel.GetComponent<Image>().color = new Color(0, 0, 0, alfa);
                    }
                    else
                    {
                        player.gameObject.transform.localScale = playerStartScale;
                        //scene再読み込み
                        SceneManager.LoadScene("main");
                    }
                }
            }
            else
            {
                //離れていく～
                CameraController cC = cameraObj.GetComponent<CameraController>();
                cC.startDistance = cC.startDistance + 1;
                resultCnt++;
                if (resultCnt == 50)
                {
                    //生存したとき
                    if (gameJudge)
                    {
                        //飛来物全滅
                        GameObject[] tagobjs = GameObject.FindGameObjectsWithTag("dust");
                        foreach (GameObject obj in tagobjs)
                        {
                            Destroy(obj);
                        }
                    }
                }
            }
        }
        //title
        else if (mode == 0)
        {
            if (alfa > 0)
            {
                alfa = alfa - 0.01f;
                panel.GetComponent<Image>().color = new Color(0, 0, 0, alfa);
            }
            
        }

    }

    public void rankClicked()
    {
        //ランキング機能
        naichilab.RankingLoader.Instance.SendScoreAndShowRanking(resultScore);
    }

    //imageが押された時
    public void continueClicked()
    {
        panelFlag = true;
    }

    //score
    public void plusScore(int plusPoint)
    {
        score = score + plusPoint;
    }

    //hp
    public void setHP(int nowHP)
    {
        hp = nowHP;
    }
    public int getHP()
    {
        return hp;
    }

    public void setMode(int modeNum)
    {
        if (modeNum >= 0 && modeNum <= 2)
        {
            mode = modeNum;
        }
    }
    public int getMode()
    {
        return mode;
    }

    public void plusHitNum()
    {
        hitNum++;
    }
    public void plusBreakNum()
    {
        breakNum++;
    }
}
