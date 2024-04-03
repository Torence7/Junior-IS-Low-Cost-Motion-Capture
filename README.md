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
