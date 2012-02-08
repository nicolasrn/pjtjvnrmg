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

        public override void accept(Visiteur.IVisiteurComposite visiteur, Rectangle zone)
        {
            visiteur.visit(this, (zone.X == 0 && zone.Y == 0 && zone.Width == 0 && zone.Height == 0 ? rect : zone));
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

        public override void accept(Visiteur.IVisiteurComposite visiteur, Rectangle zone)
        {
            visiteur.visit(this, zone);
        }

        public void accept(Visiteur.IVisiteurComposite visiteur)
        {
            visiteur.visit(this, rect);
        }
    }
}
