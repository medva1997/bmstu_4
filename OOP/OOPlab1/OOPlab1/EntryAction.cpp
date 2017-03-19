#include "EntryAction.h"


TError EntryAction(const Action_T &act, const Data_T &data)
{
	TError error = ErrorNoErr;
	DrawingPanel panel = InitPanel();
	Model_T model = InitModel();

	switch (act)
	{
	case ActionInit:
		error = InitPanel_A(panel, data);
		break;
	case ActionFree:
		error = FreeModel_A(model);
		break;
	case ActionLoad:
		error = LoadModel_A(model, data);
		break;
	case ActionSave:
		error = SaveModel_A(model, data);
		break;
	case ActionMove:
		error = Move_A(model, data);
		break;
	case ActionRotate:
		error = Rotate_A(model, data);
		break;
	case ActionDraw:
		error = DrawModel_A(model, panel);
		break;
	}
	return error;
}
