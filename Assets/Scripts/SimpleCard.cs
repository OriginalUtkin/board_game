using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCard : MonoBehaviour
{
    public GameObject cardFaceTemplate;
    public GameObject cardFace;
    public GameObject cardBackTemplate;

    public Main mainScript;

    // Start is called before the first frame update
    void Start()
    {
        cardFace = Instantiate(cardFaceTemplate, transform.position, Quaternion.identity);
        cardFace.transform.SetParent(transform);
        // cardFace.transform.position = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown() {
        Debug.Log("Mouse down");
        mainScript.PlayCard(this);
    }
}
