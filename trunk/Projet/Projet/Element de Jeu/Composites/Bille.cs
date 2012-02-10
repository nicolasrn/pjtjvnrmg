using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Projet.HelperFarseerObject;
using FarseerPhysics.Dynamics;

namespace Projet.Element_de_Jeu.Composites
{

    [Serializable]
    public class Bille : ObjetTexture
    {
        /// <summary>
        /// Constructeur
        /// </summary>
        public Bille(float x, float y, float width, float height)
            : base("Ours", x, y, width, height)
        {
            item = new FarseerObject(
                SingletonWorld.getInstance().getWorld(),
                FarseerObject.FarseerObjectType.Box,
                x,
                y,
                width,
                height,
                new Rectangle(0, 0, 300, 300));
            //item.Fixture.Body.BodyType = FarseerPhysics.Dynamics.BodyType.Static;
        }
    }
}
