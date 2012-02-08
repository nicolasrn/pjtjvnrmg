using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Projet.Element_de_Jeu.Composites;

namespace Projet.Element_de_Jeu.Visiteur
{
    class DrawVisiteurFarseer : IVisiteurComposite
    {
        private SpriteBatch spriteBatch;
        private GraphicsDeviceManager graphics;

        /// <summary>
        /// Constructeur prennant un spriteBatch pour dessiner
        /// </summary>
        /// <param name="spriteBatch">pour dessiner</param>
        /// <param name="graphics">pour récupérer des informations relatives à l'affichage de la fenetre</param>
        public DrawVisiteurFarseer(SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            this.spriteBatch = spriteBatch;
            this.graphics = graphics;
        }

        public void visit(ObjetCompositeAbstrait obj, Rectangle zone)
        {

        }

        public void visit(ObjetTexture obj, Rectangle zone)
        {
            double x = Math.Floor(zone.X + obj.Rect.X / 100.0 * zone.Width);
            double y = Math.Floor(zone.Y + obj.Rect.Y / 100.0 * zone.Height);
            double w = Math.Floor(obj.Rect.Width / 100.0 * zone.Width);
            double h = Math.Floor(obj.Rect.Height / 100.0 * zone.Height);
            obj.RectangleCourant = new Rectangle((int)x, (int)y, (int)w, (int)h);

            spriteBatch.Begin();
            spriteBatch.Draw(obj.Texture, obj.RectangleCourant, Color.White);
            spriteBatch.End();
        }

        public void visit(Corde obj, Rectangle zone)
        {
            
        }

        public void visit(Planche obj, Rectangle zone)
        {
            
        }

        public void visit(ListeObjet obj, Rectangle zone)
        {
            if (graphics != null && obj.Texture != null)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(obj.Texture, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);
                spriteBatch.End();
            }

            foreach (ObjetCompositeAbstrait o in obj.List)
            {
                if (zone.X == 0 && zone.Y == 0 && zone.Width == 0 && zone.Height == 0) //si l'objet que l'on traite n'a pas de zone on affiche normalement
                    o.accept(this, o.Rect);
                else
                    o.accept(this, zone);
            }
        }
    }
}
