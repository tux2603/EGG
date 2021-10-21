using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeColor : MonoBehaviour
{

    public Color c = Color.black;
    public Renderer rend;

    // Start is called before the first frame update
    void Start()
    {      
        rend = GetComponent<Renderer>();
        rend.material.color = c;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void Hover(BaseEventData data) {
        setRed();
    }

    public void setRed() {
        c.r = 1.0f;
        c.g = 0.0f;
        c.b = 0.0f;
        updateColor();
    }

    public void setBlue() {
        c.r = 0.0f;
        c.g = 0.0f;
        c.b = 1.0f;
        updateColor();
    }

    public void setGreen() {
        c.r = 0.0f;
        c.g = 1.0f;
        c.b = 0.0f;
        updateColor();
    }

    public void setColor(string hexCode) {
        ColorUtility.TryParseHtmlString(hexCode, out c);
        updateColor();
    }

    void updateColor() {
        rend.material.color = c;
    }
}
