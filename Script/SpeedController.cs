using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class SpeedController : MonoBehaviour
{
    public Slider mySlider;

    // Start is called before the first frame update
    void Start()
    {
        mySlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }

    public void ValueChangeCheck()
    {

        Debug.Log(mySlider.value);
    }
}
