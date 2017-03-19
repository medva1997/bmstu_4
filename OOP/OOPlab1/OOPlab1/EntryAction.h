#pragma once
#include "errors.h"
#include "actions.h"
#include "model.h"
#pragma warning( disable : 4430 )

enum Action_T
{
	ActionInit,
	ActionFree,
	ActionLoad,
	ActionSave,
	ActionMove,
	ActionRotate,
	ActionDraw
};

TError EntryAction(const Action_T &act, const Data_T &data);