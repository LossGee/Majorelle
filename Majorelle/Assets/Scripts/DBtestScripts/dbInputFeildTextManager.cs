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
    public InputField inputFieldText;                                                             // �Է� �ؽ�Ʈ ������ Text ��������


    // Start is called before the first frame update
    void Start()
    {
        FirebaseApp.DefaultInstance.Options.DatabaseUrl = new Uri(DBurl);
        reference = FirebaseDatabase.DefaultInstance.RootReference;

        WriteDB(0, "�ȳ��ϼ���!");
        ReadDB();                                                                       // DB �о����
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
                    DataSnapshot snapshot = task.Result;                              // snapshot���� ���� ������ �� �ش� ��ġ�� ��� �����͸� �����ϴ� �������� ���Ե�

                    IDictionary Message = (IDictionary)snapshot.Value;               // Message �������� snapshot�� ���� ��ȯ
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