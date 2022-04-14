using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;                                                         // .ContinueWithOnMainThread()를 불러오기 위한 namespace(ReadDB에서 사용)
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
        reference = FirebaseDatabase.DefaultInstance.RootReference;                 // refernece: 데이터 저장, 읽어오기에서 공통적으로 필요한 참조를 위한 변수 
        UID = uid.ToString();                                                       // UID: uid를 json의 key로 사용하기 위해 string으로 변환한 변수
        WriteDB(UID);                                                                   
        ReadDB();
    }

    // WriteDB: Firebase DB에 데이터 저장하기 
    public void WriteDB(string UID)
    {
        // step1) DATA 생성하기
        Flower DATA1 = new Flower(1, "오늘 하루도 화이팅!");
        Flower DATA2 = new Flower(2, "강사님... 진도가 너무 빠릅니다...! 쿨럭..!");

        // step2) DATA를 json 형태로 변환하기 
        string jsondata1 = JsonUtility.ToJson(DATA1);                               // JsonUtility.ToJson(변환전 데이터)
        string jsondata2 = JsonUtility.ToJson(DATA2);

        // step3) FirebaseDB에 json형태의 DATA 저장 
        reference.Child(UID).SetRawJsonValueAsync(jsondata1);                       // DB 저장 명령 형식: reference.Child(최상위루트).Child(하위루트1).SetRawJsonValueAsysnc(JSON데이터)
        reference.Child(UID).SetRawJsonValueAsync(jsondata2);                       // cf) 하위 루트 로 들어가려면 .Child(하위루트 이름)으로 계속 타고 들어갈 수 있다.(

        // [Delete DB DATA] FirebaseDB에 특정 DATA 삭제하기 
        //reference.Child("Korea").SetRawJsonValueAsync(null);
        reference.Child("Korea").RemoveValueAsync();
    }

    // Firebase DB에서 데이터 읽어오기
    public void ReadDB()
    {
        FirebaseDatabase.DefaultInstance                                             // Firebase 공식문서 '데이터 한번 읽기'참고(https://firebase.google.com/docs/database/unity/retrieve-data?hl=ko#read_data_once)
            .GetReference(UID)                                                       // .GetReference(최상위루트)   if) 하위 경로까지 들어가고 싶다면 ".Child(하위루트)를 뒤에
            .GetValueAsync().ContinueWithOnMainThread(task =>                        // .GetValueAsync(): 지정된 경로에서 콘텐츠의 정적 스냅샷을 읽을 수 있도록 하는 명령
            {
                if (task.IsFaulted)
                {
                    print("ReadDB Erorr");
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;                              // snapshot에는 하위 데이터 등 해당 위치의 모든 데이터를 포함하는 스냅샷이 포함됨

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