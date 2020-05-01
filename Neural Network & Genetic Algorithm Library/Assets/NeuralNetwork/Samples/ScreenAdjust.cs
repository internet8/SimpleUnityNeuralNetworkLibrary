using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenAdjust : MonoBehaviour
{
    void Start()
    {
        Camera.main.aspect = 1080f / 1080f;
    }
}
