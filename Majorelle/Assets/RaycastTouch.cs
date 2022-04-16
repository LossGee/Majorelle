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
        // ���콺 ���� ��ư�� ������ ����
        if (false == isUseMessageUI && Input.GetButtonDown("Fire1"))
        {
            print("1");
            // 2. Ŭ���� ��ġ�� �������� Ray�� �����
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // 3. �ٷκ��� �ε������� ���̶��
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
                    // 4. messageUI�� Ȱ��ȭ��Ű�� �ʹ�.
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
        // 1. ȭ���� ��ġ�ߴٸ� (1���̻� ��ġ�� �Ͼ�ٸ�)
        if (Input.touchCount > 0)
        {
            // 2. ��ġ�� ������ �����̶��
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                // 2. Ŭ���� ��ġ�� �������� Ray�� �����
                Ray ray = Camera.main.ScreenPointToRay(touch.position);

                // 3. �ٷκ��� �ε������� ���̶��
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo))
                {
                    if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Flower"))
                    {
                        IAMFlower ac = hitInfo.transform.gameObject.GetComponent<IAMFlower>();
                        float delayTime = ac.ShowBloom();
                        isUseMessageUI = true;
                        // 4. messageUI�� Ȱ��ȭ��Ű�� �ʹ�.
                        Invoke("ShowMessage", delayTime);
                    }
                }
            }
        }
    }




}

