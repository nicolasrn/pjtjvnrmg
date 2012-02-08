using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Projet.Element_de_Jeu.Composites
{
    /// <summary>
    /// Met en oeuvre les objet possédant une texture
    /// </summary>
    [Serializable]
    public abstract class ObjetTexture : ObjetCompositeAbstrait
    {
        /// <summary>
        /// Constructeur utilisant le nom de la texture
        /// </summary>
        /// <param name="textureName">le nom de la texture</param>
        public ObjetTexture(String textureName, Rectangle rect)
            : base(rect)
        {
            this.texture = null;
            this.textureName = textureName;
        }

        public ObjetTexture()
            : base(new Rectangle(0, 0, 0, 0))
        {
        }

        /// <summary>
        /// Pour le chargement des images
        /// </summary>
        /// <param name="Content">pour l'acces à la méthode Load</param>
        protected override void init(ContentManager Content)
        {
            this.texture = Content.Load<Texture2D>(this.textureName);
        }

        public override void accept(Visiteur.IVisiteurComposite visiteur, Rectangle zone)
        {
            visiteur.visit(this, (zone.X == 0 && zone.Y == 0 && zone.Width == 0 && zone.Height == 0 ? rect : zone));
        }
    }

}
