using System;
using UnityEngine;

public class Utils
{
    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0;
        return vec;
    }

    public static Vector3 GetMouseWorldPositionWithZ()
    {
        if (!Camera.main)
        {
            Debug.LogError("No camera found");
            return Vector3.zero;
        }
        Vector3 worldPosition = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        return worldPosition;
    }

    public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera)
    {
        if (!worldCamera)
        {
            Debug.LogError("No camera found");
            return Vector3.zero;
        }
        Vector3 worldPosition = GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
        return worldPosition;
    }
    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        if (!worldCamera)
        {
            Debug.LogError("No camera found");
            return Vector3.zero;
        }
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
}