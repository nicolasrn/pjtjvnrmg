using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Dynamics.Joints;
using FarseerPhysics.Factories;
using Projet.HelperFarseerObject;

namespace Projet.Element_de_Jeu.Composites
{
    /// <summary>
    /// Objet concret mettant en oeuvre le pattern Composite
    /// </summary>
    [Serializable]
    public class ListeObjet : ObjetCompositeAbstrait
    {
        private List<ObjetCompositeAbstrait> list;

        private GraphicsDeviceManager graphics;

        private Rectangle rect;

        /// <summary>
        /// constructeur
        /// <param name="rect">le rectangle d'affichage</param>
        /// </summary>
        public ListeObjet(Rectangle rect, String textureName = null, GraphicsDeviceManager graphics = null)
            : base(textureName)
        {
            list = new List<ObjetCompositeAbstrait>();
            this.graphics = graphics;
            this.rect = rect;
        }

        /// <summary>
        /// constructeur
        /// <param name="rect">le rectangle d'affichage</param>
        /// </summary>
        public ListeObjet(String textureName, GraphicsDeviceManager graphics)
            : base(textureName)
        {
            list = new List<ObjetCompositeAbstrait>();
            this.rect = new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            this.graphics = graphics;
        }

        /// <summary>
        /// constructeur
        /// <param name="graphics">pour savoir comment dessiner le fond</param>
        /// </summary>
        public ListeObjet()
            : base()
        {
            list = null;
            this.graphics = null;
            rect = Rectangle.Empty;
        }

        /*[XmlArrayItem("ObjetCompositeAbstrait", typeof(ObjetCompositeAbstrait))]
        [XmlArrayItem("ObjetTexture", typeof(ObjetTexture))]
        [XmlArrayItem("Corde", typeof(Corde))]
        [XmlArrayItem("Planche", typeof(Planche))]*/
        public List<ObjetCompositeAbstrait> List
        {
            get { return list; }
            set { list = value; }
        }

        public GraphicsDeviceManager Graphics
        {
            set { graphics = value; }
        }

        /// <summary>
        /// ajoute un élément a dessiner
        /// </summary>
        /// <param name="obj">l'objet a ajouter</param>
        public void Add(ObjetCompositeAbstrait obj)
        {
            list.Add(obj);
        }

        public void getSelectionnable(List<ISelectionnable> listS)
        {
            foreach (ObjetCompositeAbstrait obj in list)
                if (obj is ISelectionnable)
                    listS.Add((ISelectionnable)obj);
                else if (obj is ListeObjet)
                    ((ListeObjet)obj).getSelectionnable(listS);
        }

        /// <summary>
        /// Pour le chargement des images
        /// </summary>
        /// <param name="Content">pour l'acces à la méthode Load</param>
        protected override void init(ContentManager Content)
        {
            if (graphics != null)
                texture = Content.Load<Texture2D>(textureName);

            foreach (ObjetCompositeAbstrait obj in list)
                obj.Initialize(Content);
        }

        protected override void dessin(SpriteBatch spriteBatch)
        {
            if (graphics != null)
                spriteBatch.Draw(texture, rect, Color.White);

            foreach (ObjetCompositeAbstrait o in list)
                o.Dessin(spriteBatch);
        }

        protected override void update()
        {
            foreach (ObjetCompositeAbstrait o in list)
                o.Update();
        }
    }
}
