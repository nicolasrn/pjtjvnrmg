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

namespace Projet.Element_de_Jeu
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

        public Rectangle Rect
        {
            get { return rect; }
            set { rect = value; }
        }
        /*
        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }
        */
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
        /// méthode permettant de dessiner les textures
        /// </summary>
        /// <param name="spriteBatch">Pour le dessin des textures</param>
        protected abstract void dessiner(SpriteBatch spriteBatch);

        /// <summary>
        /// méthode permettant de dessiner les textures
        /// </summary>
        /// <param name="spriteBatch">Pour le dessin des textures</param>
        /// <param name="zone">pour dessiner dans une zone contraignant rect</param>
        protected abstract void dessiner(SpriteBatch spriteBatch, Rectangle zone);

        /// <summary>
        /// Pour le chargement des images
        /// </summary>
        /// <param name="Content">pour l'acces à la méthode Load</param>
        public void Initialize(ContentManager Content)
        {
            this.init(Content);
        }

        /// <summary>
        /// Pour le dessin des textures
        /// </summary>
        /// <param name="spriteBatch">pour dessiner</param>
        public void Dessiner(SpriteBatch spriteBatch)
        {
            this.dessiner(spriteBatch);
        }

        /// <summary>
        /// Pour le dessin des textures
        /// </summary>
        /// <param name="spriteBatch">pour dessiner</param>
        /// <param name="zone">pour dessiner dans zone restreinte du rect</param>
        public void Dessiner(SpriteBatch spriteBatch, Rectangle zone)
        {
            this.dessiner(spriteBatch, zone);
        }

    }

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
        public ObjetTexture(String textureName, Rectangle rect) : base(rect)
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

        /// <summary>
        /// dessine l'objet
        /// </summary>
        /// <param name="spriteBatch">pour dessiner l'objet</param>
        protected override void dessiner(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(this.texture, rect, Color.White);
            spriteBatch.End();
        }

        /// <summary>
        /// méthode permettant de dessiner les textures
        /// </summary>
        /// <param name="spriteBatch">Pour le dessin des textures</param>
        /// <param name="zone">pour dessiner dans une zone contraignant rect</param>
        protected override void dessiner(SpriteBatch spriteBatch, Rectangle zone)
        {
            double x = Math.Floor(zone.X + rect.X / 100.0 * zone.Width);
            double y = Math.Floor(zone.Y + rect.Y / 100.0 * zone.Height);
            double w = Math.Floor(rect.Width / 100.0 * zone.Width);
            double h = Math.Floor(rect.Height / 100.0 * zone.Height);
            rectangleCourant = new Rectangle((int)x, (int)y, (int)w, (int)h);

            spriteBatch.Begin();
            spriteBatch.Draw(this.texture, rectangleCourant, Color.White);
            spriteBatch.End();
        }
    }

    /// <summary>
    /// Objet concret représentant une Corde
    /// </summary>
    [Serializable]
    public class Corde : ObjetTexture, ISelectionnable
    {
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="rect">le rectangle d'affichage</param>
        public Corde(Rectangle rect)
            : base("corde", rect)
        {
        }

        public Corde()
            : base("corde", new Rectangle(0, 0, 0, 0))
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

    /// <summary>
    /// Objet concret mettant en oeuvre le pattern Composite
    /// </summary>
    [Serializable]
    public class ListeObjet : ObjetCompositeAbstrait
    {
        private List<ObjetCompositeAbstrait> list;

        private GraphicsDeviceManager graphics;

        /// <summary>
        /// constructeur
        /// <param name="rect">le rectangle d'affichage</param>
        /// </summary>
        public ListeObjet(Rectangle rect)
            : base(rect)
        {
            list = new List<ObjetCompositeAbstrait>();
            this.graphics = null;
        }

        /// <summary>
        /// constructeur
        /// <param name="graphics">pour savoir comment dessiner le fond</param>
        /// </summary>
        public ListeObjet(GraphicsDeviceManager graphics)
            : base()
        {
            list = new List<ObjetCompositeAbstrait>();
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
                texture = Content.Load<Texture2D>("NiveauBanquise");
            foreach (ObjetCompositeAbstrait obj in list)
                obj.Initialize(Content);
        }

        /// <summary>
        /// dessine l'objet
        /// </summary>
        /// <param name="spriteBatch">pour dessiner l'objet</param>
        protected override void dessiner(SpriteBatch spriteBatch)
        {
            dessiner(spriteBatch, rect);
        }

        /// <summary>
        /// dessine l'objet
        /// </summary>
        /// <param name="spriteBatch">pour dessiner l'objet</param>
        /// <param name="zone">avec restriction de zone</param>
        protected override void dessiner(SpriteBatch spriteBatch, Rectangle zone)
        {
            if (graphics != null)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(texture, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);
                spriteBatch.End();
            }

            foreach (ObjetCompositeAbstrait obj in list)
            {
                if (zone.X == 0 && zone.Y == 0 && zone.Width == 0 && zone.Height == 0) //si l'objet que l'on traite n'a pas de zone on affiche normalement
                    obj.Dessiner(spriteBatch);
                else
                    obj.Dessiner(spriteBatch, zone);
            }
        }
    }
}
