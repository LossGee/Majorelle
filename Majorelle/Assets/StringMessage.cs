using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StringMessage : MonoBehaviour
{
    public InputField inputField;
    //
    public IAMFlower iAMFlower;
    public void OnClickButton()
    {
        string s = inputField.text;
        for (int i = 0; i < badWords.Length; i++)
        {
            if (s.Contains(badWords[i]))
            {
                s = s.Replace(badWords[i], "**");
            }
        }
        print(s);
        iAMFlower.description = s;
        iAMFlower = null;
    }
    string[] badWords;
    public TextAsset ta;
    // Start is called before the first frame update
    void Start()
    {
        
        // = Resources.Load<TextAsset>("BadWords");
        badWords = ta.text.Replace("\r", "").Split('\n');
        for (int i = 0; i < badWords.Length; i++)
        {
            print(badWords[i]);
        }
        
    }



}
