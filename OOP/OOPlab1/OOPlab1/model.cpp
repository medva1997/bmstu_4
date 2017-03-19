#include "model.h"

Model_T InitModel()
{
	Model_T Model;
	Model.VertexArray = InitVertexArray();
	Model.EdgeArray = InitEdgeArray();
	return Model;
}

VertexArray_T ModelGetVertexArray(const Model_T &model)
{
	return model.VertexArray;
}

EdgeArray_T ModelGetEdgeArray(const Model_T &model)
{
	return model.EdgeArray;
}

void ModelSet(Model_T &model, VertexArray_T &VertexArray)
{
	model.VertexArray = VertexArray;
}

void ModelSet(Model_T &model, EdgeArray_T &EdgeArray)
{
	model.EdgeArray = EdgeArray;
}

TError FreeModel(Model_T &model)
{
	VertexArray_T vertexarr = ModelGetVertexArray(model);
	EdgeArray_T edgearr = ModelGetEdgeArray(model);
	TError error = FreeVertexArray(vertexarr);
	error = FreeEdgeArray(edgearr);
	return error;
}

