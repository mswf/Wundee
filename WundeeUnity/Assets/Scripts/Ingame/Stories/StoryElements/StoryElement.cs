

using LitJson;


namespace Wundee.Stories
{
	public abstract class StoryElement<ChildType> where ChildType : StoryElement<ChildType>
	{
		public StoryElementDefinition<ChildType> definition;
		public StoryNode parentStoryNode;

		public abstract void ParseParams(JsonData parameters);

		public virtual ChildType GetClone(StoryNode parent)
		{
			this.parentStoryNode = parent;
			return (ChildType) MemberwiseClone();
		}
		
	}
}