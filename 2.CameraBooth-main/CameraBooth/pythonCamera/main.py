import cv2
import sxcu
from PIL import Image
from time import sleep
from datetime import datetime
import cvzone
import segno
import socket
import cv2
import os
import struct
import time
import numpy as np
import json
from cvzone.SelfiSegmentationModule import SelfiSegmentation
connector = sxcu.SXCU()

segmentor=SelfiSegmentation()
server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

host = '127.0.0.1' 
port = 12345
cam_port = 0
cam = cv2.VideoCapture(cam_port) 
duration = 3
timer_display = 0
capture = None

server_socket.bind((host, port))
server_socket.listen(1)
print(f"Server listening on {host}:{port}...")
client_socket, client_address = server_socket.accept()
print(f"Connection from {client_address} has been established!")

if not cam.isOpened():
    cam = cv2.VideoCapture(0)

if not cam.isOpened():
    print("Unable to open camera")
    exit()

def server():
    global cam, duration, capture, duration, timer_display

    while True: 
        data = client_socket.recv(8192)  #1024  4096  8192
        print(f"Received from client: {data.decode()}")

        if data.decode() == "*cmd=exit":
            client_socket.sendall(b"Hello from server!")
            break

        if data.decode() == "*cmd=capture":
            client_socket.sendall(b"Hello from server!")
            cv2.imwrite("captured_image.jpg", image)

        if data.decode() == "*cmd=getFrame":
            ret, image = cam.read()
            if not ret:
                print("Error: Cannot read frame from camera.")
                break

            img_preview = cv2.flip(cv2.resize(image, (0,0), fx=1.5, fy=1.5), 1)
            encode_param = [int(cv2.IMWRITE_JPEG_QUALITY), 90]
            result, encoded_image = cv2.imencode('.jpg', img_preview, encode_param)
            if not result:
                print("Error: Cannot encode frame to JPEG.")
                continue
            data = encoded_image.tobytes()
            message_size = struct.pack("L", len(data))
            client_socket.sendall(message_size + data)
            continue

        if "*cmd=background" in data.decode():
            split_data = data.decode().split(',')
            packImage(split_data[1], split_data[2])
            continue

        if "*cmd=assemble" in data.decode():
            client_socket.sendall(b"Hello from server!")
            split_data = data.decode().split(',')
            assemble(split_data[1], split_data[2], split_data[3], split_data[4], split_data[5], split_data[6])
            continue

    client_socket.close()
    server_socket.close()

def packImage(pathPreview, pathDrop):
    img = cv2.resize(cv2.imread(pathPreview), (640, 480))
    encode_param = [int(cv2.IMWRITE_JPEG_QUALITY), 90]

    if pathDrop != "none":
        drop = cv2.resize(cv2.imread(pathDrop), (640, 480))
        frame=segmentor.removeBG(img, drop, cutThreshold=0.7)
        result, encoded_image = cv2.imencode('.jpg', frame, encode_param)
    else:
        result, encoded_image = cv2.imencode('.jpg', img, encode_param)

    if not result:
        print("Error: Cannot encode frame to JPEG.")
        return
    data = encoded_image.tobytes()
    message_size = struct.pack("L", len(data))
    client_socket.sendall(message_size + data)

def assemble(pathFolder, file1, file2, file3, pathDrop, pathBackground):
    i1 = cv2.resize(cv2.imread(os.path.join(pathFolder, file1)), (480, 360))
    i2 = cv2.resize(cv2.imread(os.path.join(pathFolder, file2)), (480, 360))
    i3 = cv2.resize(cv2.imread(os.path.join(pathFolder, file3)), (480, 360))
    drop = cv2.resize(cv2.imread(pathDrop), (480, 360))

    frame1=segmentor.removeBG(i1, drop, cutThreshold=0.4)
    frame2=segmentor.removeBG(i2, drop, cutThreshold=0.4)
    frame3=segmentor.removeBG(i3, drop, cutThreshold=0.4)

    pathRemoveBG = os.path.join(pathFolder, "removeBG")
    if not os.path.exists(pathRemoveBG):
        os.makedirs(pathRemoveBG) 

    cv2.imwrite(os.path.join(pathRemoveBG, "g1.jpg"), frame1)
    cv2.imwrite(os.path.join(pathRemoveBG, "g2.jpg"), frame2)
    cv2.imwrite(os.path.join(pathRemoveBG, "g3.jpg"), frame3)

    frame1 = Image.open(os.path.join(pathRemoveBG, "g1.jpg"))
    frame2 = Image.open(os.path.join(pathRemoveBG, "g2.jpg"))
    frame3 = Image.open(os.path.join(pathRemoveBG, "g3.jpg"))

    component_bg = Image.open(pathBackground)
    component_overlay = Image.open("overlay.png")
    component_bg.paste(frame1, (60, 300))
    component_bg.paste(frame2, (60, 750))
    component_bg.paste(frame3, (60, 1200))
    component_bg.paste(component_overlay, (0,0), mask=component_overlay)

    file_name = os.path.join(pathFolder, "booth.png")
    component_bg.save(file_name)

    fi = cv2.imread(file_name)
    printing = cvzone.stackImages([fi, fi], cols=2, scale=1)
    cv2.imwrite(os.path.join(pathFolder, "printing.png"), printing)

    r = connector.upload_file(file_name)
    q = segno.make_qr(r["url"])
    q.save(os.path.join(pathFolder, "qr.png"), scale=15)

    config_data = f"id={r['id']}\nurl={r['url']}\ndel_url={r['del_url']}\nthumb={r['thumb']}"
    with open(os.path.join(pathFolder, "url.logUrl"), 'w') as config_file:
        config_file.write(config_data)

if __name__ == '__main__':
    print("Welcome to Photobooth!")
    server()
