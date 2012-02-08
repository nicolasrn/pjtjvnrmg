using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Projet.Element_de_Jeu.Visiteur
{
    public interface IVisiteurComposite
    {
        /// <summary>
        /// visite la classe la plus abstraite
        /// </summary>
        /// <param name="obj">l'objet a visiter</param>
        /// <param name="zone">zone de restriction</param>
        void visit(ObjetCompositeAbstrait obj, Rectangle zone);

        /// <summary>
        /// visite la classe mere de corde et planche
        /// </summary>
        /// <param name="obj">l'objet a visiter</param>
        /// <param name="zone">zone de restriction</param>
        void visit(ObjetTexture obj, Rectangle zone);

        /// <summary>
        /// visite la Corde
        /// </summary>
        /// <param name="obj">l'objet a visiter</param>
        /// <param name="zone">zone de restriction</param>
        void visit(Corde obj, Rectangle zone);

        /// <summary>
        /// visite la Planche
        /// </summary>
        /// <param name="obj">l'objet a visiter</param>
        /// <param name="zone">zone de restriction</param>
        void visit(Planche obj, Rectangle zone);

        /// <summary>
        /// visite la liste d'objet
        /// </summary>
        /// <param name="obj">l'objet a visiter</param>
        /// <param name="zone">zone de restriction</param>
        void visit(ListeObjet obj, Rectangle zone);
    }
}
