#pragma once
#include "VertexEdge.h"

struct Model_T
{
	VertexArray_T VertexArray;
	EdgeArray_T EdgeArray;
};

Model_T InitModel();
VertexArray_T ModelGetVertexArray(const Model_T &model);
EdgeArray_T ModelGetEdgeArray(const Model_T &model);
void ModelSet(Model_T &model, VertexArray_T &VertexArray);
void ModelSet(Model_T &model, EdgeArray_T &EdgeArray);
TError FreeModel(Model_T &model);