using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Projet.Element_de_Jeu.Composites
{

    [Serializable]
    public class Bille : ObjetTexture, ISelectionnable
    {
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="rect">le rectangle d'affichage</param>
        public Bille(Rectangle rect)
            : base("Ours", rect)
        {
        }

        public Bille()
            : base("Ours", new Rectangle(0, 0, 0, 0))
        {
        }

        /// <summary>
        /// propriété pour récupérer la taille de la texture affiché a l'écran 
        /// retourne un rectangle null si l'affichage proportionnel n'est pas activé
        /// </summary>
        public Rectangle Bounds
        {
            get
            {
                return RectangleCourant;
            }
        }
    }

}
