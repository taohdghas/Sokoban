using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
using System;
using Unity.VisualScripting ;
*/
public class GameManagerScript : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject boxPrefab;
    public GameObject clearText;
    public GameObject goalPrefab;
    public GameObject ParticlePrefab;
    //配列の宣言
    int[,] map;
    GameObject[,] field;
    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1280, 720, false);
        //マップの生成
        map = new int[,]
   {
        {0,0,0,0,0 },
        {0,3,1,3,0 },
        {0,0,2,0,0 },
        {0,2,3,2,0 },
        {0,0,0,0,0 },
    };
        //フィールドサイズ決定
        field = new GameObject [
            map.GetLength(0),
            map.GetLength(1)
            ];
        string debugText = "";
       
        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                debugText += map[y, x].ToString() + ",";
                if (map[y,x] == 1)
                {
                    field[y,x] = Instantiate(
                     playerPrefab, new Vector3(x, map.GetLength(0) - y, 0), Quaternion.identity);
                }
                if (map[y,x] == 2)
                {
                    field[y,x] = Instantiate(
                        boxPrefab,new Vector3(x,map.GetLength(0) - y,0),Quaternion.identity);
                }
                if (map[y,x] == 3)
                {
                    field[y, x] = Instantiate(
                       goalPrefab, new Vector3(x, map.GetLength(0) - y, 0), Quaternion.identity);
                }
            }
            debugText += "\n";
        }
        Debug.Log(debugText);
        //配列の実態の作成と初期化
        //  map = new int[] { 0, 0, 0, 1, 0, 2, 0, 0, 0 };
        //PrintArray();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Vector2Int playerIndex = GetPlayerIndex();
            MoveNumber(playerIndex,playerIndex + new Vector2Int(1,0));
           // MoveNumber(playerIndex, playerIndex + new Vector2Int(1,0));
            // PrintArray();
            if(IsCleard())
            {
                clearText.SetActive(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Vector2Int playerIndex = GetPlayerIndex();
            MoveNumber(playerIndex, playerIndex + new Vector2Int(-1, 0));
            //MoveNumber(playerIndex,playerIndex - new Vector2Int(1,0));
            // PrintArray();
            if (IsCleard())
            {
                clearText.SetActive(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Vector2Int playerIndex = GetPlayerIndex();
            MoveNumber(playerIndex, playerIndex + new Vector2Int(0, -1));
            // MoveNumber(playerIndex, playerIndex + new Vector2Int(0, 1));
            // PrintArray();
            if (IsCleard())
            {
                clearText.SetActive(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Vector2Int playerIndex = GetPlayerIndex();
            MoveNumber(playerIndex, playerIndex + new Vector2Int(0, 1));
            // MoveNumber(playerIndex, playerIndex - new Vector2Int(0, 1));
            // PrintArray();
            if (IsCleard())
            {
                clearText.SetActive(true);
            }
        }

        //もしクリアしていたら
        if (IsCleard())
        {
            Debug.Log("Clear");
        }
      
    }

    private Vector2Int GetPlayerIndex()
    {
        for(int y = 0; y < field.GetLength(0); y++)
        {
            for(int x = 0; x < field.GetLength(1); x++)
            {
                if (field[y,x] == null) { continue; }
                if (field[y,x].tag == "Player")
                {
                    return new Vector2Int(x, y);
                }
            }
        }
        return new Vector2Int(-1, -1);
    }
    
  bool MoveNumber(Vector2Int moveFrom,Vector2Int moveTo)
  {
      //移動可能か判断
      if(moveTo.y < 0||moveTo.y >= field.GetLength(0)) { return false; }
      if(moveTo.x < 0||moveTo.x >= field.GetLength(1)) { return false; }

        if (field[moveTo.y, moveTo.x] != null && field[moveTo.y, moveTo.x].tag == "Box")
        {
            Vector2Int velocity = moveTo - moveFrom;
            bool success = MoveNumber(moveTo, moveTo + velocity);
            if (!success) { return false; }
        }

      //プレイヤー・箱変わらずの移動処理
       field[moveTo.y,moveTo.x] = field[moveFrom.y,moveFrom.x];
        Vector3 moveToPosition = new Vector3(
          moveTo.x, map.GetLength(0) - moveTo.y, 0);
        field[moveFrom.y, moveFrom.x].GetComponent<Move>().MoveTo(moveToPosition);
        field[moveFrom.y, moveFrom.x] = null;
        return true;
  }

  bool IsCleard()
    {
        //Vector2Int型の可変長配列の作成
        List<Vector2Int> goals = new List<Vector2Int>();

        for(int y = 0;y<map.GetLength(0);y++)
        {
            for(int x = 0; x < map.GetLength(1);x++)
            {
                //格納場所か否かを判断
                if (map[y,x] == 3)
                {
                    //格納場所のインデックスを控えておく
                    goals.Add(new Vector2Int(x, y));
                }
            }
        }
        //要素数はgoals.Countで取得
        for(int i = 0;i<goals.Count;i++)
        {
            GameObject f = field[goals[i].y, goals[i].x];
            if(f == null || f.tag != "Box")
            {
                //1つでも箱がなかったら条件未達成
                return false;
            }
        }
        //条件未達成出なければ条件達成
        return true;
    }
}
