
import socket

target_ip = "127.0.0.1"
port = 19564
buffer_size = 1024

tcp_client = socket.socket(socket.AF_INET,socket.SOCK_STREAM)

tcp_client.connect((target_ip,port))

print("connect")

while True:
    send_data = input(">>> ")
    if send_data == "exit":
        tcp_client.shutdown(socket.SHUT_WR)
        tcp_client.close()
        break
    tcp_client.send(send_data.encode("utf-8"))

    recv = tcp_client.recv(buffer_size)
    print(recv.decode("utf-8"))