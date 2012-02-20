using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

using Projet.HelperFarseerObject;
using FarseerPhysics.Dynamics.Joints;
using FarseerPhysics.Dynamics;

namespace Projet.Element_de_Jeu.Composites
{
    /// <summary>
    /// Objet concret représentant une Corde
    /// </summary>
    [Serializable]
    public class Obstacle : ObjetTexture
    {
        
        /// <summary>
        /// Constructeur
        /// </summary>
        public Obstacle(float x, float y, float width, float height)
            : base("rondin", x, y, width, height)
        {
        }

        public Obstacle()
            : base()
        {
        }

        public float BoundsX
        {
            get { return x; }
        }

        public float BoundsY
        {
            get { return y; }
        }

        public float BoundsWidth
        {
            get { return width; }
        }

        public float BoundsHeight
        {
            get { return height; }
        }

        protected override void specialisationInit()
        {
            item = new FarseerObject(
                SingletonWorld.getInstance().getWorld(),
                FarseerObject.FarseerObjectType.Box,
                x,
                y,
                width,
                height,
                new Rectangle(0, 0, 128, 128));
            item.Fixture.Body.BodyType = FarseerPhysics.Dynamics.BodyType.Static;
        }

        
    }
}
