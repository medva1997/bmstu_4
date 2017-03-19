#pragma once
#include "model.h"
#include "MyForm.h"

struct DrawingPanel
{
	MyCanvas panel;
	double x_center;
	double y_center;
};

DrawingPanel InitPanel();
void PanelSet(DrawingPanel &canvas, MyCanvas panel);
void PanelSet(DrawingPanel &canvas, double x_center, double y_center);
MyCanvas PanelGetCanvas(DrawingPanel &canvas);
int PanelGetXCenter(DrawingPanel &canvas);
int PanelGetYCenter(DrawingPanel &canvas);
void DrawModel(DrawingPanel panel, const Model_T &model);