using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Projet.Element_de_Jeu;
using Projet.Element_de_Jeu.Composites;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Projet.HelperFarseerObject;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using FarseerPhysics.Dynamics;

namespace Projet.Jeu
{
    public enum Etat
    {
        ENCOURS,
        VICTOIRE,
        DEFAITE
    }

    public class Level
    {
        private BarreDeChargement barre;
        private Bille bille;
        private Sol sol;
        private ListeObjet listeObjet;
        private Selectionnable selectionnable;
        private Boolean victoireDelai, victoireCollision, victoireNiveau;

        private KeyboardState lastKeyboardState;
        private KeyboardState currentKeyboardState;

        private String name;

        private Etat etat;

        private Texture2D texSouris;

        public Level(String name)
        {
            this.name = name;

            victoireDelai = false;
            victoireCollision = false;
            victoireNiveau = false;
        }

        public Etat Etat
        {
            get { return etat; }
        }

        public ListeObjet ListeObjet
        {
            get { return listeObjet; }
        }

        public void LoadContent(ContentManager content, GraphicsDeviceManager graphics)
        {
            barre = new BarreDeChargement();
            selectionnable = new Selectionnable();
            etat = Etat.ENCOURS;

            texSouris = content.Load<Texture2D>("gradient");

            barre.init(content);
            loadLevel(name, graphics, content);
            selectionnable.init(content);
        }

        public void Update(GameTime gameTime)
        {
            lastKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            if (lastKeyboardState.IsKeyUp(Keys.Right) && currentKeyboardState.IsKeyDown(Keys.Right))
                selectionnable.suivant();
            if (lastKeyboardState.IsKeyUp(Keys.Left) && currentKeyboardState.IsKeyDown(Keys.Left))
                selectionnable.precedant();
            if (lastKeyboardState.IsKeyUp(Keys.Space) && currentKeyboardState.IsKeyDown(Keys.Space))
                selectionnable.desactiver();

            foreach (ISelectionnable sel in selectionnable)
            {
                Rectangle r = new Rectangle((int)sel.BoundsX, (int)sel.BoundsY, (int)sel.BoundsWidth, (int)sel.BoundsHeight);
                Rectangle s = new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 5, 5);
                if (r.Intersects(s))
                {
                    selectionnable.Courant = sel;
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                        selectionnable.desactiver();
                    break;
                }
            }

            SingletonWorld.getInstance().getWorld().Step((float)gameTime.ElapsedGameTime.TotalSeconds);

            listeObjet.Update();
            barre.run(gameTime);

            Rectangle a = new Rectangle(bille.Item.BoxRatated.X, bille.Item.BoxRatated.Y, bille.Item.BoxRatated.Width, bille.Item.BoxRatated.Height);
            Rectangle b = new Rectangle(sol.Item.BoxRatated.X, sol.Item.BoxRatated.Y, sol.Item.BoxRatated.Width, sol.Item.BoxRatated.Height);

            victoireDelai = !barre.TimeOver;
            if (victoireDelai && victoireCollision) //verification victoire
            {
                victoireNiveau = true;
                barre.Stop();
                etat = Etat.VICTOIRE;
            }
            else if (a.Intersects(b) || victoireDelai == false) //verification defaite
            {
                victoireNiveau = false;
                barre.Stop();
                etat = Etat.DEFAITE;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // TODO : ajouter le code de dessin ici

            listeObjet.Dessin(spriteBatch);
            selectionnable.dessiner(spriteBatch);
            barre.dessiner(spriteBatch);
            spriteBatch.Draw(texSouris, new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 5, 5), Color.White);
            /*
            if (victoireNiveau)
            {
                //spriteBatch.DrawString(font, "Good Game", new Vector2(150, 250), Color.Red);
                //nextLevel if it's not the last
            }
            else if (!victoireDelai)
            {
                //spriteBatch.DrawString(font, "Game Over", new Vector2(150, 250), Color.Red);
                //restart for the biginning or quit
            }*/
        }

        public Boolean JeuGagne
        {
            get { return victoireNiveau; }
        }

        public Boolean JeuPerdu
        {
            get { return !victoireNiveau; }
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
        private void loadLevel(String name, GraphicsDeviceManager graphics, ContentManager content)
        {
            using (StreamReader rd = new StreamReader(name + ".xml"))
            {
                try
                {
                    System.Xml.Serialization.XmlSerializer serialiser = new System.Xml.Serialization.XmlSerializer(typeof(ListeObjet));
                    listeObjet = serialiser.Deserialize(rd) as ListeObjet;
                    listeObjet.Graphics = graphics;
                    listeObjet.Initialize(content);

                    foreach (ObjetCompositeAbstrait o in listeObjet.List)
                        (o as ListeObjet).lier();
                    bille = listeObjet.getBille();
                    foreach (ObjetCompositeAbstrait o in listeObjet.List)
                        (o as ListeObjet).ignoreCollisionWith(bille);

                    List<ISelectionnable> tmp = new List<ISelectionnable>();
                    listeObjet.getSelectionnable(tmp);
                    selectionnable.List = tmp;

                    sol = listeObjet.getSol();
                    bille.Detail();
                    listeObjet.getPanier().Detail();
                    bille.Item.Fixture.Body.OnCollision += new OnCollisionEventHandler(OnCollisionDetectVictory);

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

        public void delete()
        {
            listeObjet.delete();
        }
    }
}
