#include <chrono>
#include <thread>
#include <mutex>
#include <queue>
#include "SendImageUDP.h"
#include <Windows.h>

#define WIDTH 640
#define HEIGHT 480
#define PORT  9000
#define SERVER_IP "192.168.0.53"
//#define SERVER_IP "127.0.0.1"

std::queue<cv::Mat> buffer;
std::mutex mutex;
bool isRunning;

void fn(UDPSocket_Image& Socket) {
	cv::Mat image;
	while (1) {
		try
		{
			Socket.RecvFromImg(image);
			if (image.empty()) { continue; }
			else { 
				cv::imshow("recv_image", image);
				isRunning = true;
			}
		}
		catch (std::system_error& e)
		{
			std::cout << e.what();
		}

		cv::waitKey(1);
		system("cls");
	}
}

int main()
{
	std::cout << "Test Controler Operate..." << std::endl;
	isRunning = false;
	WSASession Session;

	std::string serip = SERVER_IP;

	UDPSocket_Image Socket;
	Socket.Bind(PORT);
	
	std::cout << "Waiting..." << std::endl;
	int count = 0;

	std::jthread t1(fn, std::ref(Socket));

	while (1) {
		std :: cout << "Sending Image";
		cv::waitKey(1);
	}
	std::cout << std::endl << "Done" << std::endl;
}