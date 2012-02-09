using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

using Projet.HelperFarseerObject;
using FarseerPhysics.Dynamics.Joints;

namespace Projet.Element_de_Jeu.Composites
{
    /// <summary>
    /// Objet concret représentant une Corde
    /// </summary>
    [Serializable]
    public class Corde : ObjetTexture
    {
        private RevoluteJoint joint;

        /// <summary>
        /// Constructeur
        /// </summary>
        public Corde(float x, float y, float width, float height)
            : base("corde")
        {
            item = new FarseerObject(
                SingletonWorld.getInstance().getWorld(),
                FarseerObject.FarseerObjectType.Box,
                x,
                y,
                width,
                height,
                new Rectangle(0, 0, 38, 400));
            item.Fixture.Body.BodyType = FarseerPhysics.Dynamics.BodyType.Static;
        }

        public RevoluteJoint Joint
        {
            get { return joint; }
            set { joint = value; }
        }
    }
}
