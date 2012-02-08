using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet.Element_de_Jeu
{
    interface IDessiner
    {
        /// <summary>
        /// permet de charger les textures
        /// </summary>
        /// <param name="Content">pour le chargement des texture</param>
        void init(Microsoft.Xna.Framework.Content.ContentManager Content);

        /// <summary>
        /// permet de dessiner en se basant sur le SpriteBatch
        /// </summary>
        /// <param name="spriteBatch">pour dessiner la texture</param>
        void dessiner(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch);
    }
}
