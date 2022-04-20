using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UidController : MonoBehaviour
{
    // Receive Text from DB Manager
    dbManager DBmanager;

    // uid & ���� UI�� ���õ� ����
    public string uid = "";
    string stateMessage = "";
    string boardState = "todayCafeteriaMenu";                                            // boardState: ��������("notice"), ������ �Ĵ�("todayCafeteriaMenu") ���� ǥ��
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

        // StateMessage �ʱ�ȭ
        nameText.text = uid;
        DBmanager.ReadMyState(uid);

        // BoardInputField �ʱ�ȭ
        LoadBoardInputField();

        // GuestText �ʱ�ȭ: Guest Mesasge �� ù��° �޽����� ����ϱ�  
        DBmanager.ReadGuestList(uid);
    }


    private void Update()
    {
        // DBmanager�κ��� Messag ���� �������� 


        // StateTextMessage �ݿ�: DBmanager�κ��� string�� �޾ƿ��� & InputFeild�� �ݿ�
        if (DBmanager.isReceiveMyStateMessage)
        {
            stateMessage = DBmanager.myStateMessage;
            DBmanager.CompleteReceiveMyStateMessage();
            StateInputField.text = stateMessage;
        }

        // BoardText: DBmanager�κ��� string�� �޾ƿ��� & InputFeild�� �ݿ�
        SetBoardText();                                                                         // BoadText(����) �ݿ�[��������, ������ �޴�]
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

        // GuestText �ݿ�: DBmanager�κ��� string�� �޾ƿ��� & InputField�� �ݿ�
        if (DBmanager.isReceiveGuestList)
        {
            print("22222222222222");
            // DBmanager�κ��� string�� �޾ƿ���
            guest_uidList = DBmanager.guestList;
            guestMessage = DBmanager.guestMessage;

            // InputField�� �ݿ�
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
            BoardText.text = "��������";
        }
        else if (boardState == "todayCafeteriaMenu")
        {
            BoardText.text = "������ �޴�";
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
