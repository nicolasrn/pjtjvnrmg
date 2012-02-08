using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

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
        /// <param name="rect">le rectangle d'affichage</param>
        public Planche(Rectangle rect)
            : base("planche", rect)
        {
        }

        public Planche()
            : base("planche", new Rectangle(0, 0, 0, 0))
        {
        }
    }

}
