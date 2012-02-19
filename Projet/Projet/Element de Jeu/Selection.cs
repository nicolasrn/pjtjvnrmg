using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using FarseerPhysics.Dynamics.Joints;

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
        float BoundsX
        {
            get;
        }

        float BoundsY
        {
            get;
        }

        float BoundsWidth
        {
            get;
        }

        float BoundsHeight
        {
            get;
        }

        void Desactiver();
    }

    /// <summary>
    /// classe qui permet de switcher entre les éléments selectionnable
    /// </summary>
    class Selectionnable : IDessiner, IEnumerable<ISelectionnable>
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

        public ISelectionnable Courant
        {
            get { return courant; }
            set { courant = value; }
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
            int coeff = 1;//HelperFarseerObject.FarseerObject.PixelPerMeter;
            Rectangle rect = new Rectangle((int)(courant.BoundsX * coeff)-20, (int)(courant.BoundsY * coeff), 20, 20);/*(int)(courant.BoundsWidth * coeff), (int)(courant.BoundsHeight * coeff));*/
            spriteBatch.Draw(texture, rect, null, Color.White);
            //spriteBatch.End();
        }

        public void desactiver()
        {
            courant.Desactiver();
        }

        public IEnumerator<ISelectionnable> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }
    }
}
