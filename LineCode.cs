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

public class LineCode : MonoBehaviour
{
    LineRenderer lineRenderer; // Reference to the LineRenderer component
    public Transform origin; // The starting point of the line
    public Transform destination; // The ending point of the line
    public float startWidth = 0.05f; // Default start width
    public float endWidth= 0.05f;// Default end width

    // Start is called before the first frame update
    void Start()
    {
        // Get the LineRenderer component attached to this GameObject
        lineRenderer = GetComponent<LineRenderer>();

        // Set the width of the line
        lineRenderer.startWidth = startWidth;
        lineRenderer.endWidth = endWidth;
    }

    // Update is called once per frame
    void Update()
    {
        // Set the starting position of the line to the position of the origin transform
        lineRenderer.SetPosition(0, origin.position);
        
        // Set the ending position of the line to the position of the destination transform
        lineRenderer.SetPosition(1, destination.position);
    }
}
