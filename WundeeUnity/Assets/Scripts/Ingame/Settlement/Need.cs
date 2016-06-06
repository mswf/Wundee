namespace Wundee
{
	[System.Serializable]
	public class Need
	{
		public readonly Settlement owner;
		public readonly string type;


		public double amount = 50d;

		public Need(Settlement owner, string type)
		{
			this.owner = owner;
			this.type = type;
		}
	}
}