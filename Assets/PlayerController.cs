using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameObject planet;
    public GameObject point;
	public float speed;
   
    private AudioSource[] sources;

    //システムオブジェクト
    GameObject engineObj;

    void FixedUpdate()
    {
        // 向かう向きの取得
        var direction = point.transform.position - this.transform.position;
        direction.Normalize();

        // 加速度与える
        this.gameObject.GetComponent<Rigidbody>().AddForce(speed * direction, ForceMode.Acceleration);

        if (Input.GetMouseButtonDown(0))
        {
            // 向かう向きの取得
            var powerDirection = point.transform.position - this.transform.position;
            powerDirection.Normalize();
            // 加速度与える
            this.gameObject.GetComponent<Rigidbody>().AddForce(speed * powerDirection*10, ForceMode.Acceleration);
        }

    }

    // Use this for initialization
    void Start () {
        sources = gameObject.GetComponents<AudioSource>();
        engineObj = GameObject.Find("engine");

        Renderer r = GetComponent<Renderer>(); //Rendererコンポーネントを取得（Material取得のため）
        r.material.EnableKeyword("_EMISSION");
        r.material.SetColor("_EmissionColor", new Color(Random.value, Random.value, Random.value));
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision collision)
    {
        GameEngine gE = engineObj.GetComponent<GameEngine>();
        if (gE.getMode() != 2)
        {
            if (collision.gameObject.tag == "dust")
            {
                if (collision.gameObject.GetComponent<Renderer>().material.color == Color.white)
                {
                    //再生
                    sources[0].Play();
                    //破壊
                    Destroy(collision.gameObject);

                    //得点加算
                    gE.plusScore(40);

                    gE.plusBreakNum();
                }
                else
                {
                    //再生
                    sources[1].Play();
                    //色変え
                    collision.gameObject.GetComponent<Renderer>().material.color = Color.white;
                    collision.gameObject.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");

                    //得点加算
                    gE.plusScore(10);
                    //hp増加
                    gE.setHP(gE.getHP() + 1);

                    gE.plusHitNum();

                    //試作
                    //this.gameObject.transform.localScale = transform.localScale * 1.001f;
                }
            }
            else
            {
                //それ以外の処理
            }
        }
    }
}
