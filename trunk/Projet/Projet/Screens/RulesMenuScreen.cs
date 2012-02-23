#region File Description
//-----------------------------------------------------------------------------
// OptionsMenuScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
#endregion

namespace GameStateManagement
{
    /// <summary>
    /// The options screen is brought up over the top of the main menu
    /// screen, and gives the user a chance to configure the game
    /// in various hopefully useful ways.
    /// </summary>
    class RulesMenuScreen : MenuScreen
    {
        #region Fields
        ContentManager content;
        Texture2D backgroundTexture;

        #endregion
           
        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public RulesMenuScreen()
            : base("Regle")
        {

            MenuEntry back = new MenuEntry("Retour");

            // Hook up menu event handlers.
            back.Selected += OnCancel;
            
            // Add entries to the menu.
            MenuEntries.Add(back);
        }


        #endregion

        #region Handle Input




        #endregion

        public override void LoadContent()
        {
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");

            backgroundTexture = content.Load<Texture2D>("Regles");
        }

        #region Draw

        /// <summary>
        /// Draws the loading screen.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            SpriteFont font = ScreenManager.Game.Content.Load<SpriteFont>("rulesFont");

<<<<<<< .mine
                
=======
            List<String> mess = new List<String>();
            int ligne = 1;
            
            mess.Add("Règle");

            mess.Add("Il vous faut faire tomber la bille bleu");
            mess.Add("dans le panier. Pour cela, il vous faudra");
            mess.Add("résoudre le puzzle qui vous est proposé");
            mess.Add("en supprimant des liaisons au niveau des cordes.");
            mess.Add("Vous pouvez utiliser la souris ou ");
            mess.Add("les touches gauche, droite ainsi que la barre d'espace.");
            mess.Add("Bon Jeu !!!");

            spriteBatch.Begin();

            foreach (String message in mess)
            {
>>>>>>> .r32
                // Center the text in the viewport.
                Viewport viewport = ScreenManager.GraphicsDevice.Viewport;

                Color color = Color.White * TransitionAlpha;
                Rectangle fullscreen = new Rectangle(0, 0, viewport.Width, viewport.Height);

                textPosition.Y = ligne * 50;
                // Draw the text.
                

                spriteBatch.Draw(backgroundTexture, fullscreen,
                                 new Color(TransitionAlpha, TransitionAlpha, TransitionAlpha));


                ligne++;
            }
            spriteBatch.End();
        }
    }
        #endregion
}
