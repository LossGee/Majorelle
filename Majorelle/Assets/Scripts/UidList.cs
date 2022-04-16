using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UidList : MonoBehaviour
{
    public static UidList Instance;
    private void Awake()
    {
        Instance = this;
    }

    public string[] uidList = new string[]
    {
        "�̿���(�����)",
        "�ڿ���(�Ŵ�����)",
        "�赵��",
        "����",
        "�輼��",
        "������",
        "���¿�",
        "������",
        "�ڼ�ȯ",
        "�ڼ���",
        "�ڿ���",
        "���籤",
        "����ȣ",
        "�۽�ȯ",
        "�ۻ��",
        "�ۼ���",
        "������",
        "���¹�",
        "������",
        "������",
        "�����",
        "������",
        "���Ͽ�",
        "���ڿ�"
    };
    public

    void Start()
    {
        //print(uid.Length);
    }
}


public class StateMessage
{
    public string stateMessage;

    public StateMessage(string stateMessage)
    {
        this.stateMessage = stateMessage;
    }

}

public class GuestMessage
{
    public string message;

    public GuestMessage(string message)
    {
        this.message = message;
    }
}

public class Board
{
    public string todayCafeteriaMenu;
    public string notice;

    public Board(string menu, string notice)
    {
        this.todayCafeteriaMenu = menu;
        this.notice = notice;
    }
}
public class Notice
{
    public string notice;

    public Notice(string notice)
    {
        this.notice = notice;
    }
}
public class Menu
{
    public string menu;

    public Menu(string menu)
    {
        this.menu = menu;
    }
}