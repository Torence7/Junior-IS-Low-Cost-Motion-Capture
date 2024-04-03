import cv2
import mediapipe as mp
import socket

width, height = 1280, 720

mp_drawing = mp.solutions.drawing_utils
mp_drawing_styles = mp.solutions.drawing_styles
mp_pose = mp.solutions.pose
mp_hands = mp.solutions.hands  # Import mediapipe hands

cap = cv2.VideoCapture(0)
cap.set(3, width)
cap.set(4, height)

sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
serverAddressPort = ('127.0.0.1', 2411)

with mp_pose.Pose(
    min_detection_confidence=0.5,
    min_tracking_confidence=0.5) as pose, mp_hands.Hands(min_detection_confidence=0.5, min_tracking_confidence=0.5) as hands:

    while cap.isOpened():
        success, image = cap.read()
        if not success:
            print("Ignoring No Video in Camera frame")
            continue

        image.flags.writeable = False
        image = cv2.cvtColor(image, cv2.COLOR_BGR2RGB)

        # Body Pose Estimation
        pose_results = pose.process(image)
        hand_results = hands.process(image)

        image.flags.writeable = True
        image = cv2.cvtColor(image, cv2.COLOR_RGB2BGR)

        mp_drawing.draw_landmarks(
            image,
            pose_results.pose_landmarks,
            mp_pose.POSE_CONNECTIONS,
            landmark_drawing_spec=mp_drawing_styles.get_default_pose_landmarks_style()
        )

        if hand_results.multi_hand_landmarks:
            for hand_landmarks in hand_results.multi_hand_landmarks:
                mp_drawing.draw_landmarks(
                    image,
                    hand_landmarks,
                    mp_hands.HAND_CONNECTIONS,
                    landmark_drawing_spec=mp_drawing_styles.get_default_hand_landmarks_style()
                )

        data = []
        if pose_results.pose_landmarks:
            landmark_list = [[lm.x, lm.y, lm.z] for lm in pose_results.pose_landmarks.landmark]
            for lm in landmark_list:
                data.extend([lm[0], height - lm[1], lm[2]])


        if hand_results.multi_hand_landmarks:
            left_hand_present = False
            right_hand_present = False
            left_landmarks = []
            right_landmarks = []

            for hand_landmarks, handedness in zip(hand_results.multi_hand_landmarks, hand_results.multi_handedness):
                if handedness.classification[0].label == "Left":
                    left_hand_present = True
                    for lm in hand_landmarks.landmark:
                        left_landmarks.extend([lm.x, height - lm.y, lm.z])

                elif handedness.classification[0].label == "Right":
                    right_hand_present = True
                    for lm in hand_landmarks.landmark:
                        right_landmarks.extend([lm.x, height - lm.y, lm.z])

            # Extend data with left hand landmarks or zeros if not detected
            if left_hand_present:
                data.extend(left_landmarks)
            else:
                data.extend([0] * 63)  # Assuming 21 landmarks (3 coordinates per landmark)

            # Extend data with right hand landmarks or zeros if not detected
            if right_hand_present:
                data.extend(right_landmarks)
            else:
                data.extend([0] * 63)  # Assuming 21 landmarks (3 coordinates per landmark)

        else:
            data.extend([0] * 126)  # Assuming 42 landmarks total (21 for each hand)

        cv2.imshow('Mediapipe Pose and Hand Estimation', cv2.flip(image, 1))
        sock.sendto(str.encode(str(data)), serverAddressPort)

        key = cv2.waitKey(1)
        if key & 0xFF == ord('q'):
            break

    cap.release()
