using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Factories;
using FarseerPhysics.Dynamics.Contacts;

namespace Projet.HelperFarseerObject
{
    public class FarseerObject
    {
        public enum FarseerObjectType
        {
            Box,
            Ball
        }

        private static int pixelPerMeter = 50;

        private Fixture fixture;
        private Body body;

        private Rectangle destinationRectangle;
        private Texture2D texture;
        private Rectangle sourceRectangle;

        public FarseerObject(World world, FarseerObjectType type, float x, float y, float width, float height, Rectangle sourceRectangle)
        {
            body = BodyFactory.CreateBody(world, new Vector2(x, y));
            body.BodyType = BodyType.Dynamic;
            
            destinationRectangle = new Rectangle(0, 0, (int)(width * pixelPerMeter), (int)(height * pixelPerMeter));
            destinationRectangle.X = (int)(body.Position.X * pixelPerMeter) - destinationRectangle.Width / 2;
            destinationRectangle.Y = (int)(body.Position.Y * pixelPerMeter) - destinationRectangle.Height / 2;

            this.sourceRectangle = sourceRectangle;

            if (type == FarseerObjectType.Box)
            {
                fixture = FixtureFactory.AttachRectangle(width,
                    height,
                    1,
                    Vector2.Zero,
                    body);
            }
            else if (type == FarseerObjectType.Ball)
            {
                fixture = FixtureFactory.AttachCircle(width / 2.0f,
                    1,
                    body);
            }
        }

        public Fixture Fixture
        {
            get { return fixture; }
        }

        public Rectangle DestinationRectangle
        {
            get { return destinationRectangle; }
        }

        public static int PixelPerMeter
        {
            get { return pixelPerMeter; }
            set { pixelPerMeter = value; }
        }

        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        /*public Body Body
        {
            get { return body; }
        }*/

        public void update()
        {
            destinationRectangle.X = (int)(fixture.Body.Position.X * pixelPerMeter) - destinationRectangle.Width / 2;
            destinationRectangle.Y = (int)(fixture.Body.Position.Y * pixelPerMeter) - destinationRectangle.Height / 2;
        }

        public void draw(SpriteBatch spriteBatch)
        {
            Rectangle boxRotated = new Rectangle(destinationRectangle.Center.X,
                destinationRectangle.Center.Y,
                destinationRectangle.Width,
                destinationRectangle.Height);
            spriteBatch.Draw
            (
                texture, 
                boxRotated, 
                null,
                Color.White,
                fixture.Body.Rotation,
                new Vector2(sourceRectangle.X+sourceRectangle.Width/2,
                    sourceRectangle.Y + sourceRectangle.Height/2),
                    SpriteEffects.None, 0
            );
        }
    }
}
