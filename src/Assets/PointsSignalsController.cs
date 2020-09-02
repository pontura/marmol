using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsSignalsController : MonoBehaviour
{
    public Transform container;
    public PointSignal pointSignal;
    public List<PointSignal> all;
    public Camera cam;
    void Start()
    {
        Events.OnAddDistanceSignal += OnAddDistanceSignal;
    }
    private void OnDestroy()
    {
        Events.OnAddDistanceSignal -= OnAddDistanceSignal;
    }
    void OnAddDistanceSignal(GameObject go1, GameObject go2, string value)
    {
        PointSignal newPointSignal = Instantiate(pointSignal, container);
        newPointSignal.Init(go1, go2, value);
        newPointSignal.transform.localScale = Vector3.one;
        all.Add(newPointSignal);
    }
    private void Update()
    {
        if (all.Count == 0)
            return;
        foreach(PointSignal ps in all)
        {
            ps.transform.position = cam.WorldToScreenPoint(Vector3.Lerp(ps.target1.transform.position, ps.target2.transform.position, 0.5f));
        }
    }
}
