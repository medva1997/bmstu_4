#pragma once
#include "errors.h"

struct Vertex_T
{
	double x;
	double y;
	double z;
};

struct Edge_T
{
	int from;
	int to;
};

struct VertexArray_T
{
	Vertex_T *vertexarr;
	int count;
};

struct EdgeArray_T
{
	Edge_T *edgearr;
	int count;
};

Vertex_T InitVertex();
double VertexGetX(const Vertex_T &vertex);
double VertexGetY(const Vertex_T &vertex);
double VertexGetZ(const Vertex_T &vertex);
void VertexSet(Vertex_T &vertex, double xval, double yval, double zval);

Edge_T InitEdge();
int EdgeGetFrom(const Edge_T &edge);
int EdgeGetTo(const Edge_T &edge);
void EdgeSet(Edge_T &edge, int fromval, int toval);

VertexArray_T InitVertexArray();
int VertexArrayGetCount(const VertexArray_T &VertexArray);
Vertex_T VertexArrayGetElement(const VertexArray_T &VertexArray, int i);
void VertexArraySet(VertexArray_T &VertexArray, Vertex_T *array);
void VertexArraySet(VertexArray_T &VertexArray, Vertex_T vertex, int i);
void VertexArraySet(VertexArray_T &VertexArray, int count);
Vertex_T VertexArrayGetCenter(const VertexArray_T &VertexArray);
TError FreeVertexArray(VertexArray_T &VertexArray);

EdgeArray_T InitEdgeArray();
int EdgeArrayGetCount(const EdgeArray_T &EdgeArray);
Edge_T EdgeArrayGetElement(const EdgeArray_T &EdgeArray, int i);
void EdgeArraySet(EdgeArray_T &EdgeArray, Edge_T *array);
void EdgeArraySet(EdgeArray_T &EdgeArray, Edge_T edge, int i);
void EdgeArraySet(EdgeArray_T &EdgeArray, int count);
TError FreeEdgeArray(EdgeArray_T &EdgeArray);