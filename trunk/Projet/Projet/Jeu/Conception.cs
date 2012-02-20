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

namespace Projet.Jeu
{
    /// <summary>
    /// Type principal pour votre jeu
    /// </summary>
    public class Conception : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        
        #region variable de conception de niveau
        private ListeObjet obja, objb, objc, objd, obje, objf, objg, objh, obji, objj, objk, objl, objm, objn, objo, lBille;
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

        public Conception()
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
            //balle + corde
            Corde b, c;
            ObjetTexture d;
            Bille bille;

            lBille = new ListeObjet(Rectangle.Empty);
            lBille.Add(b = new Corde(6f, 1f, 0.25f, 1.25f));
            lBille.Add(d = (bille = new Bille(6f, 1.5f, 1, 1)));
            selectionnable.Add(b);
           
            //1
            obja = new ListeObjet(Rectangle.Empty);
            obja.Add(b = new Corde(4.1f, 3f, 0.25f, 1f));
            obja.Add(c = new Corde(7.95f, 3f, 0.25f, 1f));
            obja.Add(d = new Planche(6f, 3.5f, 4f, 0.25f));

            selectionnable.Add(b);
            selectionnable.Add(c);

            //2
            objb = new ListeObjet(Rectangle.Empty);
            objb.Add(b = new Corde(3.9f, 5f, 0.25f, 1f));
            objb.Add(c = new Corde(8.15f, 5f, 0.25f, 1f));
            objb.Add(d = new Planche(6f, 5.5f, 4.4f, 0.25f));

            selectionnable.Add(b);
            selectionnable.Add(c);

            //3
            obje = new ListeObjet(Rectangle.Empty);
            obje.Add(b = new Corde(3.7f, 7f, 0.25f, 1f));
            obje.Add(c = new Corde(8.35f, 7f, 0.25f, 1f));
            obje.Add(d = new Planche(6f, 7.5f, 4.8f, 0.25f));

            selectionnable.Add(b);
            selectionnable.Add(c);

            objc = new ListeObjet(Rectangle.Empty);
            objc.ALier = false;
            objc.Add(new Sol(0, (graphics.PreferredBackBufferHeight) / FarseerObject.PixelPerMeter, 24, 1.5f));
            objc.Add(new Sol(0f, 6f, 0.5f, graphics.PreferredBackBufferHeight / FarseerObject.PixelPerMeter));
            objc.Add(new Sol(graphics.PreferredBackBufferWidth/FarseerObject.PixelPerMeter, 6f, 0.5f, graphics.PreferredBackBufferHeight / FarseerObject.PixelPerMeter));
            objc.Add(
                        new Panier
                        (
                            6f, 
                            13.5f,
                            2, 
                            0.5f
                        )
                    );



            objc.Add(new Obstacle(5f, 12.5f, 0.25f, 1f));
            objc.Add(new Obstacle(7f, 12.5f, 0.25f, 1f));
            //ici c'est la racine pour éviter le redimensionnement on utilise le constructeur qui définit un rectangle null (toutes les valeurs sont à 0)
            listeObjet = new ListeObjet("NiveauMer", graphics);

            listeObjet.Add(obja);
            listeObjet.Add(objb);
            listeObjet.Add(obje);
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
        /// LoadContent est appelé une fois par partie. Emplacement de chargement
        /// de tout votre contenu.
        /// </summary>
        protected override void LoadContent()
        {
            // Créer un SpriteBatch, qui peut être utilisé pour dessiner des textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO : utiliser this.Content pour charger le contenu de jeu ici
            barre.init(Content);
            //loadLevel("test");
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || 
                Keyboard.GetState().IsKeyDown(Keys.Escape))
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
            catch (Exception e)
            {
                Console.Out.WriteLine(e);
            }
            return true;
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
