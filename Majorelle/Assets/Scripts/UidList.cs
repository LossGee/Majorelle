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
        "이영훈(강사님)",
        "박연희(매니저님)",
        "김도영",
        "김상옥",
        "김세현",
        "김현석",
        "남승원",
        "문종국",
        "박성환",
        "박세련",
        "박우진",
        "박재광",
        "서영호",
        "송승환",
        "송상운",
        "송수영",
        "오수진",
        "유승민",
        "윤성하",
        "이지은",
        "임재원",
        "정세진",
        "조하영",
        "최자원"
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