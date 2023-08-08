using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debug
{
    class LVCMOS
    {
		public string rowName;
		public int min;
		public int max;

		public LVCMOS(string rowName, int min, int max)
		{
			this.rowName = rowName;
			this.min = min;
			this.max = max;
		}
	}

}
