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
        protected float x, y, width, height;

        /// <summary>
        /// Constructeur utilisant le nom de la texture
        /// </summary>
        public ObjetTexture()
            : base()
        {
        }
         
        /// <summary>
        /// Constructeur utilisant le nom de la texture
        /// </summary>
        public ObjetTexture(String textureName, float x, float y, float width, float height)
            : base(textureName)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        public float X
        {
            get { return x; }
        }

        public float Y
        {
            get { return y; }
        }

        public float Width
        {
            get { return width; }
        }

        public float Height
        {
            get { return height; }
        }

        /// <summary>
        /// Pour le chargement des images
        /// </summary>
        /// <param name="Content">pour l'acces à la méthode Load</param>
        protected override void init(ContentManager Content)
        {
            this.texture = Content.Load<Texture2D>(textureName);
            this.Item.Texture = this.Texture;
        }

        protected override void dessin(SpriteBatch spriteBatch)
        {
            item.draw(spriteBatch);
        }

        protected override void update()
        {
            item.update();
        }
    }
}
