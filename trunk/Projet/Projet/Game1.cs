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
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Collision.Shapes;

using Projet.Element_de_Jeu.Composites;
using FarseerPhysics.Dynamics.Joints;
using FarseerPhysics.Factories;

namespace Projet
{
    /// <summary>
    /// Type principal pour votre jeu
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        
        #region variable de conception de niveau
        private ListeObjet obja, objb, objc, lBille;
        #endregion

        #region variable permanante
        private BarreDeChargement barre;
        private ListeObjet listeObjet;
        private Selectionnable selectionnable;
        private Boolean victoireDelai, victoireCollision, victoireNiveau;
        #endregion

        #region controlleur de clavier
        private KeyboardState lastKeyboardState;
        private KeyboardState currentKeyboardState;
        #endregion

        #region déclaration ScreenManager
        //ScreenManager screenManager;
        #endregion

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            #region ScreenManager
            /*
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

            SingletonWorld.getInstance().getWorld();

            barre = new BarreDeChargement();
            selectionnable = new Selectionnable();
            victoireDelai = false;
            victoireCollision = false;
            victoireNiveau = false;

            #region conception de niveau
            /*
            Corde b, c;
            ObjetTexture d;
            Bille bille;

            lBille = new ListeObjet(Rectangle.Empty);
            lBille.Add(b = new Corde(1.5f, 0.10f, 0.25f, 1.25f));
            lBille.Add(d = (bille = new Bille(1.5f, 0.5f, 0.5f, 0.5f)));
            selectionnable.Add(b);
            
            obja = new ListeObjet(Rectangle.Empty);
            obja.Add(b = new Corde(1.10f, 0.75f, 0.25f, 1f)); 
            obja.Add(c = new Corde(2.95f, 0.75f, 0.25f, 1f)); 
            obja.Add(d = new Planche(2f, 1.38f, 2f, 0.25f));

            selectionnable.Add(b);
            selectionnable.Add(c);

            objb = new ListeObjet(Rectangle.Empty);
            
            objb.Add(b = new Corde(1.10f+1, 0.75f+2, 0.25f, 1f));
            objb.Add(c = new Corde(2.95f+1, 0.75f+2, 0.25f, 1f));
            objb.Add(d = new Planche(2f+1, 1.38f+2, 2f, 0.25f));

            selectionnable.Add(b);
            selectionnable.Add(c);

            objc = new ListeObjet(Rectangle.Empty);
            objc.ALier = false;
            objc.Add(new Sol(0, (graphics.PreferredBackBufferHeight - 37.5f) / FarseerObject.PixelPerMeter, 24, 1.5f));
            objc.Add(new Sol(0f, 6f, 0.5f, graphics.PreferredBackBufferHeight / FarseerObject.PixelPerMeter));
            objc.Add(new Sol(graphics.PreferredBackBufferWidth/FarseerObject.PixelPerMeter, 6f, 0.5f, graphics.PreferredBackBufferHeight / FarseerObject.PixelPerMeter));
            objc.Add(
                        new Panier
                        (
                            graphics.PreferredBackBufferWidth/FarseerObject.PixelPerMeter/1.8f, 
                            (graphics.PreferredBackBufferHeight-300)/FarseerObject.PixelPerMeter,
                            2, 
                            0.3f
                        )
                    );
            //ici c'est la racine pour éviter le redimensionnement on utilise le constructeur qui définit un rectangle null (toutes les valeurs sont à 0)
            listeObjet = new ListeObjet("NiveauBanquise", graphics);

            listeObjet.Add(objb);
            listeObjet.Add(obja);
            listeObjet.Add(lBille);
            listeObjet.Add(objc);
            
            listeObjet.Initialize(Content);
            foreach (ObjetCompositeAbstrait o in listeObjet.List)
                (o as ListeObjet).lier();
            foreach (ObjetCompositeAbstrait o in listeObjet.List)
                (o as ListeObjet).ignoreCollisionWith(bille);
            //listeObjet.ignoreCollisionBetween(listeObjet.getPanier(), listeObjet.getBille());

            listeObjet.getBille().Item.Fixture.Body.OnCollision += new OnCollisionEventHandler(OnCollisionDetectVictory);

            saveLevel("test");
            //*/
            #endregion
            
