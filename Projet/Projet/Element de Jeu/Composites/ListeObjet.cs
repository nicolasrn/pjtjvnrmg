﻿using System;
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

        private bool aLier;

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
            aLier = true;
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
            aLier = true;
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
            aLier = true;
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

        public Boolean ALier
        {
            get { return aLier; }
            set { aLier = value; }
        }

        public GraphicsDeviceManager Graphics
        {
            set 
            {
                graphics = value;
                this.rect = new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            }
        }

        public Rectangle Rectangle
        {
            get { return this.rect; }
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

        public void lier()
        {
            if (aLier)
            {
                if (list.Count == 3)
                {
                    Corde a = (list[0] as Corde);
                    Corde b = (list[1] as Corde);
                    Planche p = (list[2] as Planche);
                    a.Joint = JointFactory.CreateRevoluteJoint(SingletonWorld.getInstance().getWorld(),
                        a.Item.Fixture.Body,
                        p.Item.Fixture.Body,
                        new Vector2(a.X - p.X, 0));

                    b.Joint = JointFactory.CreateRevoluteJoint(SingletonWorld.getInstance().getWorld(),
                        b.Item.Fixture.Body,
                        p.Item.Fixture.Body,
                        new Vector2(b.X - p.X, 0));
                }
                else if (list.Count == 2)
                {
                    Corde a = list[0] as Corde;
                    Bille b = list[1] as Bille;

                    a.Joint = JointFactory.CreateRevoluteJoint(SingletonWorld.getInstance().getWorld(),
                        a.Item.Fixture.Body,
                        b.Item.Fixture.Body,
                        new Vector2(a.X - b.X, 0));
                }
            }
        }

        public void ignoreCollisionWith(ObjetTexture o)
        {
            if (aLier && list.Count == 3)
            {
                (list[0] as Corde).ignoreCollision(o);
                (list[1] as Corde).ignoreCollision(o);
            }
        }

        public void ignoreCollisionBetween(ObjetTexture a, ObjetTexture b)
        {
            a.Item.Fixture.Body.IgnoreCollisionWith(b.Item.Fixture.Body);
            //b.Item.Fixture.Body.IgnoreCollisionWith(a.Item.Fixture.Body);
        }

        public override void delete()
        {
            foreach (ObjetCompositeAbstrait o in list)
                o.delete();
        }

        /// <summary>
        /// Pour le chargement des images
        /// </summary>
        /// <param name="Content">pour l'acces à la méthode Load</param>
        protected override void init(ContentManager Content)
        {
            if (graphics != null)
                loadTexture(Content);

            foreach (ObjetCompositeAbstrait obj in list)
                obj.Initialize(Content);
        }

        public void loadTexture(ContentManager Content)
        {
            texture = Content.Load<Texture2D>(textureName);
        }

        protected override void dessin(SpriteBatch spriteBatch)
        {
            if (texture != null)
                spriteBatch.Draw(texture, rect, Color.White);

            foreach (ObjetCompositeAbstrait o in list)
                o.Dessin(spriteBatch);
        }

        protected override void update()
        {
            foreach (ObjetCompositeAbstrait o in list)
                o.Update();
        }

        public Bille getBille()
        {
            Bille b = null;
            foreach (ObjetCompositeAbstrait o in list)
            {
                if (b == null)
                {
                    if (o is Bille)
                        b = o as Bille;
                    else if (o is ListeObjet)
                        b = (o as ListeObjet).getBille();
                }
            }
            return b;
        }

        public Panier getPanier()
        {
            Panier b = null;
            foreach (ObjetCompositeAbstrait o in list)
            {
                if (b == null)
                {
                    if (o is Panier)
                        b = o as Panier;
                    else if (o is ListeObjet)
                        b = (o as ListeObjet).getPanier();
                }
            }
            return b;
        }

        public Sol getSol()
        {
            Sol s = null;
            foreach (ObjetCompositeAbstrait o in list)
            {
                if (s == null)
                {
                    if (o is Sol)
                        s = o as Sol;
                    else if (o is ListeObjet)
                        s = (o as ListeObjet).getSol();
                }
            }
            return s;
        }
    }
}
