import cv2
import mediapipe as mp
import socket


width, height = 1280, 720

mp_drawing = mp.solutions.drawing_utils
mp_drawing_styles = mp.solutions.drawing_styles
mp_pose = mp.solutions.pose

cap = cv2.VideoCapture(0)
cap.set(3, width)
cap.set(4, height)

sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
serverAddressPort = ('127.0.0.1', 2411)

with mp_pose.Pose(
    min_detection_confidence= 0.5,
    min_tracking_confidence = 0.5) as pose:
    while cap.isOpened():
        success, image = cap.read()
        if not success:
            print("Ignoring No Video in Camera frame")
            continue

        image.flags.writeable = False
        image = cv2.cvtColor(image, cv2.COLOR_BGR2RGB)
        results = pose.process(image)

        image.flags.writeable = True
        image = cv2.cvtColor(image, cv2.COLOR_RGB2BGR)

        mp_drawing.draw_landmarks(
            image,
            results.pose_landmarks,
            mp_pose.POSE_CONNECTIONS,
            landmark_drawing_spec=mp_drawing_styles.get_default_pose_landmarks_style()
        )
        data = []
        if results.pose_landmarks:
            landmark_list = [[lm.x, lm.y, lm.z] for lm in results.pose_landmarks.landmark]
            # print("Landmark List:", landmark_list)
            for lm in landmark_list:
                data.extend([lm[0], height - lm[1], lm[2]])
            sock.sendto(str.encode(str(data)), serverAddressPort)

        cv2.imshow('Mediapipe Pose Estimation Program Video', cv2.flip(image,1))

        key = cv2.waitKey(1)
        if key & 0xFF == ord('q'):
            break
    cap.release() 
