import socket

# 接続先の設定
target_ip = "127.0.0.1"
target_port = 19564
buffer_size = 4096

# ソケットオブジェクトの作成
tcp_client = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

# サーバに接続
tcp_client.connect((target_ip,target_port))
print("connect")
while True:
    # 送信する文字列の入力
    send_data = input(">>> ")
    if send_data == "exit":
        tcp_client.shutdown(socket.SHUT_WR)
        tcp_client.close()
        break
    # データの送信
    tcp_client.send(send_data.encode("utf-8"))
    # サーバからの応答受信
    recv_data = tcp_client.recv(buffer_size)
    print(recv_data.decode("utf-8"))

