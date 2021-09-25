using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

public class CameraResolutionGUI : MonoBehaviour
{
    public Camera mainCamera;

    private void Start()
    {
        AdjustScalingFactor();
    }

    private void LateUpdate()
    {
        AdjustScalingFactor();
    }

    void AdjustScalingFactor()
    {
        GetComponent<CanvasScaler>().scaleFactor = mainCamera.GetComponent<PixelPerfectCamera>().pixelRatio;
    }

}
