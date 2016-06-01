

using LitJson;


namespace Wundee.Stories
{
	public abstract class StoryElement<ChildType> where ChildType : StoryElement<ChildType>
	{
		public StoryNode parentStoryNode;

		public abstract void ParseParams(JsonData parameters);


		public ChildType GetClone()
		{
			return (ChildType) MemberwiseClone();
		}
		
	}
}