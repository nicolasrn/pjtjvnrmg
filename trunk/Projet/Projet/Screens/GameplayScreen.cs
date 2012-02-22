#region File Description
//-----------------------------------------------------------------------------
// GameplayScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Projet.Jeu;
using Projet.HelperFarseerObject;
using System.IO;
#endregion

namespace GameStateManagement
{
    /// <summary>
    /// This screen implements the actual game logic. It is just a
    /// placeholder to get the idea across: you'll probably want to
    /// put some more interesting gameplay in here!
    /// </summary>
    class GameplayScreen : GameScreen
    {
        #region Fields

        ContentManager content;
        SpriteFont gameFont;

        float pauseAlpha;

        #endregion

        #region GameFields
        private Level level;
        private int levelCourant;
        private List<String> listLevel;
        private List<Texture2D> textureVictoire;

        private Texture2D fondCourant;
        private Rectangle rectangleFondCourant;

        private Rectangle rectangleBille;

        //private SpriteFont font;

        private int time = 2000;
        private int timeTravail;

        private GraphicsDeviceManager graphics;
        #endregion

        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public GameplayScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        /// <summary>
        /// Load graphics content for the game.
        /// </summary>
        public override void LoadContent()
        {
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");

            graphics = ((Projet.Jeu.Game)ScreenManager.Game).graphics;

            gameFont = content.Load<SpriteFont>("gamefont");

            //*
            SingletonWorld.getInstance().getWorld();
            timeTravail = time;

            listLevel = new List<String>();
            textureVictoire = new List<Texture2D>();
            levelCourant = 0;

            foreach (String ligne in File.ReadLines("levels.txt"))
            {
                String[] elements = ligne.Split(';');
                listLevel.Add(elements[0]);
                textureVictoire.Add(content.Load<Texture2D>(elements[1]));
            }

            level = new Level(listLevel[levelCourant]);
            level.LoadContent(content, graphics);
            fondCourant = level.ListeObjet.Texture;
            rectangleFondCourant = level.ListeObjet.Rectangle;

            //*/
            // once the load has finished, we use ResetElapsedTime to tell the game's
            // timing mechanism that we have just finished a very long frame, and that
            // it should not try to catch up.
            ScreenManager.Game.ResetElapsedTime();
        }


        /// <summary>
        /// Unload graphics content used by the game.
        /// </summary>
        public override void UnloadContent()
        {
            content.Unload();
        }

        #endregion

        #region Update and Draw

        /// <summary>
        /// Updates the state of the game. This method checks the GameScreen.IsActive
        /// property, so the game will stop updating when the pause menu is active,
        /// or if you tab away to a different application.
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);

            // Gradually fade in or out depending on whether we are covered by the pause screen.
            if (coveredByOtherScreen)
                pauseAlpha = Math.Min(pauseAlpha + 1f / 32, 1);
            else
                pauseAlpha = Math.Max(pauseAlpha - 1f / 32, 0);

