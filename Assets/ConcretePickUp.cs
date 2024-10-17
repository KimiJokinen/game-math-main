using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ConcretePickUp : MonoBehaviour, IPointerClickHandler
{

    private bool firstMove = false;
    [SerializeField] private float CraneConcreteAngle;
    [SerializeField] private Transform nearLimit;
    [SerializeField] private Transform farLimit;
    [SerializeField] private Transform crane;
    [SerializeField] private Transform ground;
    private Vector3 planeNormal;
    [SerializeField] private Vector3 craneVector;
    [SerializeField] private Vector3 concreteVector;
    [SerializeField] private Transform trolley;
    [SerializeField] private Vector3 trolleyDistance;
    [SerializeField] private Slider trolleySlider;
    [SerializeField] private Slider cableSlider;
    [SerializeField] private Transform centerPoint;
    [SerializeField] private GameObject hookPoint;


    void Start()
    {
        planeNormal = ground.up;
    }

    void Update()
    {
        craneVector = Vector3.ProjectOnPlane(nearLimit.position - farLimit.position, planeNormal);
        concreteVector = Vector3.ProjectOnPlane(crane.position - this.transform.position, planeNormal);
        CraneConcreteAngle = Vector3.SignedAngle(craneVector, concreteVector, Vector3.up);
        trolleyDistance = Vector3.ProjectOnPlane(trolley.position - this.transform.position, planeNormal);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked!");
        StartCoroutine(RotateCrane());
    }
    IEnumerator RotateCrane()
    {
        if (CraneConcreteAngle >= 0) {
            while (CraneConcreteAngle > 0.1f)
            {
                crane.Rotate(0, 0.1f, 0, Space.World);
                yield return null;
            }
            yield return StartCoroutine(MoveTrolley());
        }
        else if (CraneConcreteAngle < 0)
        {
            while (CraneConcreteAngle < -0.1f)
            {
                crane.Rotate(0, -0.1f, 0, Space.World);
                yield return null;
            }
            yield return StartCoroutine(MoveTrolley());
        }
    }
    IEnumerator MoveTrolley()
    {
        if ((trolleyDistance + craneVector * 0.01f).magnitude < trolleyDistance.magnitude)
        {
            while (trolleyDistance.magnitude > 0.1f)
            {
                trolleySlider.value -= 0.002f;
                yield return null;
            }
            yield return StartCoroutine(LowerCable());
        }
        else if ((trolleyDistance + craneVector * 0.01f).magnitude > trolleyDistance.magnitude)
        {
            while (trolleyDistance.magnitude > 0.1f)
            {
                trolleySlider.value += 0.002f;
                yield return null;
            }
            yield return StartCoroutine(LowerCable());
        }
    }
    IEnumerator LowerCable()
    {
        if (cableSlider.value <= 1f)
        {
            while (cableSlider.value < 1f)
            {
                if (hookPoint.GetComponent<ConcreteMoveScript>().isAttached == true)
                {
                    break;
                }
                cableSlider.value += 0.002f;
                yield return null;
            }
        }
        yield return new WaitForSeconds(1f);
        while (cableSlider.value > 0.01f)
        {
            cableSlider.value -= 0.002f;
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(TeleportConcrete());

    }
    IEnumerator TeleportConcrete()
    {
        hookPoint.GetComponent<ConcreteMoveScript>().isAttached = false;
        this.transform.position = centerPoint.position + Random.insideUnitSphere * 12f; //12 is the distance between near limit and far limit. Fist randomize position on a 12-radius sphere.
        this.transform.position = this.transform.position + Vector3.ProjectOnPlane(this.transform.position - crane.position, planeNormal).normalized * 10f; //Then move the position away from the crane by a distance of 10
        this.transform.position = new Vector3(transform.position.x, Random.Range(10, 20), transform.position.z); //Randomize y-position between 10 and 20
        yield return null;
    }
}
