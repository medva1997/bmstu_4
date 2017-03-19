#include "FileProcessing.h"
#include "MemoryRes.h"


TError ReadCountFromFile(int &count, FILE *file)
{
	if (fscanf(file, "%d", &count) == 1)
		return ErrorNoErr;
	return ErrorReadFile;
}

TError ReadVertexFromFile(double &x, double &y, double &z, FILE *file)
{
	if (fscanf(file, "%f %f %f", &x, &y, &z) == 3)
		return ErrorNoErr;
	return ErrorReadFile;
}

TError ReadVertexFromFile(Vertex_T &vertex, FILE *file)
{
	double x=0.0, y=0.0, z=0.0;
	TError error = ReadVertexFromFile(x, y, z, file);
	if (error != ErrorNoErr)
		return error;
	VertexSet(vertex, x, y, z);
	return error;
}

TError ReadEdgeFromFile(double &from, double &to, FILE *file)
{
	if (fscanf(file, "%f %f", &from, &to) == 2)
		return ErrorNoErr;
	return ErrorReadFile;
}

TError ReadEdgeFromFile(Edge_T &edge, FILE *file)
{
	double from = 0, to = 0;
	TError error = ReadEdgeFromFile(from, to, file);
	if (error != ErrorNoErr)
		return error;
	EdgeSet(edge, from, to);
	return error;
}

TError LoadModelFromFile(Model_T &model, const char *filename)
{
	FILE *file = fopen(filename, "r");
	if (!file)
		return ErrorOpenFile;

	TError error = ErrorNoErr;
	error = LoadModelFromFile(model, file);
	return error;
}

TError LoadModelFromFile(Model_T &model, FILE *file)
{
	TError error = ErrorNoErr;

	VertexArray_T VertexArr = InitVertexArray();
	error = ReadVertexArrayFromFile(VertexArr, file);
	if (error != ErrorNoErr)
		return error;

	EdgeArray_T EdgeArr = InitEdgeArray();
	error = ReadEdgeArrayFromFile(EdgeArr, file);
	if (error != ErrorNoErr)
		return error;

	ModelSet(model, VertexArr);
	ModelSet(model, EdgeArr);
	return error;
}

TError ReadVertexArrayFromFile(VertexArray_T &VertexArray, FILE *file)
{
	TError error = ErrorNoErr;
	int count = 0;
	error = ReadCountFromFile(count, file);
	if (error != ErrorNoErr)
		return error;
	VertexArraySet(VertexArray, count);
	void *ptr = AllocateMemory(sizeof(Vertex_T)*count);
	if (!ptr)
		return ErrorMemoryAlloc;
	VertexArraySet(VertexArray, (Vertex_T *)ptr);
	Vertex_T vert = InitVertex();
	for (int i = 0; i < count; i++)
	{
		error = ReadVertexFromFile(vert, file);
		if (error == ErrorNoErr)
			VertexArraySet(VertexArray, vert, i);
	}
	return error;
}

TError ReadEdgeArrayFromFile(EdgeArray_T &EdgeArray, FILE *file)
{
	TError error = ErrorNoErr;
	int count = 0;
	error = ReadCountFromFile(count, file);
	if (error != ErrorNoErr)
		return error;
	EdgeArraySet(EdgeArray, count);
	void *ptr = AllocateMemory(sizeof(Edge_T)*count);
	if (!ptr)
		return ErrorMemoryAlloc;
	EdgeArraySet(EdgeArray, (Edge_T *)ptr);
	Edge_T edge = InitEdge();
	for (int i = 0; i < count; i++)
	{
		error = ReadEdgeFromFile(edge, file);
		if (error == ErrorNoErr)
			EdgeArraySet(EdgeArray, edge, i);
	}
	return error;
}


TError PrintVertexToFile(Vertex_T &vertex, FILE *file)
{
	double x = VertexGetX(vertex);
	double y = VertexGetY(vertex);
	double z = VertexGetZ(vertex);
	if (fprintf(file, "%f %f %f\n", x, y, z) < 0)
		return ErrorWriteFile;
	return ErrorNoErr;
}

TError PrintEdgeToFile(Edge_T &edge, FILE *file)
{
	int from = EdgeGetFrom(edge);
	int to = EdgeGetTo(edge);
	if (fprintf(file, "%f %f\n", from, to) < 0)
		return ErrorWriteFile;
	return ErrorNoErr;
}

TError PrintCountToFile(int count, FILE *file)
{
	if (fprintf(file, "%d\n", count) < 0)
		return ErrorWriteFile;
	return ErrorNoErr;
}

TError SaveModelToFile(Model_T &model, const char *filename)
{
	FILE *file = fopen(filename, "w");
	if (!file)
		return ErrorOpenFile;

	TError error = ErrorNoErr;
	error = SaveModelToFile(model, file);
	return error;
}

TError SaveModelToFile(Model_T &model, FILE *file)
{
	TError error = ErrorNoErr;

	VertexArray_T VertexArr = ModelGetVertexArray(model);
	EdgeArray_T EdgeArr = ModelGetEdgeArray(model);

	error = SaveVertexArrayToFile(VertexArr, file);
	if (error != ErrorNoErr)
		return error;

	error = SaveEdgeArrayToFile(EdgeArr, file);
	return error;
}

TError SaveVertexArrayToFile(VertexArray_T &VertexArr, FILE *file)
{
	int count = VertexArrayGetCount(VertexArr);
	TError error = PrintCountToFile(count, file);
	if (error != ErrorNoErr)
		return error;

	for (int i = 0; i < count; i++)
	{
		Vertex_T vertex = VertexArrayGetElement(VertexArr, i);
		error = PrintVertexToFile(vertex, file);
		if (error != ErrorNoErr)
			return error;
	}
	return error;
}

TError SaveEdgeArrayToFile(EdgeArray_T &EdgeArr, FILE *file)
{
	int count = EdgeArrayGetCount(EdgeArr);
	TError error = PrintCountToFile(count, file);
	if (error != ErrorNoErr)
		return error;

	for (int i = 0; i < count; i++)
	{
		Edge_T edge = EdgeArrayGetElement(EdgeArr, i);
		error = PrintEdgeToFile(edge, file);
		if (error != ErrorNoErr)
			return error;
	}
	return error;
}



