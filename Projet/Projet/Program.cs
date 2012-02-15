using System;
using Projet.Jeu;

namespace Projet
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// Point d’entrée principal pour l’application.
        /// </summary>
        static void Main(string[] args)
        {
            //using (Microsoft.Xna.Framework.Game game = new Conception())
            using (Microsoft.Xna.Framework.Game game = new Conception())
            {
                game.Run();
            }
        }
    }
#endif
}

