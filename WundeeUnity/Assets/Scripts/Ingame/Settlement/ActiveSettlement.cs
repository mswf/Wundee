using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wundee
{
	public class ActiveSettlement
	{
		private List<Person> _persons;
		private Settlement _settlement;

		public ActiveSettlement(Settlement settlement)
		{
			this._settlement = settlement;

			this._persons = new List<Person>();
		}

		public void Tick(double deltaTime)
		{
			
		}
	}
}
