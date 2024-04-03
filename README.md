# Junior-IS-Low-Cost-Motion-Capture

## Install Modules for 3D Human Pose Estimation

To use the functionalities provided in this project, you need to install the following Python modules:

### OpenCV
```bash
pip install opencv-python
```

### Mediapipe
```bash
pip install mediapipe==0.10.9
```
### Description of pose_estimation.py
1. Initializes a video capture device (webcam) and sets the resolution.
2. Initializes MediaPipe models for pose estimation and hand detection.
3. Captures frames from the video feed.
4. Processes and converts each frame to RGB color space.
5. Performs pose estimation and hand detection.
6. Draws pose and hand landmarks on the frame.
7. Extracts landmark data and sends it over UDP socket to be later accesed and read by Unity.
8. Displays the processed frame with overlayed skeleton.
9. Teminates the program when 'q' is pressed.

### Stepwise Description of UDPReceive.cs:


### Description of Tracking.cs:
This C# Unity script, named "Tracking", is designed to receive data via UDP (User Datagram Protocol) and update the positions of various body joint obejects created in Unity accordingly. Here's a summary of what the code does:

1. The script starts by importing necessary Unity libraries.
2. It defines a class named "Tracking" that inherits from MonoBehaviour, indicating that it is a Unity script.
3. Public variables are declared to store references to an object responsible for receiving UDP data (udpReceive), arrays of GameObjects representing body joints (bodyJoints), left hand joints (leftHandJoints), and right hand joints (rightHandJoints).
4. The Start() method is defined but left empty, implying there's no initialization logic required at the start of the script.
5. The Update() method is defined, which is called once per frame by Unity.

Within the Update() method:
6. The script retrieves the UDP data received by udpReceive and removes the leading and trailing characters (as they are delimiters).
7. The data is split into individual points using the comma as a delimiter.

For each body joint (33 in total):
8. The X, Y, and Z coordinates are extracted from the data and multiplied by 1.5 for scaling.
9. The local position of the corresponding GameObject in the bodyJoints array is updated with the new coordinates.
10. Similar operations are performed for the left and right hand joints (21 in each), with an additional offset applied to adjust their positions.
