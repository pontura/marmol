using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(ARRaycastManager))]
public class MeasurementMultipleController : MonoBehaviour
{
    public states state;
    public enum states
    {
        IDLE,
        ACTIVE
    }
    [SerializeField]
    private Transform container;

    [SerializeField]
    private GameObject measurementPointPrefab;

    [SerializeField]
    private PointsSignalsController pointsSignalsController;

    [SerializeField]
    private ARCameraManager arCameraManager;

    private LineRenderer measureLine;
    private ARRaycastManager arRaycastManager;
    public List<GameObject> all;
    private Vector2 touchPosition = default;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Awake()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
        measureLine = GetComponent<LineRenderer>();
    }

    public void Back()
    {
        Data.Instance.LoadLevel("0_Home");
    }
    GameObject activePoint;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                touchPosition = touch.position;               
                if (arRaycastManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
                {
                    AddPoint();
                    activePoint.transform.localScale = Vector3.one;
                    Pose hitPose = hits[0].pose;
                    activePoint.transform.SetPositionAndRotation(hitPose.position, hitPose.rotation);

                    if (all.Count == 0)
                        AddPoint();
                }
                
            }

            if (touch.phase == TouchPhase.Moved)
            {
                touchPosition = touch.position;

                if (arRaycastManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
                {
                    measureLine.gameObject.SetActive(true);
                    activePoint.SetActive(true);

                    Pose hitPose = hits[0].pose;
                    activePoint.transform.SetPositionAndRotation(hitPose.position, hitPose.rotation);

                    state = states.ACTIVE;
                }
                
            }

            if (touch.phase == TouchPhase.Ended && state == states.ACTIVE )
            {
                state = states.IDLE;
                GameObject origin = all[all.Count - 2];
                GameObject dest = all[all.Count - 1];
                Events.OnAddDistanceSignal(origin, dest, CalculateDistance());
            }
        }        
        if (state == states.ACTIVE)
        {
            if (all.Count > 1)
                CalculateDistance();
        }
    }
    void AddPoint()
    {
        activePoint = Instantiate(measurementPointPrefab, Vector3.zero, Quaternion.identity, container);
        all.Add(activePoint);
    }
    string CalculateDistance()
    {
        
        GameObject origin = all[all.Count - 2];
        GameObject dest = all[all.Count - 1];
        measureLine.SetPosition(0, origin.transform.position);
        measureLine.SetPosition(1, dest.transform.position);

        return "Dist: " + (Vector3.Distance(origin.transform.position, dest.transform.position)/* * measurementFactor */).ToString("F2");
    }

    //public GameObject point1;
    //public GameObject point2;
    //public GameObject point3;
    //public GameObject point4;
    //void Start()
    //{
    //    Invoke("Delayed", 1);
    //}
    //void Delayed()
    //{
    //    Events.OnAddDistanceSignal(point1, point2, (Vector3.Distance(point1.transform.position, point2.transform.position)/* * measurementFactor */).ToString("F2"));
    //    Events.OnAddDistanceSignal(point2, point3, (Vector3.Distance(point2.transform.position, point3.transform.position)/* * measurementFactor */).ToString("F2"));
    //    Events.OnAddDistanceSignal(point3, point4, (Vector3.Distance(point3.transform.position, point4.transform.position)/* * measurementFactor */).ToString("F2"));
    //    Events.OnAddDistanceSignal(point4, point1, (Vector3.Distance(point4.transform.position, point1.transform.position)/* * measurementFactor */).ToString("F2"));
    //}
}
