using System.Collections.Generic;

namespace Wundee
{
	public class ActiveSettlement
	{
		private Settlement _settlement;

		private List<Person> _persons;

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
