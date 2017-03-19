#pragma once
#include "VertexEdge.h"
#include "model.h"

void TransfMoveVertex(Vertex_T &vertex, double dx, double dy, double dz);
void TransfRotateVertexX(Vertex_T &vertex, double angle, Vertex_T &center);
void TransfRotateVertexY(Vertex_T &vertex, double angle, Vertex_T &center);
void TransfRotateVertexZ(Vertex_T &vertex, double angle, Vertex_T &center);

void TransfMoveModel(Model_T &model, double dx, double dy, double dz);
void TransfRotateModelX(Model_T &model, double angle);
void TransfRotateModelY(Model_T &model, double angle);
void TransfRotateModelZ(Model_T &model, double angle);