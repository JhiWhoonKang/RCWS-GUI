import cv2
import numpy as np
import socket
import struct
from threading import Thread

def send_image_to(sock, image, address):
    encode_param = [int(cv2.IMWRITE_JPEG_QUALITY), 90]
    result, imgencode = cv2.imencode('.jpg', image, encode_param)
    data = np.array(imgencode)
    byteData = data.tobytes()

    # send the length of byte array first
    length = len(byteData)
    sock.sendto(struct.pack('!I', length), address)

    # send the image data
    sock.sendto(byteData, address)

def video_stream(sock, addr):
   cap = cv2.VideoCapture(0) # Capture video from webcam. Adjust the parameter if you have multiple webcams.
   
   while True:
       ret,img= cap.read()
       if not ret: break

       send_image_to(sock,img,addr)

def main():
   sock= socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
   addr= ("127.0.0.1", 12345) # IP and port of receiver
   
   thread= Thread(target=video_stream,args=(sock,addr))
   thread.start()

if __name__ == "__main__":
   main()
