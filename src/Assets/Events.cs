using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public static class Events
{
    public static System.Action<UnityEngine.XR.ARSubsystems.TrackableId, Vector3, Quaternion> OnAddAngle = delegate { };
    public static System.Action<GameObject, GameObject, string> OnAddDistanceSignal = delegate { };
}
