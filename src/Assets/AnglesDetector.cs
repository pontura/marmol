using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AnglesDetector : MonoBehaviour
{
    [Serializable]
    public class WallData
    {
        public GameObject wall;
        public List<GameObject> anglesGO;
        public Vector3 angle;
    }
    public Text field;
    public List<WallData> walls;

    public GameObject placedPrefab;

    public void AddAngleGO(GameObject hittedWall, Vector3 pos)
    {
        WallData wallData = GetWallData(hittedWall);
        GameObject go = Instantiate(placedPrefab);
        go.transform.position = pos;
        go.transform.localEulerAngles = hittedWall.transform.localEulerAngles;

        if (wallData == null)
        {
            wallData = new WallData();
            wallData.anglesGO = new List<GameObject>();
            wallData.wall = hittedWall;
            walls.Add(wallData);
        }        
        wallData.anglesGO.Add(go);
        
    }
    WallData GetWallData(GameObject hittedWall)
    {
        foreach(WallData wallData in walls)
        {
            if (wallData.wall == hittedWall)
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
        Vector3 v = Vector3.zero;
        foreach (WallData wallData in walls)
        {
            if (wallData.angle.y > 180)
                wallData.angle.y -= 180;
            v +=wallData.angle;
        }
        int id = 0;
        field.text += "promedio: " + v.y/walls.Count + "\n";
        foreach (WallData wallData in walls)
        {
            field.text += "id: " + id + "1 " + wallData.angle.y + "\n";
            id++;
        }
    }
}
