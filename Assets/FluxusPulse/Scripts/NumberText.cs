using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class NumberText : MonoBehaviour
{
    public string floatFormat = "F2";

    private Text m_text;
    public Text TextElement { get { return m_text; } }

    public string Text {
        get { return m_text.text; }
        set { m_text.text = value; }
    }
    //~ prop
    
    void Awake()
    {
        m_text = GetComponent<Text>();
    }
    //~ fn

    public void SetInt(int value)
    {
        TextElement.text = value.ToString();
    }
    //~ fn

    public void SetFloat(float value)
    {
        TextElement.text = value.ToString(floatFormat);
    }
    //~ fn
}
//~ class
