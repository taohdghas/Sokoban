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
    //�z��̐錾
    int[,] map;
    GameObject[,] field;
    // Start is called before the first frame update
    void Start()
    {
        //�}�b�v�̐���
        map = new int[,]
   {
        {1,0,0,0,0 },
        {0,0,0,0,0 },
        {0,0,0,0,0 },
    };
        //�t�B�[���h�T�C�Y����
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
                     playerPrefab, new Vector3(x, map.GetLength(0) -1 - y, 0), Quaternion.identity);
                }
            }
            debugText += "\n";
        }
        Debug.Log(debugText);
        //�z��̎��Ԃ̍쐬�Ə�����
        //  map = new int[] { 0, 0, 0, 1, 0, 2, 0, 0, 0 };
        //PrintArray();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            /*
            int playerIndex = GetPlayerIndex();
            MoveNumber(1, playerIndex, playerIndex + 1);
            PrintArray();
            */
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            /*
            int playerIndex = GetPlayerIndex();
            MoveNumber(1,playerIndex, playerIndex - 1);
            PrintArray();
            */
        }
        /*
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
    /*
  void PrintArray()
  {
      string debugText = "";
      for (int i = 0; i < map.Length; i++)
      {
          debugText += map[i].ToString() + ",";
      }
      Debug.Log(debugText);
  }
    */
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
    
  bool MoveNumber(string objName,Vector2Int moveFrom,Vector2Int moveTo)
  {
      //�ړ��\�����f
      if(moveTo.x < 0||moveTo.x >= field.Length) { return false; }
      if(moveTo.y < 0||moveTo.y >= field.Length) { return false; }  
      //�ړ���ɔ��������
      if (field[moveTo.] == 2)
      {
          //�ǂ̕����ֈړ����邩
          int velocity = moveTo - moveFrom;
          //�v���C���[�̈ړ��悩�炳��ɐ��2(��)���ړ�������
          //���̈ړ������Bmovenumber���]�b�g����movenumber���]�b�g��
          //�ړ��s��bool�ŋL�^
          bool success = MoveNumber(2, moveTo, moveTo + velocity);
          //���̈ړ������s������player���ړ��ł��Ȃ�
          if (!success) { return false; }
      }
      //�v���C���[�E���ς�炸�̈ړ�����
      field[moveTo.y,moveTo.x] = field[moveFrom.y,moveFrom.x];
      field[moveFrom.y, moveFrom.x] = null;

      return true;
  }
  
}
