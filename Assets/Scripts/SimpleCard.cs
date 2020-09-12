using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCard : MonoBehaviour
{
    GameObject cardFace;
    Main mainScript;

    public void Setup(GameObject _cardFaceTemplate, Main _mainScript)
    {
        cardFace = Instantiate(_cardFaceTemplate, transform);
        mainScript = _mainScript;
    }

    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        Debug.Log("Mouse down");
        mainScript.PlayCard(this);
    }
}
