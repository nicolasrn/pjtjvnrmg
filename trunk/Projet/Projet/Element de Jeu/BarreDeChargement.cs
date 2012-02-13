using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Projet.Time;

namespace Projet.Element_de_Jeu
{

    /// <summary>
    /// La classe qui gère la progression de la barre de temps
    /// </summary>
    class ChargementBarre : Interval
    {
        private int width;

        /// <summary>
        /// Constructeur du Chargement de la barre
        /// </summary>
        /// <param name="time">représente le délai que l'on souhaite attendre</param>
        /// <param name="width">la valeur initial de la width</param>
        public ChargementBarre(float time, int width)
            : base(time, 0)
        {
            this.width = width;
        }

        /// <summary>
        /// le code executé tous les intervalles de temps (time)
        /// </summary>
        protected override void execute()
        {
            if (width > 0)
                width -= 1;
        }

        /// <summary>
        /// propriété de la width
        /// </summary>
        public int Width
        {
            get
            {
                return width;
            }
            set
            {
                width = value;
            }
        }

        public void stop()
        {
            continu = false;
        }

        public void start()
        {
            continu = true;
        }
    }

    /// <summary>
    /// représente la barre de chargement
    /// </summary>
    class BarreDeChargement : IDessiner
    {
        private Texture2D barre;

        private ChargementBarre chargementBar;

        /// <summary>
        /// constructeur
        /// </summary>
        public BarreDeChargement()
        {
            chargementBar = new ChargementBarre(100, 600);
        }

        /// <summary>
        /// permet de lancer l'animation du temps
        /// </summary>
        /// <param name="gameTime"></param>
        public void run(GameTime gameTime)
        {
            chargementBar.start(gameTime);
        }

        /// <summary>
        /// permet de charger les textures
        /// </summary>
        /// <param name="Content">pour le chargement des texture</param>
        public void init(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            barre = Content.Load<Texture2D>("chargement");
        }

        /// <summary>
        /// permet de dessiner
        /// </summary>
        /// <param name="spriteBatch">le SpriteBatch qui permet de dessiner</param>
        public void dessiner(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.barre, new Rectangle(0, 0, chargementBar.Width, 20), Color.White);
        }

        public Boolean TimeOver
        {
            get 
            {
                return chargementBar.Width <= 0;
            }
        }

        public void Stop()
        {
            chargementBar.stop();
        }

        public void reStart()
        {
            chargementBar.start();
        }
    }
}
