using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System;


public class dbManager : MonoBehaviour
{
    public static dbManager Instance;
    private void Awake()
    {
        Instance = this;
        FirebaseApp.DefaultInstance.Options.DatabaseUrl = new Uri(DBurl);
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    // Firebase variable 
    public string DBurl = "https://majorelle-c1849-default-rtdb.firebaseio.com/";
    DatabaseReference reference;

    // DB control variable
    string myStateMessage = "Now Loading";
    string guestMessage = "Now Loading";
    string notice = "Now Loading";
    string todayCafeteriaMenu = "Now Loading";

    bool isReceiveMyStateMessage = false;
    bool isReceiveGuestMessage = false;
    bool isReceiveNotice = false;
    bool isReceiveMenu = false;


    void Start()
    {
        // <Initialize>
        //InitializeDB();                                                               // �ʱ�ȭ �Լ�

        // <Read Test>
        //ReadMyState("������");                                                          // test My state message Read form DB
        //ReadGuestMesssage("������", "Majorelle");                                       // test Guest message read form DB
        //ReadNotice();                                                                   // test Notice read from DB
        //ReadTodayCafeteriaMenu();                                                       // test today menu from Db

        // <Write Test>
        //WriteMystateMessage("������", "�ȳ��ϼ���.\n �������Դϴ� :)");
        //WriteGuestMessage("������", "������", "��������");
        //WriteTodayCafeteriaMenu("[������ �����Ĵ� �޴�\n�۹�\n��������\n���غ���");
        //WriteNotice("[������ ��������]\n ���� �ñ�...! ���� �ʿ�...");

        // <Remove Test>
        RemoveGuestMessage("������", "������");
    }

    void Update()
    {
        // Test ��� Ȯ�� 
        if (isReceiveMyStateMessage)
        {
            print("myStateMessage" + myStateMessage);
            CompleteReceiveMyStateMessage();
        }

        if (isReceiveGuestMessage)
        {
            print("guestMessage" + guestMessage);
            CompleteReceiveGuestMessage();
        }

        if (isReceiveNotice)
        {
            print("notice" + notice);
            CompleteReceiveNotice();
        }

        if (isReceiveMenu)
        {
            print("todayCafeteriaMenu" + todayCafeteriaMenu);
            CompleteReceiveMenu();
        }
    }

    // InitializeDB(): FirebaseDB �ʱ�ȭ
    public void InitializeDB()
    {
        // ������ �����Ĵ�޴� & �������� �ʱ�ȭ 
        Board boardDATA = new Board("������ �޴�", "��������");
        string boardjsondata = JsonUtility.ToJson(boardDATA);
        reference.Child("Board").SetRawJsonValueAsync(boardjsondata);

        // StateMessage �ʱ�ȭ
        foreach (string uid in UidList.Instance.uidList)
        {
            //print("StateMessage: " + uid);
            StateMessage DATA = new StateMessage("<���¸޽��� �Է�â>\n������ ����� ��Ű���?");
            string jsondata = JsonUtility.ToJson(DATA);
            reference.Child(uid).SetRawJsonValueAsync(jsondata);
        }

        // Guest Message �ʱ�ȭ
        foreach (string uid in UidList.Instance.uidList)
        {
            //print("GuestMessage: " + uid);
            GuestMessage DATA = new GuestMessage("���õ� �ູ�� �Ϸ�ǽñ� �ٶ��ϴ� :)");
            string jsondata = JsonUtility.ToJson(DATA);
            reference.Child(uid).Child("guestMessage").Child("Majorelle").SetRawJsonValueAsync(jsondata);
        }
    }

    // [Read]
    // ReadMyState(): DB���� ���¸޽��� �о����
    public void ReadMyState(string uid)
    {
        myStateMessage = "Now Loading...";

        FirebaseDatabase.DefaultInstance                                            // Async�� Network�� ��û�� ������ ������ ������ ������ �ִ� �Լ���� �����ϸ� ��
            .GetReference(uid)
            .GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.Log("Read My State Fail");
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;

                    // ���⿡�� StateMessage�� �������� ��.
                    foreach (var childSnapshot in snapshot.Children)
                    {
                        if (childSnapshot.Key == "stateMessage")
                        {
                            myStateMessage = childSnapshot.Value.ToString();
                            if (null != myStateMessage)
                            {
                                isReceiveMyStateMessage = true;
                            }
                        }
                    }
                }
            });
    }


    // ReadGuestMesssage(): Geust Message �о���� 
    public void ReadGuestMesssage(string uid, string guest_uid)
    {
        FirebaseDatabase.DefaultInstance
           .GetReference(uid).Child("guestMessage").Child(guest_uid)
           .GetValueAsync().ContinueWithOnMainThread(task =>
           {
               if (task.IsFaulted)
               {
                   Debug.Log("Read My State Fail");
               }
               else if (task.IsCompleted)
               {
                   DataSnapshot snapshot = task.Result;

                   // ���⿡�� StateMessage�� �������� ��.
                   foreach (var childSnapshot in snapshot.Children)
                   {
                       if (childSnapshot.Key == "message")
                       {
                           //print(childSnapshot.Child("Medici").Child("message").Value);
                           guestMessage = childSnapshot.Value.ToString();
                           if (null != guestMessage)
                           {
                               isReceiveGuestMessage = true;
                           }
                       }
                   }
               }
           });
    }


    // ReadNotice(): ���� ���� �о���� 
    public void ReadNotice()
    {
        FirebaseDatabase.DefaultInstance
           .GetReference("Board")
           .GetValueAsync().ContinueWithOnMainThread(task =>
           {
               if (task.IsFaulted)
               {
                   Debug.Log("Read My State Fail");
               }
               else if (task.IsCompleted)
               {
                   DataSnapshot snapshot = task.Result;

                   // ���⿡�� StateMessage�� �������� ��.
                   foreach (var childSnapshot in snapshot.Children)
                   {
                       if (childSnapshot.Key == "notice")
                       {
                           //print(childSnapshot.Key + " : " + childSnapshot.Value);
                           notice = childSnapshot.Value.ToString();
                           if (null != notice)
                           {
                               isReceiveNotice = true;
                           }
                       }
                   }
               }
           });
    }

    // ReadTodayCafeteriaMenu(): ������ �����Ĵ� �޴� �о����
    public void ReadTodayCafeteriaMenu()
    {
        FirebaseDatabase.DefaultInstance
           .GetReference("Board")
           .GetValueAsync().ContinueWithOnMainThread(task =>
           {
               if (task.IsFaulted)
               {
                   Debug.Log("Read My State Fail");
               }
               else if (task.IsCompleted)
               {
                   DataSnapshot snapshot = task.Result;

                   // ���⿡�� StateMessage�� �������� ��.
                   foreach (var childSnapshot in snapshot.Children)
                   {
                       if (childSnapshot.Key == "todayCafeteriaMenu")
                       {
                           //print(childSnapshot.Child("Medici").Child("message").Value);
                           todayCafeteriaMenu = childSnapshot.Value.ToString();
                           if (null != todayCafeteriaMenu)
                           {
                               isReceiveMenu = true;
                           }
                       }
                   }
               }
           });
    }


    // [Write]
    // WriteMyStateMessage(): �� ���¸޽����� DB�� ����
    public void WriteMystateMessage(string uid, string stateMessage)
    {
        Dictionary<string, object> dic = new Dictionary<string, object>
        {
            {"stateMessage", stateMessage }
        };

        reference.Child(uid).UpdateChildrenAsync(dic);
    }

    // WriteGuestMessage(): �湮�ڰ� �ۼ��� �޽����� DB�� ����
    public void WriteGuestMessage(string uid, string guest_uid, string guestMessage)
    {
        GuestMessage DATA = new GuestMessage(guestMessage);
        string jsondata = JsonUtility.ToJson(DATA);
        reference.Child(uid).Child("guestMessage").Child(guest_uid).SetRawJsonValueAsync(jsondata);
    }

    // WriteTodayCafeteriaMenu(): ���� �����Ĵ� �޴��� DB�� ����
    public void WriteTodayCafeteriaMenu(string menu)
    {
        Dictionary<string, object> dic = new Dictionary<string, object>
        {
            {"todayCafeteriaMenu", menu}
        };

        reference.Child("Board").UpdateChildrenAsync(dic);
    }

    // WriteNotice(): �������� ������ DB�� ����
    public void WriteNotice(string notice)
    {
        Dictionary<string, object> dic = new Dictionary<string, object>
        {
            {"notice", notice}
        };

        reference.Child("Board").UpdateChildrenAsync(dic);
    }


    // [Remove]
    // RemoveGuestMessage(): ������ 
    public void RemoveGuestMessage(string uid, string guest_uid)
    {
        reference.Child(uid).Child("guestMessage").Child(guest_uid).RemoveValueAsync();
    }


    // [Check Receive Message]
    public void CompleteReceiveMyStateMessage()
    {
        isReceiveMyStateMessage = false;
    }
    public void CompleteReceiveGuestMessage()
    {
        isReceiveGuestMessage = false;
    }
    public void CompleteReceiveNotice()
    {
        isReceiveNotice = false;
    }
    public void CompleteReceiveMenu()
    {
        isReceiveMenu = false;
    }

}
