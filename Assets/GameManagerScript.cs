using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    void PrintArray()
    {
        string debugText = "";
        for (int i = 0; i < map.Length; i++)
        {
            debugText += map[i].ToString() + ",";
        }
        Debug.Log(debugText);
    }
    int GetPlayerIndex()
    {
        for(int i = 0; i< map.Length; i++)
        {
            if (map[i] == 1)
            {
                return i;
            }
        }
        return -1;
    }
    bool MoveNumber(int number,int moveFrom,int moveTo)
    {
        //移動可能か判断
        if(moveTo < 0||moveTo >= map.Length) { return false; }
        //移動先に箱があれば
        if (map[moveTo] == 2)
        {
            //どの方向へ移動するか
            int velocity = moveTo - moveFrom;
            //プレイヤーの移動先からさらに先へ2(箱)を移動させる
            //箱の移動処理。movenumberメゾット内でmovenumberメゾット化
            //移動可不可をboolで記録
            bool success = MoveNumber(2, moveTo, moveTo + velocity);
            //箱の移動が失敗したらplayerも移動できない
            if (!success) { return false; }
        }
        //プレイヤー・箱変わらずの移動処理
        map[moveTo] = number;
        map[moveFrom] = 0;
        return true;
    }
    //配列の宣言
    int[] map;
    // Start is called before the first frame update
    void Start()
    {
        //配列の実態の作成と初期化
        map = new int[] { 0, 0, 0, 1, 0, 2, 0, 0, 0 };
        PrintArray();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            int playerIndex = GetPlayerIndex();
            MoveNumber(1, playerIndex, playerIndex + 1);
            PrintArray();
        }
        /*
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            int playerIndex = -1;
            for (int i = 0; i < map.Length; i++)
            {
                if (map[i] == 1)
                {
                    playerIndex = i;
                    break;
                }
            }
            if (playerIndex > 0)
            {
                map[playerIndex - 1] = 1;
                map[playerIndex] = 0;
            }
            string debugText = "";
            for (int i = 0; i < map.Length; i++)
            {
                debugText += map[i].ToString() + ",";
            }
            Debug.Log(debugText);
        }
        */
    }
}
