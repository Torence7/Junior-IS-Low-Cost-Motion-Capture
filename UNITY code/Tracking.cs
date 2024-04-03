using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracking : MonoBehaviour
{
    // Start is called before the first frame update
    public UDPReceive udpReceive;
    public GameObject[] bodyJoints;
    public GameObject[] leftHandJoints;
    public GameObject[] rightHandJoints;

    void Start()
    {

    }

    void Update()
    {
      string data = udpReceive.data;
      data = data.Remove(0, 1);
      data = data.Remove(data.Length - 1, 1);
      print(data);

      string[] points = data.Split(',');
      print(points[0]);

      for (int i = 0; i < 33; i++)
      {
          float x = float.Parse(points[i * 3])*1.5f;
          float y = float.Parse(points[i * 3 + 1])*1.5f;
          float z = float.Parse(points[i * 3 + 2])*1.5f;

          bodyJoints[i].transform.localPosition = new Vector3(x, y, z);
      }

      // Update left hand joints
     for (int i = 0; i < 21; i++)
     {
         float x = float.Parse(points[(i + 33) * 3])*1.5f;
         float y = float.Parse(points[(i + 33) * 3 + 1])*1.5f;
         float z = float.Parse(points[(i + 33) * 3 + 2])*1.5f;
         float yoffset = 0.3f;
         float zoffset = 0.5f;
         leftHandJoints[i].transform.localPosition = new Vector3(x, y + yoffset, z - zoffset);
     }

     // Update right hand joints
     for (int i = 0; i < 21; i++)
     {
         float x = float.Parse(points[(i + 54) * 3])*1.5f;
         float y = float.Parse(points[(i + 54) * 3 + 1])*1.5f;
         float z = float.Parse(points[(i + 54) * 3 + 2])*1.5f;
         float yoffset = 0.3f;
         float zoffset = 0.5f;
         rightHandJoints[i].transform.localPosition = new Vector3(x, y + yoffset, z - zoffset);
     }
  }
}
