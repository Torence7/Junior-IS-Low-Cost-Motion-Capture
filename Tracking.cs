// This project takes inspiration and adapts some approaches from the YouTube video titled 
// "3d Hand Tracking in Virtual Environment Computer Vision". This project's procedure utilises 
// this video's approach to sending data to Unity as well as approaches to rendering a model 
// representation using pose estimation data. Given that this video only explores motion capture 
// for a single hand, this project extends the methodology outlined in the video by incorporating 
// the capability to model and perform pose estimation on the full human body and either one hand 
// or both hands simultaneously. The software of this project also utilises the OpenCV and Mediapipe Library 
// documentation as a guide on how to achieve the aforementioned pose estimation goals.


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracking : MonoBehaviour
{
    // Reference to the UDPReceive script for receiving data
    public UDPReceive udpReceive;
    
    // Arrays to hold body joints, left hand joints, and right hand joints
    public GameObject[] bodyJoints;        // Array for body joints
    public GameObject[] leftHandJoints;    // Array for left hand joints
    public GameObject[] rightHandJoints;   // Array for right hand joints

    void Start()
    {
        // Initialization code can go here if needed
    }

    void Update()
    {
        // Receive data from UDPReceive script
        string data = udpReceive.data;
        
        // Remove leading and trailing characters from the received data (assuming they are delimiters)
        data = data.Remove(0, 1);
        data = data.Remove(data.Length - 1, 1);
        // Print the received data (for debugging purposes)
        print(data);

        // Split the received data into individual points
        string[] points = data.Split(',');
        // Print the first point (for debugging purposes)
        print(points[0]);

        // Update body joints
        for (int i = 0; i < 33; i++)
        {
            // Extract x, y, and z coordinates of the joint from the data and scale them
            float x = float.Parse(points[i * 3]) * 1.5f;
            float y = float.Parse(points[i * 3 + 1]) * 1.5f;
            float z = float.Parse(points[i * 3 + 2]) * 1.5f;

            // Update the position of the body joint
            bodyJoints[i].transform.localPosition = new Vector3(x, y, z);
        }

        // Update left hand joints
        for (int i = 0; i < 21; i++)
        {
            // Extract x, y, and z coordinates of the left hand joint from the data and scale them
            float x = float.Parse(points[(i + 33) * 3]) * 1.5f;
            float y = float.Parse(points[(i + 33) * 3 + 1]) * 1.5f;
            float z = float.Parse(points[(i + 33) * 3 + 2]) * 1.5f;
            float yoffset = 0.3f;
            float zoffset = 0.5f;

            // Update the position of the left hand joint, with additional offset adjustments
            leftHandJoints[i].transform.localPosition = new Vector3(x, y + yoffset, z - zoffset);
        }

        // Update right hand joints
        for (int i = 0; i < 21; i++)
        {
            // Extract x, y, and z coordinates of the right hand joint from the data and scale them
            float x = float.Parse(points[(i + 54) * 3]) * 1.5f;
            float y = float.Parse(points[(i + 54) * 3 + 1]) * 1.5f;
            float z = float.Parse(points[(i + 54) * 3 + 2]) * 1.5f;
            float yoffset = 0.3f;
            float zoffset = 0.5f;

            // Update the position of the right hand joint, with additional offset adjustments
            rightHandJoints[i].transform.localPosition = new Vector3(x, y + yoffset, z - zoffset);
        }
    }
}
