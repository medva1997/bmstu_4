#include "errors.h"

const char *ErrorMessage(TError err)
{
	switch (err)
	{
	case ErrorNoErr:
		return "No errors";
	case ErrorOpenFile:
		return "Can not open file";
	case ErrorReadFile:
		return "Can not read file";
	case ErrorSaveFile:
		return "Can not save to file";
	case ErrorWriteFile:
		return "Can not write to file";
	case ErrorModelInit:
		return "Can not init model";
	case ErrorMemoryAlloc:
		return "Memory error";
	default:
		return "Error";
	}
}