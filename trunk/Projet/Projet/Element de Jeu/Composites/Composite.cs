using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Xml.Serialization;

using Projet.FarseerObjet;

namespace Projet.Element_de_Jeu.Composites
{
    /// <summary>
    /// Objet abstrait servant de base au pattern composite
    /// </summary>
    [Serializable]
    [XmlInclude(typeof(ObjetTexture)), XmlInclude(typeof(Corde)), XmlInclude(typeof(Planche)), XmlInclude(typeof(ListeObjet)), XmlInclude(typeof(Bille))]
    public abstract class ObjetCompositeAbstrait
    {
        protected Rectangle rect;
        protected Texture2D texture;
        protected String textureName;
        protected Rectangle rectangleCourant;

        protected FarseerObject element;

        public Rectangle Rect
        {
            get { return rect; }
            set { rect = value; }
        }
        
        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }
        
        public String TextureName
        {
            get { return textureName; }
            set { textureName = value; }
        }

        public Rectangle RectangleCourant
        {
            get { return rectangleCourant; }
            set { rectangleCourant = value; }
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="rect">le rectangle englobant la zone de dessin</param>
        public ObjetCompositeAbstrait(Rectangle rect)
        {
            this.rect = rect;
            rectangleCourant = new Rectangle(0, 0, 0, 0);
        }

        /// <summary>
        /// constructeur par défaut initialise les rectangles avec 0 partout, texture a null, et les chaines a chaine vide
        /// </summary>
        public ObjetCompositeAbstrait()
        {
            this.rect = new Rectangle(0, 0, 0, 0);
            this.RectangleCourant = new Rectangle(0, 0, 0, 0);
            this.texture = null;
            this.textureName = "";
        }

        /// <summary>
        /// méthode permettant d'initialiser une texture
        /// </summary>
        /// <param name="Content">Pour le chargement des texture</param>
        protected abstract void init(ContentManager Content);

        /// <summary>
        /// met en application le pattern visiteur
        /// </summary>
        /// <param name="visiteur">l'objet qui visite</param>
        public abstract void accept(Visiteur.IVisiteurComposite visiteur, Rectangle zone);

        /// <summary>
        /// Pour le chargement des images
        /// </summary>
        /// <param name="Content">pour l'acces à la méthode Load</param>
        public void Initialize(ContentManager Content)
        {
            this.init(Content);
        }
    }
}
