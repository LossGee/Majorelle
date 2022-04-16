using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class RaycastTouch : MonoBehaviour
{
    // public GameObject messageUIFactory;
    public GameObject messageUI;
    public InputField inputFieldMessage;
    ARRaycastManager aRRaycastManager;
    // Start is called before the first frame update
    bool isUseMessageUI;
    public StringMessage stringMessage;

    void Start()
    {
        aRRaycastManager = GetComponent<ARRaycastManager>();
        isUseMessageUI = false;
    }


    void Update()
    {
#if UNITY_EDITOR
        UpdateMessageUI();
#else
        UpdateMessageUIForAndroid();
#endif
    }

    void UpdateMessageUI()
    {
        // 마우스 왼쪽 버튼을 누르는 순간
        if (false == isUseMessageUI && Input.GetButtonDown("Fire1"))
        {
            print("1");
            // 2. 클릭한 위치를 기준으로 Ray를 만들고
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // 3. 바로보고 부딪힌것이 꽃이라면
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                print("2");
                if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Flower"))
                {
                    IAMFlower flower = hitInfo.transform.gameObject.GetComponent<IAMFlower>();
                    float delayTime = flower.ShowBloom();
                    stringMessage.iAMFlower = flower;
                    inputFieldMessage.text = flower.description;
                    isUseMessageUI = true;
                    // 4. messageUI를 활성화시키고 싶다.
                    Invoke("ShowMessage", delayTime);
                }
            }
        }
    }

    void ShowMessage()
    {
        messageUI.SetActive(true);
    }


    public void OnClickMessageFinish()
    {
        messageUI.SetActive(false);
        isUseMessageUI = false;
    }




    void UpdateMessageUIForAndroid()
    {
        // 1. 화면을 터치했다면 (1개이상 터치가 일어났다면)
        if (Input.touchCount > 0)
        {
            // 2. 터치가 누르는 순간이라면
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                // 2. 클릭한 위치를 기준으로 Ray를 만들고
                Ray ray = Camera.main.ScreenPointToRay(touch.position);

                // 3. 바로보고 부딪힌것이 꽃이라면
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo))
                {
                    if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Flower"))
                    {
                        IAMFlower ac = hitInfo.transform.gameObject.GetComponent<IAMFlower>();
                        float delayTime = ac.ShowBloom();
                        isUseMessageUI = true;
                        // 4. messageUI를 활성화시키고 싶다.
                        Invoke("ShowMessage", delayTime);
                    }
                }
            }
        }
    }




}

