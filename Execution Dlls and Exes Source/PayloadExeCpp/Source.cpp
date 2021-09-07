#include <stdio.h>
#include <Windows.h>
#include <winternl.h>

#pragma comment(lib,"ntdll.lib")
void main()
{
	MessageBoxA(NULL, "Hello", "Welcome", 0);
	//return 0;
}