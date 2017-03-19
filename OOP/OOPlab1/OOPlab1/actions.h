#pragma once
#include "errors.h"
#include "model.h"
#include "Drawing.h"
#include "FileProcessing.h"
#include "Transformations.h"

union Data_T
{
	char *string;
	DrawingPanel panel;
	struct
	{
		double x;
		double y;
		double z;
	}values;
};

char *DataGetString(Data_T &data);
void DataSetStr(Data_T &data, char *string);

DrawingPanel DataGetPanel(Data_T &data);
void DataSet(Data_T &data, DrawingPanel panel);

void DataSet(Data_T &data, double x, double y, double z);
double DataGetValueX(Data_T &data);
double DataGetValueY(Data_T &data);
double DataGetValueZ(Data_T &data);

TError InitPanel_A(DrawingPanel &panel, Data_T data);
TError FreeModel_A(Model_T &model);
TError LoadModel_A(Model_T &model, Data_T data);
TError SaveModel_A(Model_T &model, Data_T data);
TError DrawModel_A(Model_T &model, DrawingPanel &panel);
TError Move_A(Model_T &model, Data_T data);
TError Rotate_A(Model_T &model, Data_T data);