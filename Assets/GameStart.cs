using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
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

    private void OnDestroy()
    {
        GameEngine gE = engineObj.GetComponent<GameEngine>();
        gE.setMode(1);
    }
}