            base.Initialize();
        }

        /// <summary>
        /// détecte la collision de victoire
        /// </summary>
        /// <param name="fixtureA">le premier corps</param>
        /// <param name="fixtureB">le second corps</param>
        /// <param name="contact">aucune idée</param>
        /// <returns>true : on ignore aucune collision à se niveau</returns>
        bool OnCollisionDetectVictory(Fixture fixtureA, Fixture fixtureB, FarseerPhysics.Dynamics.Contacts.Contact contact)
        {
            try
            {
                if ((Boolean)fixtureA.UserData && (Boolean)fixtureB.UserData)
                    victoireCollision = true;
            }
            catch(Exception e)
            {
                Console.Out.WriteLine(e);
            }
            return true;
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
            barre.init(Content);
            loadLevel("test");
            selectionnable.init(Content);
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();
            
            lastKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            if (lastKeyboardState.IsKeyUp(Keys.Right) && currentKeyboardState.IsKeyDown(Keys.Right))
                selectionnable.suivant();
            if (lastKeyboardState.IsKeyUp(Keys.Left) && currentKeyboardState.IsKeyDown(Keys.Left))
                selectionnable.precedant();
            if (lastKeyboardState.IsKeyUp(Keys.Space) && currentKeyboardState.IsKeyDown(Keys.Space))
                selectionnable.desactiver();

            SingletonWorld.getInstance().getWorld().Step((float)gameTime.ElapsedGameTime.TotalSeconds);
            
            listeObjet.Update();
            barre.run(gameTime);

            victoireDelai = !barre.TimeOver;
            if (victoireDelai && victoireCollision)
            {
                victoireNiveau = true;
                barre.Stop();
            }

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

            spriteBatch.Begin();
            
            listeObjet.Dessin(spriteBatch);
            selectionnable.dessiner(spriteBatch);
            barre.dessiner(spriteBatch);
            
            if (victoireNiveau)
            {
                spriteBatch.DrawString(this.Content.Load<SpriteFont>("gamefont"), "Good Game", new Vector2(150, 250), Color.Red);
                //nextLevel if it's not the last
            }
            else if (!victoireDelai)
            {
                spriteBatch.DrawString(this.Content.Load<SpriteFont>("gamefont"), "Game Over", new Vector2(150, 250), Color.Red);
                //restart for the biginning or quit
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// permet la sauvegarde de niveau en mode conception
        /// </summary>
        /// <param name="name">nom du fichier sans l'extension</param>
        private void saveLevel(String name)
        {
            using (StreamWriter wr = new StreamWriter(name + ".xml"))
            {
                try
                {
                    System.Xml.Serialization.XmlSerializer serialiser = new System.Xml.Serialization.XmlSerializer(typeof(ListeObjet));
                    serialiser.Serialize(wr, listeObjet);
                }
                catch (Exception e)
                {
                    Console.Out.WriteLine(e);
                }
            }
        }

        /// <summary>
        /// permet le chargement de niveau
        /// </summary>
        /// <param name="name">nom du fichier sans l'extension</param>
        private void loadLevel(String name)
        {
            using (StreamReader rd = new StreamReader(name + ".xml"))
            {
                try
                {
                    System.Xml.Serialization.XmlSerializer serialiser = new System.Xml.Serialization.XmlSerializer(typeof(ListeObjet));
                    listeObjet = serialiser.Deserialize(rd) as ListeObjet;
                    listeObjet.Graphics = graphics;
                    listeObjet.Initialize(Content);

                    foreach (ObjetCompositeAbstrait o in listeObjet.List)
                        (o as ListeObjet).lier();
                    Bille b = listeObjet.getBille();
                    foreach (ObjetCompositeAbstrait o in listeObjet.List)
                        (o as ListeObjet).ignoreCollisionWith(b);

                    List<ISelectionnable> tmp = new List<ISelectionnable>();
                    listeObjet.getSelectionnable(tmp);
                    selectionnable.List = tmp;

                    listeObjet.getBille().Detail();
                    listeObjet.getPanier().Detail();
                    listeObjet.getBille().Item.Fixture.Body.OnCollision += new OnCollisionEventHandler(OnCollisionDetectVictory);

                    victoireCollision = false;
                    victoireDelai = false;
                    victoireNiveau = false;
                }
                catch (Exception e)
                {
                    Console.Out.WriteLine(e);
                }
            }
        }
    }
}
