#pragma once
enum TError
{
	ErrorNoErr = 0,
	ErrorOpenFile,
	ErrorReadFile,
	ErrorSaveFile,
	ErrorWriteFile,
	ErrorModelInit,
	ErrorMemoryAlloc
};

//error text messages
const char *ErrorMessage(TError err);

