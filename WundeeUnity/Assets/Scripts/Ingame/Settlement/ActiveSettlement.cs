using System.Collections.Generic;

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
