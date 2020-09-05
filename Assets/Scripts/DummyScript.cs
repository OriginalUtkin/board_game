using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int x = 2;
        int y = x*2;
        Debug.Log("DummyScript started");
        Debug.Log(string.Format("x: {0} y: {1}", x, y));

    }

    // Update is called once per frame
    void Update()
    {
    }
}
