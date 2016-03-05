

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface ITickTask
{
    void Tick();

	void SetLastTickTime(long lastTickTime);
}