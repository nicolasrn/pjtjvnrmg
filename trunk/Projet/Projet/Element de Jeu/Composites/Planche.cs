using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Projet.HelperFarseerObject;
using FarseerPhysics.Dynamics;

namespace Projet.Element_de_Jeu.Composites
{
    /// <summary>
    /// Objet concret représentant une Planche
    /// </summary>
    [Serializable]
    public class Planche : ObjetTexture
    {
        /// <summary>
        /// Constructeur
        /// </summary>
        public Planche(float x, float y, float width, float height)
            : base("planche", x, y, width, height)
        {
        }

        public Planche()
            : base()
        {
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
                new Rectangle(0, 0, 48, 7));
            //item.Fixture.Body.BodyType = FarseerPhysics.Dynamics.BodyType.Dynamic;
        }
    }
}
