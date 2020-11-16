#include "pch.h"
#include "Strings.h"
#include <assert.h>>
#include <stdlib.h>
#include <stdio.h>

#define SIXAPI extern "C" __declspec(dllexport)

SIXAPI void RtPrint(String * string)
{
	WriteFile(GetStdHandle(STD_OUTPUT_HANDLE), string->memory, string->used, NULL, NULL);
	//printf("%s\n", string->memory);
}

SIXAPI String* RtStringCreate(const BYTE* bytes, int length)
{
	String* string = (String*)malloc(sizeof(String));
	assert(string != NULL);
	string->memory = (BYTE*)malloc((size_t)length + 1);
	string->alloced = length + 1;
	string->used = length;

	memcpy(string->memory, bytes, length);
	string->memory[length] = 0;

	return string;
}


SIXAPI String* RtStringConcat(String* s1, String* s2)
{
	return NULL;
}