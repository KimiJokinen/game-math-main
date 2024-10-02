using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CableController : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject trolley;

    void Start()
    {
        
    }

    void Update()
    {
        this.transform.localScale = new Vector3(1, slider.value*1.8f + 0.1f, 1);
        this.transform.position = trolley.transform.position;
    }
}
