using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrolleyController : MonoBehaviour
{
    [SerializeField] private Slider trolleySlider;
    [SerializeField] private GameObject crane;

    public Transform nearLimit;
    public Transform farLimit;

    void Start()
    {
        
    }

    void Update()
    {
        this.transform.position = Vector3.Lerp(nearLimit.position, farLimit.position, trolleySlider.value);
        this.transform.rotation = crane.transform.rotation;
    }
}
