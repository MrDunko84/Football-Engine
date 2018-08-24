using System;
using System.Collections.Generic;
using FM.Core.AI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FM.UI
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Pitch _pitch;
        private SpriteFont _font;
        private readonly Random _rnd = new Random();
        private readonly List<Player> _players = new List<Player> { 
            new Player(2), 
            new Player(3), 
            new Player(4), 
            new Player(5), 
            new Player(6), 
            new Player(7), 
            new Player(8), 
            new Player(9), 
            new Player(10), 
            new Player(11)
        };

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            _pitch = new Pitch(80, 120);
            _players.ForEach((x) => {
                x.SetStartLocation(new Vector2(MovementHelper.Rnd.Next(0, 80 * MovementHelper.Scale),
                                              MovementHelper.Rnd.Next(0, 120 * MovementHelper.Scale)));

                x.SetDestination(new Vector2(MovementHelper.Rnd.Next(0, 80 * MovementHelper.Scale),
                                            MovementHelper.Rnd.Next(0, 120 * MovementHelper.Scale)));
            });

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _font = Content.Load<SpriteFont>("Player");

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {

            base.Draw(gameTime);

            GraphicsDevice.Clear(Color.DarkGreen);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            DrawRegion(_pitch.Region);
            _players.ForEach((x) =>
            {
                UpdatePlayer(x, gameTime);
            });

            _spriteBatch.End();

            
        }

        private void DrawRegion(PitchRegion region)
        {

            var rect = new Rectangle
            {
                X = region.X * MovementHelper.Scale,
                Y = region.Y * MovementHelper.Scale,
                Width = region.Width * MovementHelper.Scale,
                Height = region.Length * MovementHelper.Scale
            };

            var texture2 = new Texture2D(GraphicsDevice, 1, 1);
            texture2.SetData(new Color[] {Color.White});

            const int borderWidth = 1;

            _spriteBatch.Draw(texture2, new Rectangle(rect.Left, rect.Top, borderWidth, rect.Height), Color.Black );
            _spriteBatch.Draw(texture2, new Rectangle(rect.Right, rect.Top, borderWidth, rect.Height), Color.Black );
            _spriteBatch.Draw(texture2, new Rectangle(rect.Left, rect.Top, rect.Width, borderWidth), Color.Black );
            _spriteBatch.Draw(texture2, new Rectangle(rect.Left, rect.Bottom, rect.Width, borderWidth), Color.Black );

            // Draws the region labels
            //_spriteBatch.DrawString(_font, region.ToString(), new Vector2(rect.X + (rect.Width / 2), rect.Y + (rect.Height / 2)), Color.Black);

            

            var inner = region.GetRegions();

            inner?.ForEach(DrawRegion);

            

        }


        private void UpdatePlayer(IMovement player, GameTime gameTime)
        {
            _spriteBatch.DrawString(_font, player.ToString(), player.Location, Color.Black);


            player.SetStartLocation(MovementHelper.PlotPath(player.Location, player.Destination, 0.1f, gameTime));

            if (Math.Abs(player.Location.X - player.Destination.X) <= 0 && Math.Abs(player.Location.Y - player.Destination.Y) <= 0)
            {
                // change detination
                player.SetDestination(new Vector2(MovementHelper.Rnd.Next(0, 80 * MovementHelper.Scale),
                                           MovementHelper.Rnd.Next(0, 120 * MovementHelper.Scale)));

                player.SetSpeed((float)MovementHelper.Rnd.NextDouble());

            }



            player.Update();
        }

    }
}
