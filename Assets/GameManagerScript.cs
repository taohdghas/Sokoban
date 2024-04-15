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
        //�ړ��\�����f
        if(moveTo < 0||moveTo >= map.Length) { return false; }
        //�ړ���ɔ��������
        if (map[moveTo] == 2)
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
        map[moveTo] = number;
        map[moveFrom] = 0;
        return true;
    }
    //�z��̐錾
    int[] map;
    // Start is called before the first frame update
    void Start()
    {
        //�z��̎��Ԃ̍쐬�Ə�����
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
