#include "MemoryRes.h"

void *AllocateMemory(int count)
{
	return malloc(count);
}

void FreeMemory(void * memory)
{
	free(memory);
}