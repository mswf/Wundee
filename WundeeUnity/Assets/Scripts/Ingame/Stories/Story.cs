﻿
using System.Collections;


namespace Wundee.Stories
{
	public class Story
	{
		public StoryDefinition definition;

		public StoryNode currentNode;


		public void Tick()
		{
			var result = currentNode.Tick();

			
		}
	}
}