

using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;

namespace Wundee
{
	public class Entity
	{
		public Body body;
		public Shape collisionShape;

		public Vector2 position
		{
			get { return body.Position; }
		}

		public Entity(World world, Vector2 startPosition)
		{
			body = new Body(world.physicsWorld, startPosition);
			collisionShape = new CircleShape(10f, 1f);
			var fixture = body.CreateFixture(collisionShape);
			
		}
	}

}

