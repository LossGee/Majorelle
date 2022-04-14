using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

// 메인 카메라위치에서 카메라의 앞방향으로 Ray를 만들고
// 부딪힌 것이 있다면 그곳에 인디케이터를 배치하고싶다.
// Ray로 바라볼때 Indicator 레이어를 제외하고싶다.
// 화면을 터치/클릭 했을때 인디케이터라면 그 위치에 치킨을 배치하고싶다.
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
        // 1. 마우스 왼쪽 버튼을 누르는 순간
        if (Input.GetButtonDown("Fire1"))
        {
            // 2. 클릭한 위치를 기준으로 Ray를 만들고
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // 3. 바로보고 부딪힌것이 인디케이터라면
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Indicator"))
                {
                    // 4. 그 위치에 치킨을 생성해서 배치하고싶다.
                    GameObject chicken = Instantiate(chikenFactory);
                    chicken.transform.position = hitInfo.point;
                    chicken.transform.up = hitInfo.normal;
                }
            }
        }
    }
    void UpdateIndicator()
    {
        // 메인 카메라위치에서 카메라의 앞방향으로 Ray를 만들고
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        // 부딪힌 것이 있다면 그곳에 인디케이터를 배치하고싶다.
        RaycastHit hitInfo;
        // Ray로 바라볼때 Indicator 레이어를 제외하고싶다.
        int layerMask = ~(1 << LayerMask.NameToLayer("Indicator"));
        if (Physics.Raycast(ray, out hitInfo, float.MaxValue, layerMask))
        {
            indicator.transform.position = hitInfo.point + hitInfo.normal * 0.001f;
        }
    }
    void UpdateMakeChikenForAndroid()
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
                // 3. 바로보고 부딪힌것이 인디케이터라면
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo))
                {
                    if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Indicator"))
                    {
                        // 4. 그 위치에 치킨을 생성해서 배치하고싶다.
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
        // aRRaycastManager를 이용해서 Raycast를 하고 
        Vector2 center = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        List<ARRaycastHit> hitResults = new List<ARRaycastHit>();
        if (aRRaycastManager.Raycast(center, hitResults))
        {
            // 부딪힌 것이 있다면 그곳에 인디케이터를 배치하고싶다.
            indicator.SetActive(true);
            indicator.transform.position = hitResults[0].pose.position;
        }
        else
        {
            indicator.SetActive(false);
        }
    }
}
