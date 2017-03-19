#include "Transformations.h"
#include <math.h>

#define M_PI       3.14159265358979323846

void TransfMoveVertex(Vertex_T &vertex, double dx, double dy, double dz)
{
	double x = VertexGetX(vertex);
	double y = VertexGetY(vertex);
	double z = VertexGetZ(vertex);
	x += dx;
	y += dy;
	z += dz;
	VertexSet(vertex, x, y, z);
}

void TransfRotateVertexX(Vertex_T &vertex, double angle, Vertex_T &center)
{
	double angleDeg = angle * M_PI / 180;
	double x = VertexGetX(vertex);
	double y = VertexGetY(vertex);
	double z = VertexGetZ(vertex);

	double x0 = VertexGetX(center);
	double y0 = VertexGetY(center);
	double z0 = VertexGetZ(center);

	double newx = x;
	double newy = y0 + cos(angleDeg)*(y - y0) + sin(angleDeg)*(z - z0);
	double newz = z0 - sin(angleDeg)*(y - y0) + cos(angleDeg)*(z - z0);

	VertexSet(vertex, newx, newy, newz);
}

void TransfRotateVertexY(Vertex_T &vertex, double angle, Vertex_T &center)
{
	double angleDeg = angle * M_PI / 180;
	double x = VertexGetX(vertex);
	double y = VertexGetY(vertex);
	double z = VertexGetZ(vertex);

	double x0 = VertexGetX(center);
	double y0 = VertexGetY(center);
	double z0 = VertexGetZ(center);

	double newx = x0 + cos(angleDeg)*(x-x0) - sin(angleDeg)*(z-z0);
	double newy = y;
	double newz = z0 + sin(angleDeg)*(x - x0) + cos(angleDeg)*(z - z0);

	VertexSet(vertex, newx, newy, newz);
}

void TransfRotateVertexZ(Vertex_T &vertex, double angle, Vertex_T &center)
{
	double angleDeg = angle * M_PI / 180;
	double x = VertexGetX(vertex);
	double y = VertexGetY(vertex);
	double z = VertexGetZ(vertex);

	double x0 = VertexGetX(center);
	double y0 = VertexGetY(center);
	double z0 = VertexGetZ(center);

	double newx = x0 + cos(angleDeg)*(x - x0) + sin(angleDeg)*(y - y0);
	double newy = y0 - sin(angleDeg)*(x - x0) + cos(angleDeg)*(y - y0);
	double newz = z;

	VertexSet(vertex, newx, newy, newz);
}

void TransfMoveModel(Model_T &model, double dx, double dy, double dz)
{
	VertexArray_T vertexarr = ModelGetVertexArray(model);
	int count = VertexArrayGetCount(vertexarr);
	for (int i = 0; i < count; i++)
	{
		Vertex_T vertex = VertexArrayGetElement(vertexarr, i);
		TransfMoveVertex(vertex, dx, dy, dz);
		VertexArraySet(vertexarr, vertex, i);
	}
	ModelSet(model, vertexarr);
}

void TransfRotateModelX(Model_T &model, double angle)
{
	VertexArray_T vertexarr = ModelGetVertexArray(model);
	Vertex_T center = VertexArrayGetCenter(vertexarr);
	int count = VertexArrayGetCount(vertexarr);
	for (int i = 0; i < count; i++)
	{
		Vertex_T vertex = VertexArrayGetElement(vertexarr, i);
		TransfRotateVertexX(vertex, angle, center);
		VertexArraySet(vertexarr, vertex, i);
	}
	ModelSet(model, vertexarr);
}

void TransfRotateModelY(Model_T &model, double angle)
{
	VertexArray_T vertexarr = ModelGetVertexArray(model);
	Vertex_T center = VertexArrayGetCenter(vertexarr);
	int count = VertexArrayGetCount(vertexarr);
	for (int i = 0; i < count; i++)
	{
		Vertex_T vertex = VertexArrayGetElement(vertexarr, i);
		TransfRotateVertexY(vertex, angle, center);
		VertexArraySet(vertexarr, vertex, i);
	}
	ModelSet(model, vertexarr);
}

void TransfRotateModelZ(Model_T &model, double angle)
{
	VertexArray_T vertexarr = ModelGetVertexArray(model);
	Vertex_T center = VertexArrayGetCenter(vertexarr);
	int count = VertexArrayGetCount(vertexarr);
	for (int i = 0; i < count; i++)
	{
		Vertex_T vertex = VertexArrayGetElement(vertexarr, i);
		TransfRotateVertexZ(vertex, angle, center);
		VertexArraySet(vertexarr, vertex, i);
	}
	ModelSet(model, vertexarr);
}