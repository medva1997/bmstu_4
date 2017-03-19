#include "actions.h"

// инициализация панельки, загрузка из файла, перемещение, вращение по XYZ
// сохранение в файл, удаление модели, отрисовка

char *DataGetString(Data_T &data)
{
	return data.string;
}

void DataSetStr(Data_T &data, char *string)
{
	data.string = string;
}

DrawingPanel DataGetPanel(Data_T &data)
{
	return data.panel;
}

void DataSet(Data_T &data, DrawingPanel panel)
{
	data.panel = panel;
}

void DataSet(Data_T &data, double x, double y, double z)
{
	data.values.x = x;
	data.values.y = y;
	data.values.z = z;
}

double DataGetValueX(Data_T &data)
{
	return data.values.x;
}

double DataGetValueY(Data_T &data)
{
	return data.values.y;
}

double DataGetValueZ(Data_T &data)
{
	return data.values.z;
}

TError InitPanel_A(DrawingPanel &panel, Data_T data)
{
	panel = DataGetPanel(data);
	return ErrorNoErr;
}

TError FreeModel_A(Model_T &model)
{
	TError error = FreeModel(model);
	return error;
}

TError LoadModel_A(Model_T &model, Data_T data)
{
	char *string = DataGetString(data);
	return LoadModelFromFile(model, string);
}

TError SaveModel_A(Model_T &model, Data_T data)
{
	char *string = DataGetString(data);
	return SaveModelToFile(model, string);
}

TError DrawModel_A(Model_T &model, DrawingPanel &panel)
{
	DrawModel(panel, model);
	return ErrorNoErr;
}

TError Move_A(Model_T &model, Data_T data)
{
	double dx = DataGetValueX(data);
	double dy = DataGetValueY(data);
	double dz = DataGetValueZ(data);
	
	TransfMoveModel(model, dx, dy, dz);
	return ErrorNoErr;
}

TError Rotate_A(Model_T &model, Data_T data)
{
	double anglex = DataGetValueX(data);
	double angley = DataGetValueY(data);
	double anglez = DataGetValueZ(data);

	TransfRotateModelX(model, anglex);
	TransfRotateModelY(model, angley);
	TransfRotateModelZ(model, anglez);
	return ErrorNoErr;
}

