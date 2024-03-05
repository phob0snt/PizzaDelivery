using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class SliderValDisplay : MonoBehaviour
{
    public void ChangeValue(Single val)
    {
        if (!Convert.ToString(val).Contains(","))
            GetComponent<TMP_Text>().text = val.ToString();
        else
            GetComponent<TMP_Text>().text = val.ToString()[..3];
    }
}
