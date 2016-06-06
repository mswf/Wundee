

namespace Wundee
{
	public class GameData
	{
		public static class Needs
		{
			public static string[] BaseNeeds = new string[]
			{
				//Population
				"P_Craftsmen",
				"P_Healthiness",
				"P_Willpower",

				//Goods
				"G_LivingSpaces",
				"G_Weapons",
				"G_Food",
				"G_BuildingMaterial",

				//Diplomacy
				"D_Defense",
				"D_Dominance",
				"D_Territory",

				//Satisfaction
				"S_Faith",
				"S_Prosperity",
				"S_History",
			};

			public static bool IsValidNeed(string potentialNeed)
			{
				for (int i = 0; i < BaseNeeds.Length; i++)
				{
					if (BaseNeeds[i] == potentialNeed)
						return true;
				}

				return false;
			}
		}
	}

}
