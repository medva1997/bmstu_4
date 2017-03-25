#pragma once
#include "actions.h"

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