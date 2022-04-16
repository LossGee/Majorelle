using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System;
using UnityEngine.UI;

public class dbInputFeildTextManager : MonoBehaviour
{
    public string DBurl = "https://fir-test-33256-default-rtdb.firebaseio.com/";
    DatabaseReference reference;

    public int uid;

    public int textnumber = 0;
    public InputField inputFieldText;                                                             // 입력 텍스트 상자의 Text 가져오기


    // Start is called before the first frame update
    void Start()
    {
        FirebaseApp.DefaultInstance.Options.DatabaseUrl = new Uri(DBurl);
        reference = FirebaseDatabase.DefaultInstance.RootReference;

        WriteDB(0, "안녕하세요!");
        ReadDB();                                                                       // DB 읽어오기
    }

    public void WriteDB(int textNumber, string messageText)
    {
        Message DATA = new Message(textNumber, messageText);
        string jsondata = JsonUtility.ToJson(DATA);
        reference.Child(uid.ToString()).SetRawJsonValueAsync(jsondata);
    }

    public void ReadDB()
    {
        FirebaseDatabase.DefaultInstance
            .GetReference(uid.ToString())
            .GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    print("ReadDB Erorr");
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;                              // snapshot에는 하위 데이터 등 해당 위치의 모든 데이터를 포함하는 스냅샷이 포함됨

                    IDictionary Message = (IDictionary)snapshot.Value;               // Message 형식으로 snapshot의 형식 변환
                    //print(Message);
                    //Debug.Log(Message["textNumebr"]);                                      
                    //Debug.Log(Message["message"]);

                    inputFieldText.text = (string)Message["message"];

                }
            }
            );
    }

    public void OnClickSaveButton() { WriteDB(textnumber, inputFieldText.text); }

    public void OnClickDeleteButton() { WriteDB(textnumber, null); }

    public void OnClickRefreshButton() { ReadDB(); }


}

public class Message
{
    public int textNumber;
    public string message;

    public Message(int TextNum, string MSG)
    {
        this.textNumber = TextNum;
        this.message = MSG;
    }

}