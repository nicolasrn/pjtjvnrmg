using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Projet.Element_de_Jeu
{
    /// <summary>
    /// designe et permet d'implementer une méthode récupérant le rectangle sur la zone cliente
    /// </summary>
    public interface ISelectionnable
    {
        /// <summary>
        /// propriété pour récupérer la taille de la texture affiché a l'écran 
        /// retourne un rectangle null si l'affichage proportionnel n'est pas activé
        /// </summary>
        /*Rectangle Bounds
        {
            get;
        }*/
    }

    /// <summary>
    /// classe qui permet de switcher entre les éléments selectionnable
    /// </summary>
    class Selectionnable : IDessiner
    {
        private List<ISelectionnable> list;
        private ISelectionnable courant;
        private int iterateur;
        private Texture2D texture;

        /// <summary>
        /// constructeur
        /// </summary>
        public Selectionnable()
        {
            list = new List<ISelectionnable>();
            courant = null;
            iterateur = 0;
            texture = null;
        }

        public List<ISelectionnable> List
        {
            get { return list; }
            set
            {
                list = value;
                iterateur = 0;
                courant = list[0];
            }
        }

        /// <summary>
        /// ajoute un element a la liste des objets selectionnable
        /// </summary>
        /// <param name="obj">l'objet selectionnable a ajouter</param>
        public void Add(ISelectionnable obj)
        {
            list.Add(obj);
            if (courant == null)
                courant = obj;
        }

        /// <summary>
        /// met la selection sur le selectionnable suivant
        /// </summary>
        public void suivant()
        {
            iterateur = Math.Abs((iterateur + 1) % list.Count);
            courant = list[iterateur];
        }

        /// <summary>
        /// met la selection sur le selectionnable precedant
        /// </summary>
        public void precedant()
        {
            if (iterateur == 0)
                iterateur = iterateur - list.Count + 1;
            else
                iterateur = (iterateur - 1) % list.Count;
            iterateur = Math.Abs(iterateur);
            courant = list[iterateur];
        }

        /// <summary>
        /// initialise la texture selectionnable
        /// </summary>
        /// <param name="Content">pour charger les textures</param>
        public void init(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            texture = Content.Load<Texture2D>("selection");
        }

        /// <summary>
        /// dessine la texture selon ISelection.Bounds
        /// </summary>
        /// <param name="spriteBatch">pour dessiner la texture</param>
        public void dessiner(SpriteBatch spriteBatch)
        {
            //spriteBatch.Begin();
            //spriteBatch.Draw(texture, new Rectangle(courant.Bounds.X - 15, courant.Bounds.Y - 5, courant.Bounds.Width + 5, courant.Bounds.Height + 5), new Color(255, 255, 255, 50));
            //spriteBatch.End();
        }
    }
}
