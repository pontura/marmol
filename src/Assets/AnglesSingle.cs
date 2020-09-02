using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.XR.ARFoundation;

public class AnglesSingle : MonoBehaviour
{
    int id = 1;

    [Serializable]
    public class WallData
    {
        public int id;
        public GameObject anglesGO;
        public Vector3 angle;
    }
    public Text field;
    public List<WallData> walls;

    public Transform container;
    public GameObject placedPrefab;

    private void Start()
    {
        Events.OnAddAngle += OnAddAngle;

        AddAngle(1);
        AddAngle(2);
    }
    private void OnDestroy()
    {
        Events.OnAddAngle -= OnAddAngle;
    }
    public void ChangeGO(int _id)
    {
        this.id = _id;
    }
    public void ResetAngles()
    {
        Utils.RemoveAllChildsIn(container);
        field.text = "";
        walls.Clear();
    }
    void AddAngle(int _id)
    {
        GameObject go = Instantiate(placedPrefab);
        go.transform.SetParent(container);

        WallData wallData = new WallData();
        wallData.anglesGO = go;
        wallData.id = _id;
        walls.Add(wallData);
    }
    void OnAddAngle(UnityEngine.XR.ARSubsystems.TrackableId trackeableID, Vector3 pos, Quaternion rot)
    {
        GameObject go = GetWallData().anglesGO;
        go.transform.position = pos;
        go.transform.rotation = rot;
    }
    WallData GetWallData()
    {
        foreach (WallData wallData in walls)
        {
            if (wallData.id == id)
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
            wallData.angle = wallData.anglesGO.transform.localEulerAngles;
        }
        field.text = "";

        foreach (WallData wallData in walls)
        {
            if (wallData.angle.y > 180)
                wallData.angle.y -= 180;
        }

        float angleDiff = walls[0].angle.y - walls[1].angle.y;
        int _id = 0;
        field.text += "ANGULO: " + Mathf.Abs(angleDiff) + "\n";
        foreach (WallData wallData in walls)
        {
            field.text += "id: " + _id + " " + wallData.angle.y + "\n";
            _id++;
        }
    }
}
