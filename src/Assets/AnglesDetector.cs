using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.XR.ARFoundation;

public class AnglesDetector : MonoBehaviour
{
    [Serializable]
    public class WallData
    {
        public UnityEngine.XR.ARSubsystems.TrackableId trackeableID;
        public List<GameObject> anglesGO;
        public Vector3 angle;
    }
    public Text field;
    public List<WallData> walls;

    public Transform container;
    public GameObject placedPrefab;

    private void Start()
    {
        Events.OnAddAngle += OnAddAngle;
    }
    private void OnDestroy()
    {
        Events.OnAddAngle -= OnAddAngle;
    }
    public void Back()
    {
        Data.Instance.LoadLevel("0_Home");
    }
    public void ResetAngles()
    {
        Utils.RemoveAllChildsIn(container);
        field.text = "";
        walls.Clear();
    }

    void OnAddAngle(UnityEngine.XR.ARSubsystems.TrackableId trackeableID, Vector3 pos, Quaternion rot)
    {
        WallData wallData = GetWallData(trackeableID);
        GameObject go = Instantiate(placedPrefab);
        go.transform.SetParent(container);
        go.transform.position = pos;
        go.transform.rotation = rot;

        if (wallData == null)
        {
            wallData = new WallData();
            wallData.anglesGO = new List<GameObject>();
            wallData.trackeableID = trackeableID;
            walls.Add(wallData);
        }        
        wallData.anglesGO.Add(go);
        
    }
    WallData GetWallData(UnityEngine.XR.ARSubsystems.TrackableId trackeableID)
    {
        foreach(WallData wallData in walls)
        {
            if (wallData.trackeableID == trackeableID)
                return wallData;
        }
        return null;
    }
    private void Update()
    {
        if (walls.Count <= 1)
            return;
        foreach (WallData wallData in walls)
        {
            Vector3 angles = Vector3.zero;
            foreach (GameObject go in wallData.anglesGO)
            {
                angles += go.transform.localEulerAngles;
            }
            wallData.angle = angles / wallData.anglesGO.Count;
        }
        field.text = "";
        
        foreach (WallData wallData in walls)
        {
            if (wallData.angle.y > 180)
                wallData.angle.y -= 180;
        }

        float angleDiff = walls[0].angle.y - walls[1].angle.y;
        int id = 0;
        field.text += "ANGULO: " + angleDiff + "\n";
        foreach (WallData wallData in walls)
        {
            field.text += "id: " + id + "1 " + wallData.angle.y + "\n";
            id++;
        }
    }
}
