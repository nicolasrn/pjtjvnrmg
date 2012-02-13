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

using Projet.HelperFarseerObject;

namespace Projet.Element_de_Jeu.Composites
{
    /// <summary>
    /// Objet abstrait servant de base au pattern composite
    /// </summary>
    [Serializable]
    [XmlInclude(typeof(ObjetTexture)), XmlInclude(typeof(Corde)), XmlInclude(typeof(Planche)), XmlInclude(typeof(ListeObjet)), XmlInclude(typeof(Bille)), XmlInclude(typeof(Sol)), XmlInclude(typeof(Panier))]
    public abstract class ObjetCompositeAbstrait
    {
        protected FarseerObject item;
        protected String textureName;
        protected Texture2D texture;

        [XmlIgnore]
        public FarseerObject Item
        {
            get { return item; }
            set { item = value; }
        }

        [XmlIgnore]
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

        /// <summary>
        /// constructeur par défaut initialise les rectangles avec 0 partout, texture a null, et les chaines a chaine vide
        /// </summary>
        public ObjetCompositeAbstrait()
        {
            item = null;
            texture = null;
            textureName = null;
        }

        /// <summary>
        /// constructeur par défaut initialise les rectangles avec 0 partout, texture a null, et les chaines a chaine vide
        /// </summary>
        /// <param name="TextureName">le nom de la texture</param>
        public ObjetCompositeAbstrait(String textureName)
        {
            item = null;
            texture = null;
            this.textureName = textureName;
        }
        
        /// <summary>
        /// méthode permettant d'initialiser une texture
        /// </summary>
        /// <param name="Content">Pour le chargement des texture</param>
        protected abstract void init(ContentManager Content);

        /// <summary>
        /// méthode permettant l'affichage
        /// </summary>
        /// <param name="Content">Pour le chargement des texture</param>
        protected abstract void dessin(SpriteBatch spriteBatch);

        /// <summary>
        /// méthode permettant la mise a jour des données
        /// </summary>
        protected abstract void update();

        /// <summary>
        /// Pour le chargement des images
        /// </summary>
        /// <param name="Content">pour l'acces à la méthode Load</param>
        public void Initialize(ContentManager Content)
        {
            this.init(Content);
        }

        /// <summary>
        /// Pour le dessin des images
        /// </summary>
        /// <param name="spriteBatch">pour dessinner</param>
        public void Dessin(SpriteBatch spriteBatch)
        {
            this.dessin(spriteBatch);
        }

        /// <summary>
        /// méthode permettant la mise a jout des données
        /// </summary>
        public void Update()
        {
            this.update();
        }
    }
}
