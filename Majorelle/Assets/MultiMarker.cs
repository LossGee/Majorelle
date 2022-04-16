using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

// ARTrackedImageManager ���� ��Ŀ�� �����Ǿ����� ������ �ް�ʹ�.
// ������ ��Ŀ�� ���� �˰��ִ� ��Ͽ� �ִ³༮�̶�� 
// �� ��Ŀ�� �ش��ϴ� ������Ʈ�� ����ġ�� ��ġ�ϰ�ʹ�.
public class MultiMarker : MonoBehaviour
{
    ARTrackedImageManager aRTrackedImageManager;
    void Awake()
    {
        // ARTrackedImageManager ���� ��Ŀ�� �����Ǿ����� ������ �ް�ʹ�.
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
            // ������ ��Ŀ�� ���� �˰��ִ� ��Ͽ� �ִ³༮�̶�� 
            for (int j = 0; j < infos.Length; j++)
            {
                if (marker.referenceImage.name == infos[j].name)
                {
                    if (marker.trackingState == TrackingState.Tracking)
                    {
                        // �� ��Ŀ�� �ش��ϴ� ������Ʈ�� ����ġ�� ��ġ(Ȱ��,��ġ,ȸ��)�ϰ�ʹ�.
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
