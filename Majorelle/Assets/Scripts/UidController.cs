using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UidController : MonoBehaviour
{
    public string uid = "";
    public int boardState = 0;                                      // boardState(게시판 상태): 0=공지사항, 1=오늘의 식단
    
    public Text nameText;
    public InputField StateInputFiled;

    public Text BoardText;
    public InputField BoardInputFiled;

    public Text geustNameText;
    public InputField GuestMessageInputField;

    void Start()
    {
        nameText.text = uid;
    }

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
