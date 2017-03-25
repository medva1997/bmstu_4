#pragma once
#include <stdio.h>
#include "model.h"
#include "errors.h"


TError LoadModelFromFile(Model_T &model, const char *filename);
TError LoadModelFromFile(Model_T &model, FILE *file);
TError ReadCountFromFile(int &count, FILE *file);
TError ReadVertexFromFile(double &x, double &y, double &z, FILE *file);
TError ReadVertexFromFile(Vertex_T &vertex, FILE *file);
TError ReadEdgeFromFile(double &from, double &to, FILE *file);
TError ReadEdgeFromFile(Edge_T &edge, FILE *file);
TError ReadVertexArrayFromFile(VertexArray_T &VertexArray, FILE *file);
TError ReadEdgeArrayFromFile(EdgeArray_T &EdgeArray, FILE *file);

TError SaveModelToFile(Model_T &model, const char *filename);
TError SaveModelToFile(Model_T &model, FILE *file);
TError PrintVertexToFile(Vertex_T &vertex, FILE *file);
TError PrintEdgeToFile(Edge_T &edge, FILE *file);
TError PrintCountToFile(int count, FILE *file);
TError SaveVertexArrayToFile(VertexArray_T &VertexArr, FILE *file);
TError SaveEdgeArrayToFile(EdgeArray_T &EdgeArr, FILE *file);



