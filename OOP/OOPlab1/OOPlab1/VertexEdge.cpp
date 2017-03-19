#include "VertexEdge.h"
#include "MemoryRes.h"

// VERTEX

Vertex_T InitVertex()
{
	return { 0.0, 0.0, 0.0 };
}

double VertexGetX(const Vertex_T &vertex)
{
	return vertex.x;
}

double VertexGetY(const Vertex_T &vertex)
{
	return vertex.y;
}

double VertexGetZ(const Vertex_T &vertex)
{
	return vertex.z;
}

void VertexSet(Vertex_T &vertex, double xval, double yval, double zval)
{
	vertex.x = xval;
	vertex.y = yval;
	vertex.z = zval;
}

// EDGE

Edge_T InitEdge()
{
	return{0, 0};
}

int EdgeGetFrom(const Edge_T &edge)
{
	return edge.from;
}

int EdgeGetTo(const Edge_T &edge)
{
	return edge.to;
}

void EdgeSet(Edge_T &edge, int fromval, int toval)
{
	edge.from = fromval;
	edge.to = toval;
}

// ARRAY OF VERTEXES

VertexArray_T InitVertexArray()
{
	return { NULL, 0 };
}

int VertexArrayGetCount(const VertexArray_T &VertexArray)
{
	return VertexArray.count;
}

Vertex_T VertexArrayGetCenter(const VertexArray_T &VertexArray)
{
	if (VertexArray.count <= 0)
		return InitVertex();
	Vertex_T v0 = VertexArrayGetElement(VertexArray, 0);
	double xmin = VertexGetX(v0), xmax = xmin;
	double ymin = VertexGetY(v0), ymax = ymin;
	double zmin = VertexGetZ(v0), zmax = zmin;
	for (int i = 1; i < VertexArray.count; i++)
	{
		Vertex_T vert = VertexArrayGetElement(VertexArray, i);
		double x = VertexGetX(vert);
		if (x > xmax)
			xmax = x;
		if (x < xmin)
			xmin = x;

		double y = VertexGetY(vert);
		if (y > ymax)
			ymax = y;
		if (y < ymin)
			ymin = y;

		double z = VertexGetZ(vert);
		if (z > zmax)
			zmax = z;
		if (z < zmin)
			zmin = z;
	}
	Vertex_T center = InitVertex();
	VertexSet(center, (xmin + xmax) / 2, (ymin + ymax) / 2, (zmin + zmax) / 2);
	return center;
}

Vertex_T VertexArrayGetElement(const VertexArray_T &VertexArray, int i)
{
	if (i < VertexArray.count)
		return VertexArray.vertexarr[i];
	return InitVertex();
}

void VertexArraySet(VertexArray_T &VertexArray, Vertex_T *array)
{
	VertexArray.vertexarr = array;
}

void VertexArraySet(VertexArray_T &VertexArray, Vertex_T vertex, int i)
{
	if (i < VertexArray.count)
		VertexArray.vertexarr[i] = vertex;
}

void VertexArraySet(VertexArray_T &VertexArray, int count)
{
	VertexArray.count = count;
}

TError FreeVertexArray(VertexArray_T &VertexArray)
{
	if (VertexArray.vertexarr != NULL)
		FreeMemory((void *)VertexArray.vertexarr);
	return ErrorNoErr;
}

// ARRAY OF EDGES

EdgeArray_T InitEdgeArray()
{
	return{ NULL, 0 };
}

int EdgeArrayGetCount(const EdgeArray_T &EdgeArray)
{
	return EdgeArray.count;
}

Edge_T EdgeArrayGetElement(const EdgeArray_T &EdgeArray, int i)
{
	if (i < EdgeArray.count)
		return EdgeArray.edgearr[i];
	return InitEdge();
}

void EdgeArraySet(EdgeArray_T &EdgeArray, Edge_T *array)
{
	EdgeArray.edgearr = array;
}

void EdgeArraySet(EdgeArray_T &EdgeArray, Edge_T edge, int i)
{
	if (i < EdgeArray.count)
		EdgeArray.edgearr[i] = edge;
}

void EdgeArraySet(EdgeArray_T &EdgeArray, int count)
{
	EdgeArray.count = count;
}

TError FreeEdgeArray(EdgeArray_T &EdgeArray)
{
	if (EdgeArray.edgearr != NULL)
		FreeMemory((void *)EdgeArray.edgearr);
	return ErrorNoErr;
}