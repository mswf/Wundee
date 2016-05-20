
using System.Collections.Generic;

namespace Wundee
{
	public class DefinitionLoader<DefinitionType> where DefinitionType : IDefinition, new()
	{
		private Dictionary<string, DefinitionType> _definitions;
		private DataLoader _loader;


		public DefinitionLoader(DataLoader loader)
		{
			this._loader = loader;
			_definitions = new Dictionary<string, DefinitionType>();
		}

		public void AddFolder(string relativePath)
		{
			foreach (var filePath in _loader.GetAllJsonFilePaths(relativePath))
			{
				var jsonData = _loader.GetJsonDataFromFile(filePath);

				foreach (var dataKey in jsonData.Keys)
				{
					var newDefinition = new DefinitionType();
					newDefinition.ParseDefinition(dataKey, jsonData[dataKey]);
					_definitions[dataKey] = newDefinition;
				}
			}
		}

		public DefinitionType this[string definitionKey]
		{
			get
			{
				return _definitions[definitionKey];
			}
		}



	}
}