            if (IsActive)
            {
                //*
                level.Update(gameTime);
                rectangleBille = level.ListeObjet.getBille().Item.DestinationRectangle;

                if (level.Etat == Etat.VICTOIRE)
                {
                    //changement de niveau 
                    timeTravail -= gameTime.ElapsedGameTime.Milliseconds;
                    if (timeTravail <= 0)
                    {
                        level.delete();
                        SingletonWorld.getInstance().reset();
                        /*foreach (FarseerPhysics.Dynamics.Body b in SingletonWorld.getInstance().getWorld().BodyList)
                            SingletonWorld.getInstance().getWorld().RemoveBody(b);

                        foreach (FarseerPhysics.Dynamics.Joints.Joint j in SingletonWorld.getInstance().getWorld().JointList)
                            SingletonWorld.getInstance().getWorld().RemoveJoint(j);

                        foreach (FarseerPhysics.Dynamics.BreakableBody j in SingletonWorld.getInstance().getWorld().BreakableBodyList)
                            SingletonWorld.getInstance().getWorld().RemoveBreakableBody(j);

                        foreach (FarseerPhysics.Controllers.Controller j in SingletonWorld.getInstance().getWorld().ControllerList)
                            SingletonWorld.getInstance().getWorld().RemoveController(j);*/

                        //levelCourant = (levelCourant + 1) % listLevel.Count;
                        if (levelCourant + 1 == listLevel.Count)
                        {
                            ScreenManager.AddScreen(new MainMenuScreen(), ControllingPlayer);
                        }
                        else
                        {
                            levelCourant++;
                            level = new Level(listLevel[levelCourant]);
                            level.LoadContent(content, graphics);
                            fondCourant = level.ListeObjet.Texture;
                            timeTravail = time;
                        }
                    }
                }
                else if (level.Etat == Etat.DEFAITE)
                {
                    timeTravail -= gameTime.ElapsedGameTime.Milliseconds;
                    if (timeTravail <= 0)
                    {
                        level.delete();
                        /*foreach (FarseerPhysics.Dynamics.Body b in SingletonWorld.getInstance().getWorld().BodyList)
                            SingletonWorld.getInstance().getWorld().RemoveBody(b);
                        
                        foreach (FarseerPhysics.Dynamics.Joints.Joint j in SingletonWorld.getInstance().getWorld().JointList)
                            SingletonWorld.getInstance().getWorld().RemoveJoint(j);

                        foreach (FarseerPhysics.Dynamics.BreakableBody j in SingletonWorld.getInstance().getWorld().BreakableBodyList)
                            SingletonWorld.getInstance().getWorld().RemoveBreakableBody(j);

                        foreach (FarseerPhysics.Controllers.Controller j in SingletonWorld.getInstance().getWorld().ControllerList)
                            SingletonWorld.getInstance().getWorld().RemoveController(j);*/

                        //relance du jeu ou arrêt bref quelque chose
                        SingletonWorld.getInstance().reset();
                        level = new Level(listLevel[levelCourant]);
                        level.LoadContent(content, graphics);
                        fondCourant = level.ListeObjet.Texture;
                        timeTravail = time;
                    }
                }
                //*/
            }
        }


        /// <summary>
        /// Lets the game respond to player input. Unlike the Update method,
        /// this will only be called when the gameplay screen is active.
        /// </summary>
        public override void HandleInput(InputState input)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            // Look up inputs for the active player profile.
            int playerIndex = (int)ControllingPlayer.Value;

            KeyboardState keyboardState = input.CurrentKeyboardStates[playerIndex];
            GamePadState gamePadState = input.CurrentGamePadStates[playerIndex];

            // The game pauses either if the user presses the pause button, or if
            // they unplug the active gamepad. This requires us to keep track of
            // whether a gamepad was ever plugged in, because we don't want to pause
            // on PC if they are playing with a keyboard and have no gamepad at all!
            bool gamePadDisconnected = !gamePadState.IsConnected &&
                                       input.GamePadWasConnected[playerIndex];

            if (input.IsPauseGame(ControllingPlayer) || gamePadDisconnected)
            {
                ScreenManager.AddScreen(new PauseMenuScreen(), ControllingPlayer);
            }
            else
            {

            }
        }


        /// <summary>
        /// Draws the gameplay screen.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            // This game has a blue background. Why? Because!
            ScreenManager.GraphicsDevice.Clear(ClearOptions.Target,
                                               Color.CornflowerBlue, 0, 0);

            // Our player and enemy are both actually just text strings.
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();

            //*
            if (level.Etat == Etat.VICTOIRE)
            {
                //spriteBatch.DrawString(gameFont, "Good Game", new Vector2(150, 250), Color.Red);
                spriteBatch.Draw(fondCourant, rectangleFondCourant, Color.White);
                spriteBatch.Draw(textureVictoire[levelCourant], rectangleBille, Color.White);
            }
            else if (level.Etat == Etat.DEFAITE)
                spriteBatch.DrawString(gameFont, "Game Over", new Vector2(150, 250), Color.Red);
            else if (level.Etat == Etat.ENCOURS)
            {
                level.Draw(gameTime, spriteBatch);
                //spriteBatch.DrawString(gameFont, "Encours", new Vector2(150, 250), Color.Red);
            }
            //*/
            spriteBatch.End();
        }


        #endregion
    }
}
