#pragma once
//#include "MyForm.h"
typedef  System::Windows::Forms::Panel MyCanvas;
typedef System::Drawing::Graphics^ MyDrawing;

void DrawLineOnPanel(MyDrawing g, double x1, double y1, double x2, double y2);
