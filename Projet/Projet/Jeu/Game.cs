using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using Projet.Element_de_Jeu;
using Projet.Time;
using System.IO;

using Projet.HelperFarseerObject;
using FarseerPhysics.Dynamics;

using Projet.Element_de_Jeu.Composites;
using GameStateManagement;

namespace Projet.Jeu
{
    /// <summary>
    /// Type principal pour votre jeu
    /// </summary>
    public class Game : Microsoft.Xna.Framework.Game
    {
        public GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        
        #region déclaration ScreenManager
        ScreenManager screenManager;
        #endregion

        /*private Level level;
        private int levelCourant;
        private List<String> listLevel;

        private SpriteFont font;

        private int time = 1000;
        private int timeTravail;*/

        static readonly string[] preloadAssets =
        {
            "gradient",
        };

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            #region ScreenManager
            //*
            //Create the screen manager component.
            screenManager = new ScreenManager(this);

            Components.Add(screenManager);

            // Activate the first screens.
            screenManager.AddScreen(new BackgroundScreen(), null);
            screenManager.AddScreen(new MainMenuScreen(), null);
            
            //*/
            #endregion
        }

        /// <summary>
        /// Permet au jeu de s’initialiser avant le démarrage.
        /// Emplacement pour la demande de services nécessaires et le chargement de contenu
        /// non graphique. Calling base.Initialize passe en revue les composants
        /// et les initialise.
        /// </summary>
        protected override void Initialize()
        {
            // TODO : ajouter la logique d’initialisation ici
            graphics.PreferredBackBufferHeight = 700;
            graphics.PreferredBackBufferWidth = 600;
            graphics.ApplyChanges();

            //SingletonWorld.getInstance().getWorld();
            //timeTravail = time;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent est appelé une fois par partie. Emplacement de chargement
        /// de tout votre contenu.
        /// </summary>
        protected override void LoadContent()
        {
            // Créer un SpriteBatch, qui peut être utilisé pour dessiner des textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO : utiliser this.Content pour charger le contenu de jeu ici
            foreach (string asset in preloadAssets)
            {
                Content.Load<object>(asset);
            }

            /*listLevel = new List<String>();
            levelCourant = 0;

            foreach (String ligne in File.ReadLines("levels.txt"))
                listLevel.Add(ligne);

            level = new Level(listLevel[levelCourant]);

            level.LoadContent(Content, graphics);
            font = Content.Load<SpriteFont>("gamefont");*/
        }

        /// <summary>
        /// UnloadContent est appelé une fois par partie. Emplacement de déchargement
        /// de tout votre contenu.
        /// </summary>
        protected override void UnloadContent()
        {

        }

        /// <summary>
        /// Permet au jeu d’exécuter la logique de mise à jour du monde,
        /// de vérifier les collisions, de gérer les entrées et de lire l’audio.
        /// </summary>
        /// <param name="gameTime">Fournit un aperçu des valeurs de temps.</param>
        protected override void Update(GameTime gameTime)
        {
            // Permet au jeu de se fermer
            /*if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            level.Update(gameTime);

            if (level.Etat == Etat.VICTOIRE)
            {
                //changement de niveau 
                timeTravail -= gameTime.ElapsedGameTime.Milliseconds;
                if (timeTravail <= 0)
                {
                    level.delete();
                    levelCourant = (levelCourant + 1) % listLevel.Count;
                    level = new Level(listLevel[levelCourant]);
                    level.LoadContent(Content, graphics);
                    timeTravail = time;
                }
            }
            else if (level.Etat == Etat.DEFAITE)
            {
                timeTravail -= gameTime.ElapsedGameTime.Milliseconds;
                if (timeTravail <= 0)
                {
                    //relance du jeu ou arrêt bref quelque chose
                    level.delete();
                    level = new Level(listLevel[levelCourant]);
                    level.LoadContent(Content, graphics);
                    timeTravail = time;
                }
            }*/

            base.Update(gameTime);
        }

        /// <summary>
        /// Appelé quand le jeu doit se dessiner.
        /// </summary>
        /// <param name="gameTime">Fournit un aperçu des valeurs de temps.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO : ajouter le code de dessin ici
            /*
            spriteBatch.Begin();

            level.Draw(gameTime, spriteBatch);

            if (level.Etat == Etat.VICTOIRE)
                spriteBatch.DrawString(font, "Good Game", new Vector2(150, 250), Color.Red);
            else if (level.Etat == Etat.DEFAITE)
                spriteBatch.DrawString(font, "Game Over", new Vector2(150, 250), Color.Red);
            else if (level.Etat == Etat.ENCOURS)
                spriteBatch.DrawString(font, "Encours", new Vector2(150, 250), Color.Red);

            spriteBatch.End();
            */
            base.Draw(gameTime);
        }

        
    }
}
