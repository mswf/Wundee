
using System.Collections.Generic;

namespace Wundee
{
	public class DefinitionLoader<TDefinition, TConcrete> where TDefinition : Definition<TConcrete>, new()
	{
		private Dictionary<string, TDefinition> _definitions;
		private DataLoader _loader;


		public DefinitionLoader(DataLoader loader)
		{
			this._loader = loader;
			_definitions = new Dictionary<string, TDefinition>();
		}

		public void AddFolder(string relativePath)
		{
			foreach (var filePath in _loader.GetAllJsonFilePaths(relativePath))
			{
				var jsonData = _loader.GetJsonDataFromFile(filePath);

				foreach (var dataKey in jsonData.Keys)
				{
					var newDefinition = new TDefinition();
					newDefinition.ParseDefinition(dataKey, jsonData[dataKey]);
					_definitions[dataKey] = newDefinition;
				}
			}
		}

		public TDefinition this[string definitionKey]
		{
			get
			{
				return _definitions[definitionKey];
			}
		}



	}
}
