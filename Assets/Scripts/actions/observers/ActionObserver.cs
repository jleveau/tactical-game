using System;

public interface IActionObserver
{

	void NotifyActionStarted(Unit unit, Action action);

	void NotifyActionFinished(Unit unit, Action action);

}
