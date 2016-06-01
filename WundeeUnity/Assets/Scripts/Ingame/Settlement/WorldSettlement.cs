namespace Wundee
{
	// High level simulation of settlement


	// Contains the current standing of the Settlement and potential Trends for a 
	// ActiveSettlement to represent
	public class WorldSettlement
	{
		private Settlement _settlement;

		public WorldSettlement(Settlement settlement)
		{
			this._settlement = settlement;
		}

		public void Tick(double deltaTime)
		{
		}
	}
}