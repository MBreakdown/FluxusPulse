using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputScheme : MonoBehaviour
{
    public string vertical = "Vertical";
    public string horizontal = "Horizontal";
    public string boost = "Fire1";
    public string fling = "Fire2";

    public float VerticalAxis => Input.GetAxis(vertical);
    public float HorizontalAxis => Input.GetAxis(horizontal);
    public bool BoostButton => Input.GetButton(boost);
    public bool FlingButton => Input.GetButton(fling);
}
