#pragma once
#include "model.h"
#include "Gui.h"
using namespace System::Windows::Forms;


struct DrawingPanel
{
	MyDrawing g;
	double x_center;
	double y_center;
};

DrawingPanel InitPanel();
void PanelSet(DrawingPanel &canvas, MyDrawing g);
void PanelSet(DrawingPanel &canvas, double x_center, double y_center);
MyDrawing PanelGetCanvas(DrawingPanel &canvas);
double PanelGetXCenter(DrawingPanel &canvas);
double PanelGetYCenter(DrawingPanel &canvas);
void DrawModel(DrawingPanel panel, const Model_T &model);