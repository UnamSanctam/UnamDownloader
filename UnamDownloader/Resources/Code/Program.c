#include <windows.h>
#include <ShellAPI.h>

/* Created by Unam Sanctam, https://github.com/UnamSanctam */

int main(int argc, char **argv) 
{
	ShellExecuteA(NULL, "open", "#TARGET", "#RUNCOMMAND", NULL, SW_HIDE);
}