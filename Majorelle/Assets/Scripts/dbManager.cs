using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System;

public class dbManager : MonoBehaviour
{
    //public static dbManager Instance;
    private void Awake()
    {
        //Instance = this;
        FirebaseApp.DefaultInstance.Options.DatabaseUrl = new Uri(DBurl);
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    // Firebase variable 
    public string DBurl = "https://majorelle-c1849-default-rtdb.firebaseio.com/";
    DatabaseReference reference;

    // DB control variable
    public string myStateMessage = "Now Loading";
    public string notice = "Now Loading";
    public string todayCafeteriaMenu = "Now Loading";
    public List<string> guestList = new List<string>();
    public List<string> guestMessage = new List<string>();

    // DB receive Check varibles
    public bool isReceiveMyStateMessage = false;
    public bool isReceiveGuestList = false;
    public bool isReceiveGuestMessage = false;
    public bool isReceiveNotice = false;
    public bool isReceiveMenu = false;

    void Start()
    {
        // <Initialize>
        //InitializeDB();                                                                 // 초기화 함수

        // <Read Test>
        //ReadMyState("오수진");                                                          // test My state message Read form DB
        //ReadGuestMesssage("오수진", "Majorelle");                                       // test Guest message read form DB
        //ReadNotice();                                                                   // test Notice read from DB
        //ReadTodayCafeteriaMenu();                                                       // test today menu from Db
        //ReadGuestList("오수진");

        // <Write Test>
        //WriteMystateMessage("오수진", "안녕하세요.\n 오수진입니다 :)");
        //WriteGuestMessage("오수진", "문종국", "해위해위");
        //WriteGuestMessage("오수진", "서영호", "밥먹으로 갈까?");
        //WriteTodayCafeteriaMenu("[오늘의 구내식당 메뉴\n○밥\n○육빔소\n○해빔소");
        //WriteNotice("[오늘의 공지사항]\n 복습 시급...! 휴일 필요...");

        // <Remove Test>
        //RemoveGuestMessage("오수진", "문종국");
    }

    void Update()
    {
        // Test 출력 확인 
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

        if (isReceiveGuestList)
        {
            print("guestList");
            for (int i = 0; i < guestList.Count; i++)
            {
                print("guestName:" + guestList[i] + "guestMessage" + guestMessage[i]);
            }
            print(guestList.Count);
            CompleteReceiveGuestList();
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
        //print(guestLikt.Count);
    }

    // InitializeDB(): FirebaseDB 초기화
    public void InitializeDB()
    {
        // 오늘의 구내식당메뉴 & 공지사항 초기화 
        Board boardDATA = new Board("오늘의 메뉴", "공지사항");
        string boardjsondata = JsonUtility.ToJson(boardDATA);
        reference.Child("Board").SetRawJsonValueAsync(boardjsondata);

        // StateMessage 초기화
        foreach (string uid in UidList.Instance.uidList)
        {
            //print("StateMessage: " + uid);
            StateMessage DATA = new StateMessage("<상태메시지 입력창>\n오늘의 기분은 어떠신가요?");
            string jsondata = JsonUtility.ToJson(DATA);
            reference.Child(uid).SetRawJsonValueAsync(jsondata);
        }

        // Guest Message 초기화
        foreach (string uid in UidList.Instance.uidList)
        {
            //print("GuestMessage: " + uid);
            GuestMessage DATA = new GuestMessage("오늘도 행복한 하루되시길 바랍니다 :)");
            string jsondata = JsonUtility.ToJson(DATA);
            reference.Child(uid).Child("guestMessage").Child("Majorelle").SetRawJsonValueAsync(jsondata);
        }
    }

    // [Read]
    // ReadMyState(): DB에서 상태메시지 읽어오기
    public void ReadMyState(string uid)
    {

        FirebaseDatabase.DefaultInstance                                                    // Async는 Network에 요청한 내용이 도착할 때까지 숨참고 있는 함수라고 생각하면 됨
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

                    foreach (var childSnapshot in snapshot.Children)
                    {
                        if (childSnapshot.Key == "stateMessage")
                        {
                            myStateMessage = childSnapshot.Value.ToString();
                        }
                    }
                    isReceiveMyStateMessage = true;
                }
            });
    }

    // ReadGuestList(): 방명록을 작성한 Guest들의 목록 가져오기
    public void ReadGuestList(string uid)
    {
        FirebaseDatabase.DefaultInstance
           .GetReference(uid).Child("guestMessage")
           .GetValueAsync().ContinueWithOnMainThread(task =>
           {
               if (task.IsFaulted)
               {
                   Debug.Log("Read My State Fail");
               }
               else if (task.IsCompleted)
               {
                   DataSnapshot snapshot = task.Result;

                   foreach (var childSnapshot in snapshot.Children)
                   {
                       string guest = childSnapshot.Key;
                       guestList.Add(guest);
                   }

                   foreach (var guest in guestList)
                   {
                       string message = snapshot.Child(guest).Child("message").Value.ToString();
                       guestMessage.Add(message);
                   }

               }
               isReceiveGuestList = true;
           });
    }

    // ReadGuestMesssage(): Geust Message 읽어오기 
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

                   foreach (var childSnapshot in snapshot.Children)
                   {
                       if (childSnapshot.Key == "message")
                       {
                           //guestMessage = childSnapshot.Value.ToString();
                       }
                   }
                   isReceiveGuestMessage = true;
               }
           });
    }


    // ReadNotice(): 공지 내용 읽어오기 
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

                   // 여기에서 StateMessage만 가져오면 됨.
                   foreach (var childSnapshot in snapshot.Children)
                   {
                       if (childSnapshot.Key == "notice")
                       {
                           notice = childSnapshot.Value.ToString();
                       }
                   }
                   isReceiveNotice = true;
               }
           });
    }

    // ReadTodayCafeteriaMenu(): 오늘이 구내식당 메뉴 읽어오기
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

                   // 여기에서 StateMessage만 가져오면 됨.
                   foreach (var childSnapshot in snapshot.Children)
                   {
                       if (childSnapshot.Key == "todayCafeteriaMenu")
                       {
                           //print(childSnapshot.Child("Medici").Child("message").Value);
                           todayCafeteriaMenu = childSnapshot.Value.ToString();
                       }
                   }
                   isReceiveMenu = true;
               }
           });
    }


    // [Write]
    // WriteMyStateMessage(): 내 상태메시지를 DB에 저장
    public void WriteMystateMessage(string uid, string stateMessage)
    {
        Dictionary<string, object> dic = new Dictionary<string, object>
        {
            {"stateMessage", stateMessage }
        };

        reference.Child(uid).UpdateChildrenAsync(dic);
    }

    // WriteGuestMessage(): 방문자가 작성한 메시지를 DB에 저장
    public void WriteGuestMessage(string uid, string guest_uid, string guestMessage)
    {
        GuestMessage DATA = new GuestMessage(guestMessage);
        string jsondata = JsonUtility.ToJson(DATA);
        reference.Child(uid).Child("guestMessage").Child(guest_uid).SetRawJsonValueAsync(jsondata);
    }

    // WriteTodayCafeteriaMenu(): 오늘 구내식당 메뉴를 DB에 저장
    public void WriteTodayCafeteriaMenu(string menu)
    {
        Dictionary<string, object> dic = new Dictionary<string, object>
        {
            {"todayCafeteriaMenu", menu}
        };

        reference.Child("Board").UpdateChildrenAsync(dic);
    }

    // WriteNotice(): 공지사항 내용을 DB에 저장
    public void WriteNotice(string notice)
    {
        Dictionary<string, object> dic = new Dictionary<string, object>
        {
            {"notice", notice}
        };

        reference.Child("Board").UpdateChildrenAsync(dic);
    }


    // [Remove]
    // RemoveGuestMessage(): 남겨진 
    public void RemoveGuestMessage(string uid, string guest_uid)
    {
        reference.Child(uid).Child("guestMessage").Child(guest_uid).RemoveValueAsync();
    }


    // [Check Receive Message]
    public void CompleteReceiveMyStateMessage() { isReceiveMyStateMessage = false; }
    public void CompleteReceiveGuestList() { isReceiveGuestList = false; }
    public void CompleteReceiveGuestMessage() { isReceiveGuestMessage = false; }
    public void CompleteReceiveNotice() { isReceiveNotice = false; }
    public void CompleteReceiveMenu() { isReceiveMenu = false; }

}
