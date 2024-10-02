using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameMath.UI;

public class CraneController : MonoBehaviour
{
    [SerializeField]
    private GameObject button1;
    [SerializeField]
    private GameObject button2;

    void Start()
    {
        
    }

    void Update()
    {
        if (button1.GetComponent<HoldableButton>().isPointerDown == true)
        {
            this.transform.Rotate(0, 0.1f, 0, Space.World);
        }
        if (button2.GetComponent<HoldableButton>().isPointerDown == true)
        {
            this.transform.Rotate(0, -0.1f, 0, Space.World);
        }
    }
}
