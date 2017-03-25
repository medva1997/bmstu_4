#include "Gui.h"

using namespace System;
using namespace System::Windows::Forms;

void DrawLineOnPanel( MyDrawing g, double x1, double y1, double x2, double y2)
{
	System::Drawing::Pen^ pen=gcnew System::Drawing::Pen( System::Drawing::Color::Black,1.0f );
	g->DrawLine(pen,(float)x1,(float)y1,(float)x2,(float)y2);

}