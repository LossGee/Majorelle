using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UidController : MonoBehaviour
{
    public string uid = "";
    public int boardState = 0;                                      // boardState(�Խ��� ����): 0=��������, 1=������ �Ĵ�
    
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
