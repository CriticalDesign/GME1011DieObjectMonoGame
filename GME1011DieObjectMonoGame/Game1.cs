using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

//for the List
using System.Collections.Generic;

//for the sound effect
using Microsoft.Xna.Framework.Audio;

namespace GME1011DieObjectMonoGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //these are all the attributes for our game. Look them over carefully.
        private Die _myDie;
        private SpriteFont _gameFont;
        private List<Texture2D> _dieSprites;
        private int _numSides;
        private SoundEffect _diceSound;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            //create a list to hold the dice sprites
            _dieSprites = new List<Texture2D>();

            //this will determine how many sides our die has and 
            //which sprites we load from the Content
            _numSides = 9;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //load the font.
            _gameFont = Content.Load<SpriteFont>("GameFont");

            //load the sound.
            _diceSound = Content.Load<SoundEffect>("dice");

            //load all the sprites into this list - this is trickier than loading them one
            //at a time but much more efficient. It only works because the sprites are named
            //in a way that we can use the index (i) for our loading.
            for (int i = 1; i <= _numSides; i++)
                _dieSprites.Add(Content.Load<Texture2D>("" + i));

            //finally, create our Die object. (NOTE: I added the sound as an argument)
            _myDie = new Die(_dieSprites, _diceSound);

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //This is the first way we had the dir roll, but now 
            //it rolls when it hits the screen edges.
            if(Keyboard.GetState().IsKeyDown(Keys.Space))
                _myDie.Roll();

            //in class we made the die move on its own so that we could
            //show off the Die Update method. Look in there to see what happens.
            _myDie.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //our die object is responsible for drawing itself. See the Die class Draw method.
            _myDie.Draw(_spriteBatch);

            base.Draw(gameTime);
        }
    }
}
