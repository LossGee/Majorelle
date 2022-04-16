using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

// ARTrackedImageManager 에게 마커가 추적되었는지 정보를 받고싶다.
// 추적된 마커가 내가 알고있는 목록에 있는녀석이라면 
// 그 마커에 해당하는 오브젝트를 그위치에 배치하고싶다.
public class MultiMarker : MonoBehaviour
{
    ARTrackedImageManager aRTrackedImageManager;
    void Awake()
    {
        // ARTrackedImageManager 에게 마커가 추적되었는지 정보를 받고싶다.
        aRTrackedImageManager = GetComponent<ARTrackedImageManager>();
    }
    private void OnEnable()
    {
        aRTrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }
    private void OnDisable()
    {
        aRTrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    [System.Serializable]
    public class MarkerInfo
    {
        public string name;
        public GameObject obj;
    }
    public MarkerInfo[] infos;
    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs args)
    {
        var list = args.updated;
        for (int i = 0; i < list.Count; i++)
        {
            ARTrackedImage marker = list[i];
            // 추적된 마커가 내가 알고있는 목록에 있는녀석이라면 
            for (int j = 0; j < infos.Length; j++)
            {
                if (marker.referenceImage.name == infos[j].name)
                {
                    if (marker.trackingState == TrackingState.Tracking)
                    {
                        // 그 마커에 해당하는 오브젝트를 그위치에 배치(활성,위치,회전)하고싶다.
                          infos[j].obj.SetActive(true);
                          infos[j].obj.transform.position = marker.transform.position;
                          infos[j].obj.transform.rotation = marker.transform.rotation;
                    }
                    else
                    {
                        infos[j].obj.SetActive(false);
                    }

                }
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
