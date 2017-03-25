#include "Drawing.h"

DrawingPanel InitPanel()
{
	DrawingPanel canvas;
	canvas.g =nullptr;
	canvas.x_center = 0;
	canvas.y_center = 0;
	return canvas;
}

void PanelSet(DrawingPanel &canvas, MyDrawing g)
{
	canvas.g = g;
}

void PanelSet(DrawingPanel &canvas, double x_center, double y_center)
{
	canvas.x_center = x_center;
	canvas.y_center = y_center;
}

MyDrawing PanelGetCanvas(DrawingPanel &canvas)
{
	return canvas.g;
}

double PanelGetXCenter(DrawingPanel &canvas)
{
	return canvas.x_center;
}

double PanelGetYCenter(DrawingPanel &canvas)
{
	return canvas.y_center;
}

void DrawModel(DrawingPanel panel,const Model_T &model)
{
	VertexArray_T VertexArr = ModelGetVertexArray(model);
	EdgeArray_T EdgeArr = ModelGetEdgeArray(model);
	Vertex_T center_model = VertexArrayGetCenter(VertexArr);
	double x_center_model = VertexGetX(center_model);
	double y_center_model = VertexGetY(center_model);

	MyDrawing canvas = PanelGetCanvas(panel);
	double x_center_panel = PanelGetXCenter(panel);
	double y_center_panel = PanelGetYCenter(panel);

	int count = EdgeArrayGetCount(EdgeArr);
	for (int i = 0; i < count; i++)
	{
		Edge_T edge = EdgeArrayGetElement(EdgeArr, i);
		int from = EdgeGetFrom(edge);
		int to = EdgeGetTo(edge);
		Vertex_T vertex1 = VertexArrayGetElement(VertexArr, from);
		Vertex_T vertex2 = VertexArrayGetElement(VertexArr, to);
		double x01 = VertexGetX(vertex1);
		double y01 = VertexGetY(vertex1);
		double x02 = VertexGetX(vertex2);
		double y02 = VertexGetY(vertex2);
		double x1 = x_center_panel - x_center_model + x01;
		double x2 = x_center_panel - x_center_model + x02;
		double y1 = y_center_panel - y_center_model + y01;
		double y2 = y_center_panel - y_center_model + y02;
		DrawLineOnPanel(canvas, x1, y1, x2, y2);
	}
}