using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class ObjectPointer : MonoBehaviour
{
    public Image img;
    public Transform target;
    public Vector3 offset;
    public Camera cam;

    void Update()
    {
        float minX = img.GetPixelAdjustedRect().width / 2;
        float maxX = Screen.width - minX;

        float minY = img.GetPixelAdjustedRect().width / 2;
        float maxY = Screen.height - minY;

        Vector3 pos = cam.WorldToScreenPoint(target.position + offset);

        if (Vector3.Dot((target.position - transform.position), transform.forward) < 0)
        {
            if (pos.y < Screen.height / 2) pos.y = maxY;
            else pos.y = minY;

            if (pos.x < Screen.height / 2) pos.x = maxX;
            else pos.x = minX;
        }

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        float angle = Mathf.Atan2(target.position.x - transform.position.x, target.position.z - transform.position.z) * Mathf.Rad2Deg;

        img.transform.position = pos;
        img.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
