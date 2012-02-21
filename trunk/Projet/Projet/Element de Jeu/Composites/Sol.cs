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
    public class Sol : ObjetTexture
    {
        /// <summary>
        /// Constructeur
        /// </summary>
        public Sol(float x, float y, float width, float height)
            : base("chargement", x, y, width, height)
        {
        }

        public Sol()
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
                new Rectangle(0, 0, 442, 65));
            item.Fixture.Body.BodyType = FarseerPhysics.Dynamics.BodyType.Static;
        }

        protected override void dessin(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            item.draw(spriteBatch, Color.White);
        }
    }
}
