import socket
import cv2
import numpy as np
import pickle
import struct
import time

server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
host = '127.0.0.1' 
port = 12345

server_socket.bind((host, port))
server_socket.listen(1)
print(f"Server listening on {host}:{port}...")
client_socket, client_address = server_socket.accept()
print(f"Connection from {client_address} has been established!")

cap = cv2.VideoCapture(1)
if not cap.isOpened():
    cap = cv2.VideoCapture(0)

if not cap.isOpened():
    print("Unable to open camera")
    exit()

text = "background1"

def capture_image():
    global text
    while True:
        data = client_socket.recv(1024)
        print(f"Received from client: {data.decode()}")

        if data.decode() == "exit":
            client_socket.sendall(b"Hello from server!")
            break

        if data.decode() == "capture":
            client_socket.sendall(b"Hello from server!")
            cv2.imwrite("captured_image.jpg", frame)

        if data.decode() == "background2":
            text = "background2"
            client_socket.sendall(b"Hello from server!")

        if data.decode() == "background3":
            text = "background3"
            client_socket.sendall(b"Hello from server!")

        if data.decode() == "getFrame":
            ret, frame = cap.read()
            if not ret:
                print("Error: Cannot read frame from camera.")
                break
            # frame_resized = cv2.resize(frame, (64, 48))
            # data = pickle.dumps(frame_resized)
            
            backgroundImage(frame)

            # แปลงภาพเป็น JPEG
            encode_param = [int(cv2.IMWRITE_JPEG_QUALITY), 90]  # คุณภาพของ JPEG (90%)
            result, encoded_image = cv2.imencode('.jpg', frame, encode_param)
            if not result:
                print("Error: Cannot encode frame to JPEG.")
                continue
            data = encoded_image.tobytes()
            message_size = struct.pack("L", len(data))
            client_socket.sendall(message_size + data)

    # ปิดการเชื่อมต่อ
    client_socket.close()
    server_socket.close()

def backgroundImage(frame):
    font = cv2.FONT_HERSHEY_SIMPLEX
    font_scale = 2
    color = (0, 0, 255)
    thickness = 10
    text_size = cv2.getTextSize(text, font, font_scale, thickness)[0]
    text_x = 0
    text_y = 50
    # text_x = (frame.shape[1] - text_size[0]) // 2 
    # text_y = (frame.shape[0] + text_size[1]) // 2
    cv2.putText(frame, text, (text_x, text_y), font, font_scale, color, thickness)

if __name__ == "__main__":
    capture_image()