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
