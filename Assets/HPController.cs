using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPController : MonoBehaviour
{

    //システムオブジェクト
    GameObject engineObj;

    // Use this for initialization
    void Start()
    {
        engineObj = GameObject.Find("engine");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "dust")
        {
            if (collision.gameObject.GetComponent<Renderer>().material.color != Color.white)
            {
                //hp低下
                GameEngine gE = engineObj.GetComponent<GameEngine>();
                gE.setHP(gE.getHP() - 1);
            }
        }
    }
    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "dust")
        {
            if (collision.gameObject.GetComponent<Renderer>().material.color != Color.white)
            {
                //hp増加
                GameEngine gE = engineObj.GetComponent<GameEngine>();
                gE.setHP(gE.getHP() + 1);
            }
        }
    }
}
