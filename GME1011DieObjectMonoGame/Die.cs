using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace GME1011DieObjectMonoGame
{
    internal class Die
    {
        //these are our Die attributes, look over them carefully.
        private int _numSides;
        private int _sideUp;
        private int _x, _y, _speed;
        private bool _moveRight, _moveDown;

        private List<Texture2D> _dieSprites;

        private SoundEffect _dieSound;

        private Texture2D _myCurrentSprite;

        //Our Constructor - set the default values
        public Die(List<Texture2D> dieSprites, SoundEffect dieSound)
        {

            _dieSprites = dieSprites;  //copy the sprite list
            _numSides = _dieSprites.Count;  //use the sprite list to figure out how many sides
            _dieSound = dieSound;   //Oh yeah, sound
            _moveRight = true;  //this is for the DVD bounce
            _moveDown = true;   //also for DVD bounce
            _speed = 3; //default speed
            
        }

        //some quick accessors in case we need them.
        public int GetSideUp() { return _sideUp; }
        public int GetNumSides() { return _numSides; }

        //here's the roll!!
        public void Roll()
        {
            //this part is like our regular Die objects
            Random rng = new Random();
            _sideUp = rng.Next(1, _numSides + 1);

            //this is some fanciness that gets the right sprite from the list
            //...cooool.
            _myCurrentSprite = _dieSprites[_sideUp - 1];

            //every time we roll, change the speed.
            _speed = rng.Next(3, 10);

            //every time we roll play a sound
            _dieSound.Play();
        }

        public void Update(GameTime gameTime)
        {
            //this is mostly movement and DVD bounce code.
            if (_x > 740)
            {
                _moveRight = false;
                Roll();     //hit a wall, roll
            }
            if (_x < 0)
            {
                _moveRight = true;
                Roll();     //hit a wall, roll
            }

            if (_y > 440)
            {
                _moveDown = false;
                Roll();     //hit a wall, roll
            }
            if (_y < 0) { 
                _moveDown = true;
                Roll();     //hit a wall, roll
            }

            //movement code
            if (_moveRight)
                _x+=_speed;
            if(!_moveRight)
                _x-=_speed;
            if (_moveDown)
                _y+=_speed;
            if (!_moveDown)
                _y-=_speed;
        }

        //Here's what our Draw looks like - draw the current sprite at the current location.
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(_myCurrentSprite, new Vector2(_x, _y), Color.White);
            spriteBatch.End();
        }

    }
}
