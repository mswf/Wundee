

using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;

namespace Wundee
{
	public class Entity
	{
		protected World world;
		public Body body;
		public Shape collisionShape;

		public Vector2 position
		{
			get { return body.Position; }
		}

		public Entity(World world, Vector2 startPosition)
		{
			this.world = world;

			body = new Body(world.physicsWorld, startPosition);
			body.BodyType = BodyType.Dynamic;
			collisionShape = new CircleShape(30f, 100f);
			var fixture = body.CreateFixture(collisionShape);
			
		}

		public virtual void LateUpdate()
		{
			var pos = body.Position;
			var worldBounds = world.worldBounds;

			if (worldBounds.Contains(pos) == false)
			{
				var newPos = body.Position;

				if (pos.X + float.Epsilon > worldBounds.Right)
				{
					newPos.X -= worldBounds.Width;
				}
				else if (pos.X - float.Epsilon < worldBounds.Left)
				{
					newPos.X += worldBounds.Width;
				}


				if (pos.Y + float.Epsilon > worldBounds.Top)
				{
					newPos.Y -= worldBounds.Height;
				}
				else if (pos.Y - float.Epsilon < worldBounds.Bottom)
				{
					newPos.Y += worldBounds.Height;
				}

				body.SetTransform(newPos, body.Rotation);
			}
		}
	}

}

