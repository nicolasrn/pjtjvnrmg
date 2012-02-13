using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace Projet.Time
{
    /// <summary>
    /// Classe assurant la gestion du temps
    /// </summary>
    abstract class Time
    {
        protected float time;
        protected float timeTravail;
        protected float interval;
        protected Boolean continu;

        /// <summary>
        /// Constructeur de la gestion du temps
        /// </summary>
        /// <param name="time">représente le délai que l'on souhaite attendre</param>
        /// <param name="interval">utilisé pour moduler le time mettre 0 si l'on souhaite entendre exactement time si interval > 0 l'éxécution se fera à time - interval</param>
        public Time(float time, float interval)
        {
            this.time = this.timeTravail = time;
            this.interval = interval;
            continu = true;
        }

        public void start(GameTime gameTime)
        {
            if (continu)
                run(gameTime);
        }

        /// <summary>
        /// lance l'action associé au type de Timer
        /// </summary>
        /// <param name="gameTime">Pour obtenir un temps</param>
        protected abstract void run(GameTime gameTime);

        /// <summary>
        /// propriété permettant de stopper/relancer le compteur temps
        /// </summary>
        public Boolean Continue
        {
            get { return continu; }
            set { continu = value; }
        }
    }

    /// <summary>
    /// représente un Chrono, l'action associé n'est exécuté qu'une seule fois
    /// </summary>
    abstract class Chrono : Time
    {
        /// <summary>
        /// Constructeur du Chrono
        /// </summary>
        /// <param name="time">représente le délai que l'on souhaite attendre</param>
        /// <param name="interval">utilisé pour moduler le time mettre 0 si l'on souhaite entendre exactement time si interval > 0 l'éxécution se fera à time - interval</param>
        public Chrono(float time, float interval)
            : base(time, interval)
        {

        }

        /// <summary>
        /// tous les intervalles execute la fonction execute
        /// </summary>
        /// <param name="gameTime">pour récupérer des infos relatives au temps</param>
        protected override void run(GameTime gameTime)
        {
            timeTravail -= gameTime.ElapsedGameTime.Milliseconds;
            if (timeTravail <= interval)
                execute();
        }

        /// <summary>
        /// code a executer tous les intervalles de temps (time)
        /// </summary>
        protected abstract void execute();
    }

    /// <summary>
    /// représente un Chrono, l'action associé est exécuté plusieurs fois
    /// </summary>
    abstract class Interval : Time
    {
        /// <summary>
        /// Constructeur de l'intervalle
        /// </summary>
        /// <param name="time">représente le délai que l'on souhaite attendre</param>
        /// <param name="interval">utilisé pour moduler le time mettre 0 si l'on souhaite entendre exactement time si interval > 0 l'éxécution se fera à time - interval</param>
        public Interval(float time, float interval)
            : base(time, interval)
        {

        }

        /// <summary>
        /// tous les intervalles execute la fonction execute
        /// </summary>
        /// <param name="gameTime">pour récupérer des infos relatives au temps</param>
        protected override void run(GameTime gameTime)
        {
            timeTravail -= gameTime.ElapsedGameTime.Milliseconds;
            if (timeTravail <= interval)
            {
                timeTravail = time;
                execute();
            }
        }

        /// <summary>
        /// code a executer tous les intervalles de temps (time)
        /// </summary>
        protected abstract void execute();
    }
}
