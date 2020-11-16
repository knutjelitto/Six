#pragma once

#include "pch.h"

typedef struct String
{
	BYTE* memory;
	LONG32 alloced;
	LONG32 used;
} String;