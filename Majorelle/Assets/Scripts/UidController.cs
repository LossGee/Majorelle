using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UidController : MonoBehaviour
{
    // Receive Text from DB Manager
    dbManager DBmanager;

    // uid & 구성 UI와 관련된 변수
    public string uid = "";
    string stateMessage = "";
    string boardState = "todayCafeteriaMenu";                                            // boardState: 공지사항("notice"), 오늘의 식단("todayCafeteriaMenu") 상태 표시
    //string boardState = "notice";
    string boardtextMessage = "";
    List<string> guest_uidList = new List<string>();
    List<string> guestMessage = new List<string>();

    // UI Component
    public Text nameText;
    public InputField StateInputField;

    public Text BoardText;
    public InputField BoardInputField;

    public Text guestNameText;
    public InputField guestMessageInputField;



    void Start()
    {
        DBmanager = GetComponent<dbManager>();

        // StateMessage 초기화
        nameText.text = uid;
        DBmanager.ReadMyState(uid);

        // BoardInputField 초기화
        LoadBoardInputField();

        // GuestText 초기화: Guest Mesasge 중 첫번째 메시지를 등록하기  
        DBmanager.ReadGuestList(uid);
    }


    private void Update()
    {
        // DBmanager로부터 Messag 내용 가져오기 


        // StateTextMessage 반영: DBmanager로부터 string값 받아오기 & InputFeild에 반영
        if (DBmanager.isReceiveMyStateMessage)
        {
            stateMessage = DBmanager.myStateMessage;
            DBmanager.CompleteReceiveMyStateMessage();
            StateInputField.text = stateMessage;
        }

        // BoardText: DBmanager로부터 string값 받아오기 & InputFeild에 반영
        SetBoardText();                                                                         // BoadText(제목) 반영[공지사항, 오늘의 메뉴]
        if (DBmanager.isReceiveNotice || DBmanager.isReceiveMenu)
        {
            if (boardState == "notice")
            {
                boardtextMessage = DBmanager.notice;
                DBmanager.CompleteReceiveNotice();
            }
            else if (boardState == "todayCafeteriaMenu")
            {
                boardtextMessage = DBmanager.todayCafeteriaMenu;
                DBmanager.CompleteReceiveMenu();
            }
            BoardInputField.text = boardtextMessage;
        }

        // GuestText 반영: DBmanager로부터 string값 받아오기 & InputField에 반영
        if (DBmanager.isReceiveGuestList)
        {
            print("22222222222222");
            // DBmanager로부터 string값 받아오기
            guest_uidList = DBmanager.guestList;
            guestMessage = DBmanager.guestMessage;

            // InputField에 반영
            guestNameText.text = guest_uidList[0];
            guestMessageInputField.text = guestMessage[0];

            DBmanager.CompleteReceiveGuestMessage();
        }
        print("isReceiveGuestList:" + DBmanager.isReceiveGuestList);


        //------------------------------------------------------------------------

        //// Apply State Message 
        //stateMessage = DBmanager.myStateMessage;
        //StateInputField.text = stateMessage;

        //// Apply Board Text Message 
        //if (boardState == "notice" && DBmanager.isReceiveNotice)
        //{
        //    boardtextMessage = DBmanager.notice;
        //}
        //else if (boardState == "todayCafeteriaMenu" && DBmanager.isReceiveMenu)
        //{
        //    boardtextMessage = DBmanager.todayCafeteriaMenu;
        //}
        //BoardInputField.text = boardtextMessage;

        //// Apply Guest Text Message
        //guest_uidList = DBmanager.guestList;
        //guestNameText.text = guest_uidList[0];

        //guestMessage = DBmanager.guestMessage;
        //guestMessageInputField.text = guestMessage[0];

    }

    public void SetBoardText()
    {
        if (boardState == "notice")
        {
            BoardText.text = "공지사항";
        }
        else if (boardState == "todayCafeteriaMenu")
        {
            BoardText.text = "오늘의 메뉴";
        }
    }
    public void LoadBoardInputField()
    {
        if (boardState == "notice")
        {
            DBmanager.ReadNotice();

        }
        else if (boardState == "todayCafeteriaMenu")
        {
            DBmanager.ReadTodayCafeteriaMenu();
        }
    }

    // Button Click Control Function
    public void OnClickStateUpdateButton()
    {

    }
    public void OnClickStateRefreshButton()
    {

    }
    public void OnClickBoardUpdateButtone()
    {

    }
    public void OnClickBoardRefreshButtone()
    {

    }
    public void OnClickGeustMessageUpdateButtone()
    {

    }
    public void OnClickGeustMessageRefreshButtone()
    {

    }
}
