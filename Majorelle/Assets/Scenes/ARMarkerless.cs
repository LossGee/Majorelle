using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

// ���� ī�޶���ġ���� ī�޶��� �չ������� Ray�� �����
// �ε��� ���� �ִٸ� �װ��� �ε������͸� ��ġ�ϰ�ʹ�.
// Ray�� �ٶ󺼶� Indicator ���̾ �����ϰ�ʹ�.
// ȭ���� ��ġ/Ŭ�� ������ �ε������Ͷ�� �� ��ġ�� ġŲ�� ��ġ�ϰ�ʹ�.
public class ARMarkerless : MonoBehaviour
{
    public GameObject indicator;
    public GameObject chikenFactory;
    ARRaycastManager aRRaycastManager;
    public GameObject floor;
    // Start is called before the first frame update
    void Start()
    {
#if UNITY_EDITOR
        floor.SetActive(true);
#else
        floor.SetActive(false);
#endif
        aRRaycastManager = GetComponent<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        UpdateIndicator();
        UpdateMakeChiken();
#else
        UpdateIndicatorForAndroid();
        UpdateMakeChikenForAndroid();
#endif
    }
    void UpdateMakeChiken()
    {
        // 1. ���콺 ���� ��ư�� ������ ����
        if (Input.GetButtonDown("Fire1"))
        {
            // 2. Ŭ���� ��ġ�� �������� Ray�� �����
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // 3. �ٷκ��� �ε������� �ε������Ͷ��
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Indicator"))
                {
                    // 4. �� ��ġ�� ġŲ�� �����ؼ� ��ġ�ϰ�ʹ�.
                    GameObject chicken = Instantiate(chikenFactory);
                    chicken.transform.position = hitInfo.point;
                    chicken.transform.up = hitInfo.normal;
                }
            }
        }
    }
    void UpdateIndicator()
    {
        // ���� ī�޶���ġ���� ī�޶��� �չ������� Ray�� �����
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        // �ε��� ���� �ִٸ� �װ��� �ε������͸� ��ġ�ϰ�ʹ�.
        RaycastHit hitInfo;
        // Ray�� �ٶ󺼶� Indicator ���̾ �����ϰ�ʹ�.
        int layerMask = ~(1 << LayerMask.NameToLayer("Indicator"));
        if (Physics.Raycast(ray, out hitInfo, float.MaxValue, layerMask))
        {
            indicator.transform.position = hitInfo.point + hitInfo.normal * 0.001f;
        }
    }
    void UpdateMakeChikenForAndroid()
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
                // 3. �ٷκ��� �ε������� �ε������Ͷ��
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo))
                {
                    if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Indicator"))
                    {
                        // 4. �� ��ġ�� ġŲ�� �����ؼ� ��ġ�ϰ�ʹ�.
                        GameObject chicken = Instantiate(chikenFactory);
                        chicken.transform.position = hitInfo.point;
                        chicken.transform.up = hitInfo.normal;
                    }
                }
            }
        }
    }
    void UpdateIndicatorForAndroid()
    {
        // aRRaycastManager�� �̿��ؼ� Raycast�� �ϰ� 
        Vector2 center = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        List<ARRaycastHit> hitResults = new List<ARRaycastHit>();
        if (aRRaycastManager.Raycast(center, hitResults))
        {
            // �ε��� ���� �ִٸ� �װ��� �ε������͸� ��ġ�ϰ�ʹ�.
            indicator.SetActive(true);
            indicator.transform.position = hitResults[0].pose.position;
        }
        else
        {
            indicator.SetActive(false);
        }
    }
}
