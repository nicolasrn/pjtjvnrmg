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

using GameStateManagement;
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

        private BarreDeChargement barre;
        private ListeObjet listeObjet, obja, objb, lBille;

        private Selectionnable selectionnable;

        private KeyboardState lastKeyboardState;
        private KeyboardState currentKeyboardState;

        //ScreenManager screenManager;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Create the screen manager component.
            //screenManager = new ScreenManager(this);

            //Components.Add(screenManager);

            // Activate the first screens.
            //screenManager.AddScreen(new BackgroundScreen(), null);
            //screenManager.AddScreen(new MainMenuScreen(), null);
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
            //*
            Corde b, c;
            ObjetTexture d;
            Bille bille;

            lBille = new ListeObjet(new Rectangle(150, 150, 100, 100));
            lBille.Add(b = new Corde(1.5f, 0.10f, 0.25f, 1.25f));
            lBille.Add(d = (bille = new Bille(1.5f, 0.5f, 0.5f, 0.5f)));
            selectionnable.Add(b);
            b.Joint = JointFactory.CreateRevoluteJoint(SingletonWorld.getInstance().getWorld(), b.Item.Fixture.Body, d.Item.Fixture.Body, new Vector2(b.X - d.X, 0));

            //nomenclature : par ce constructeur le rectangle de obja va contenir les "fils" de l'objet.
            //ces fils s'ettendront proportionnelement à la taille dispo 
            obja = new ListeObjet(new Rectangle(0, 0, 0, 0));
            //toutes les valeurs des rectangles des objets fils indique la taille proportionnel par rapport au père
            obja.Add(b = new Corde(1.10f, 0.75f, 0.25f, 1f)); 
            obja.Add(c = new Corde(2.95f, 0.75f, 0.25f, 1f)); 
            obja.Add(d = new Planche(2f, 1.38f, 2f, 0.25f));
            b.Joint = JointFactory.CreateRevoluteJoint(SingletonWorld.getInstance().getWorld(), b.Item.Fixture.Body, d.Item.Fixture.Body, new Vector2(b.X - d.X, 0));
            c.Joint = JointFactory.CreateRevoluteJoint(SingletonWorld.getInstance().getWorld(), c.Item.Fixture.Body, d.Item.Fixture.Body, new Vector2(c.X - d.X, 0));

            b.Item.Fixture.IgnoreCollisionWith(bille.Item.Fixture);
            c.Item.Fixture.IgnoreCollisionWith(bille.Item.Fixture);

            selectionnable.Add(b);
            selectionnable.Add(c);

            //b.Joint.Enabled = false;
            //c.Joint.Enabled = false;

            //obja.Joint = JointFactory.CreateRevoluteJoint(SingletonWorld.getInstance().getWorld(), c.Item.Fixture.Body, d.Item.Fixture.Body, new Vector2(0, 0));

            objb = new ListeObjet(new Rectangle(40, 70, 80, 80));
            //*
            objb.Add(b = new Corde(1.10f+1, 0.75f+2, 0.25f, 1f));
            objb.Add(c = new Corde(2.95f+1, 0.75f+2, 0.25f, 1f));
            objb.Add(d = new Planche(2f+1, 1.38f+2, 2f, 0.25f));

            b.Joint = JointFactory.CreateRevoluteJoint(SingletonWorld.getInstance().getWorld(), b.Item.Fixture.Body, d.Item.Fixture.Body, new Vector2(b.X - d.X, 0));
            c.Joint = JointFactory.CreateRevoluteJoint(SingletonWorld.getInstance().getWorld(), c.Item.Fixture.Body, d.Item.Fixture.Body, new Vector2(c.X - d.X, 0));

            b.Item.Fixture.IgnoreCollisionWith(bille.Item.Fixture);
            c.Item.Fixture.IgnoreCollisionWith(bille.Item.Fixture);

            selectionnable.Add(b);
            selectionnable.Add(c);

            //b.Joint.Enabled = false;
            //c.Joint.Enabled = false;
            
            //*/
            //ici c'est la racine pour éviter le redimensionnement on utilise le constructeur qui définit un rectangle null (toutes les valeurs sont à 0)
            listeObjet = new ListeObjet("NiveauBanquise", graphics);

            /*lBille = new ListeObjet(new Rectangle(150, 150, 100, 100));
            lBille.Add(b = new Corde(1.5f, 0.10f, 0.25f, 1.25f));
            lBille.Add(d = new Bille(1.5f, 0.5f, 0.5f, 0.5f));
            selectionnable.Add(b);
            b.Joint = JointFactory.CreateRevoluteJoint(SingletonWorld.getInstance().getWorld(), b.Item.Fixture.Body, d.Item.Fixture.Body, new Vector2(b.X - d.X, 0));*/
            //b.Joint.Enabled = false;
            //lBille.Joint = JointFactory.CreateRevoluteJoint(SingletonWorld.getInstance().getWorld(), c.Item.Fixture.Body, b.Item.Fixture.Body, new Vector2(0, 0));
            //lBille.Joint.Enabled = false;
            //b.Desactiver();
            listeObjet.Add(objb);
            listeObjet.Add(obja);
            listeObjet.Add(lBille);
            //selectionnable.Add(b);
            //*/
            /*
            using (StreamWriter wr = new StreamWriter("test.xml"))
            {
                System.Xml.Serialization.XmlSerializer serialiser = new System.Xml.Serialization.XmlSerializer(typeof(ListeObjet));
                serialiser.Serialize(wr, listeObjet);
            }
            //*/

            /*
            using (StreamReader rd = new StreamReader("test.xml"))
            {
                System.Xml.Serialization.XmlSerializer serialiser = new System.Xml.Serialization.XmlSerializer(typeof(ListeObjet));
                listeObjet = serialiser.Deserialize(rd) as ListeObjet;
                listeObjet.Graphics = graphics;
                List<ISelectionnable> tmp = new List<ISelectionnable>();
                listeObjet.getSelectionnable(tmp);
                selectionnable.List = tmp;
            }
            //*/
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
            listeObjet.Initialize(Content);
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
            spriteBatch.DrawString(this.Content.Load<SpriteFont>("gamefont"), "" + gameTime.ElapsedGameTime.TotalSeconds, new Vector2(50, 50), Color.Red);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
