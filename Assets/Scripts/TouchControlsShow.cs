using System.Collections;
using UnityEngine;

public class TouchControlsShow : MonoBehaviour
{
    void Start()
    {

        gameObject.SetActive(SystemInfo.deviceType == DeviceType.Handheld);

    }
}
