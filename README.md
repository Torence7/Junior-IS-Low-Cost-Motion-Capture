# Junior-IS-Low-Cost-Motion-Capture

This project takes inspiration and adapts some approaches from the YouTube video titled "3d Hand Tracking in Virtual Environment Computer Vision". This project's procedure utilises this video's approach to sending data to Unity as well as approaches to rendering a model representation using pose estimation data. Given that this video only explores motion capture for a single hand, this project extends the methodology outlined in the video by incorporating the capability to model and perform pose estimation on the full human body and either one hand or both hands simultaneously. The software of this project also utilises the OpenCV and Mediapipe Library documentation as a guide on how to achieve the aforementioned pose estimation goals.

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
### pose_estimation.py
1. Initializes a video capture device (webcam) and sets the resolution.
2. Initializes MediaPipe models for pose estimation and hand detection.
3. Captures frames from the video feed.
4. Processes and converts each frame to RGB color space.
5. Performs pose estimation and hand detection.
6. Draws pose and hand landmarks on the frame.
7. Extracts landmark data and sends it over UDP socket to be later accesed and read by Unity.
8. Displays the processed frame with overlayed skeleton.
9. Teminates the program when 'q' is pressed.

### UDPReceive.cs:
The Unity script, "UDPReceive", sets up a UDP client to receive data asynchronously. It runs a separate thread to continuously listen for incoming data on the specified port. When data is received, it converts the byte array into a UTF-8 encoded string and stores it in the data variable. Optionally, it can print the received data to the console.

### Tracking.cs:
This C# Unity script, named "Tracking", is designed to receive data via UDP (User Datagram Protocol) and update the positions of various body joint obejects created in Unity accordingly. Here's a summary of what the code does:

1. The script starts by importing necessary Unity libraries.
2. It defines a class named "Tracking" that inherits from MonoBehaviour, indicating that it is a Unity script.
3. Public variables are declared to store references to an object responsible for receiving UDP data (udpReceive), arrays of GameObjects representing body joints (bodyJoints), left hand joints (leftHandJoints), and right hand joints (rightHandJoints).
4. The Start() method is defined but left empty, implying there's no initialization logic required at the start of the script.
5. The Update() method is defined, which is called once per frame by Unity.

Within the Update() method:

6. The script retrieves the UDP data received by udpReceive and removes the leading and trailing characters (as they are delimiters).
8. The data is split into individual points using the comma as a delimiter.

For each body joint (33 in total):

8. The X, Y, and Z coordinates are extracted from the data and multiplied by 1.5 for scaling.
9. The local position of the corresponding GameObject in the bodyJoints array is updated with the new coordinates.
10. Similar operations are performed for the left and right hand joints (21 in each), with an additional offset applied to adjust their positions.


### LineCode.cs:
LineCode.cs is responsible for rendering a line between two points in the scene when displatying the rudimentary skeletal model. It requires two transforms: one representing the origin point and another representing the destination point of the line.

- In the Start() method, it initializes the LineRenderer component and sets its width.
- In the Update() method, it updates the positions of the line's start and end points to match the positions of the origin and destination transforms, respectively.


All the C# code is attached to individual objects (spheres and lines) in the Unity environent to enable functionality. To enable the application to run, the Unity Environment must be runnign whilst pose estimation is active. The data is continously read into the environment to enable the model to replicate movements.


### Todo List:
All the main features for the project have been implemented. These are improvements on some of the features.
- Update LineCode.cs to display differents line widths for the hand models as compared to the body model.
- Add a central stage in the form of a white floor for the model to be rendered on.
- Work on finiding th appropriate offset for the hand models so that they correspond with the wrist of the body model.
- Try creating a head for the skeletal model (in the form of a big sphere)
- Make lines cylindrical instead of 2d to enhance the skeletons appearnace.
- Work on finding permanent scene camera settings to avoid having to adjust the camera manually.
