using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;                                                         // .ContinueWithOnMainThread()�� �ҷ����� ���� namespace(ReadDB���� ���)
using System;

public class dbTextManager : MonoBehaviour
{
    public string DBurl = "https://fir-test-33256-default-rtdb.firebaseio.com/";
    DatabaseReference reference;
    public int uid = 0;
    string UID = "";


    // Start is called before the first frame update
    void Start()
    {
        FirebaseApp.DefaultInstance.Options.DatabaseUrl = new Uri(DBurl);
        reference = FirebaseDatabase.DefaultInstance.RootReference;                 // refernece: ������ ����, �о���⿡�� ���������� �ʿ��� ������ ���� ���� 
        UID = uid.ToString();                                                       // UID: uid�� json�� key�� ����ϱ� ���� string���� ��ȯ�� ����
        WriteDB(UID);                                                                   
        ReadDB();
    }

    // WriteDB: Firebase DB�� ������ �����ϱ� 
    public void WriteDB(string UID)
    {
        // step1) DATA �����ϱ�
        Flower DATA1 = new Flower(1, "���� �Ϸ絵 ȭ����!");
        Flower DATA2 = new Flower(2, "�����... ������ �ʹ� �����ϴ�...! ��..!");

        // step2) DATA�� json ���·� ��ȯ�ϱ� 
        string jsondata1 = JsonUtility.ToJson(DATA1);                               // JsonUtility.ToJson(��ȯ�� ������)
        string jsondata2 = JsonUtility.ToJson(DATA2);

        // step3) FirebaseDB�� json������ DATA ���� 
        reference.Child(UID).SetRawJsonValueAsync(jsondata1);                       // DB ���� ��� ����: reference.Child(�ֻ�����Ʈ).Child(������Ʈ1).SetRawJsonValueAsysnc(JSON������)
        reference.Child(UID).SetRawJsonValueAsync(jsondata2);                       // cf) ���� ��Ʈ �� ������ .Child(������Ʈ �̸�)���� ��� Ÿ�� �� �� �ִ�.(

        // [Delete DB DATA] FirebaseDB�� Ư�� DATA �����ϱ� 
        //reference.Child("Korea").SetRawJsonValueAsync(null);
        reference.Child("Korea").RemoveValueAsync();
    }

    // Firebase DB���� ������ �о����
    public void ReadDB()
    {
        FirebaseDatabase.DefaultInstance                                             // Firebase ���Ĺ��� '������ �ѹ� �б�'����(https://firebase.google.com/docs/database/unity/retrieve-data?hl=ko#read_data_once)
            .GetReference(UID)                                                       // .GetReference(�ֻ�����Ʈ)   if) ���� ��α��� ���� �ʹٸ� ".Child(������Ʈ)�� �ڿ�
            .GetValueAsync().ContinueWithOnMainThread(task =>                        // .GetValueAsync(): ������ ��ο��� �������� ���� �������� ���� �� �ֵ��� �ϴ� ���
            {
                if (task.IsFaulted)
                {
                    print("ReadDB Erorr");
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;                              // snapshot���� ���� ������ �� �ش� ��ġ�� ��� �����͸� �����ϴ� �������� ���Ե�

                    print(snapshot);
                }
            });
    }

}

public class Flower
{
    public int flowerModelNumber = 0;
    public string textMassage = "";

    public Flower(int FlowerNum, string Text)
    {
        flowerModelNumber = FlowerNum;
        textMassage = Text;
    }
}