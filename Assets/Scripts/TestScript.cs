using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.KeypadPlus))
            Debug.Log("GetKey KeypadPlus");
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
            Debug.Log("GetKeyDown KeypadPlus");
    }
}
