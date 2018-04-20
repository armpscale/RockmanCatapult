﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rockman.Models;

namespace Rockman.Sprites.Chips
{
    class Recovery : Chip
    {
        Dictionary<string, Rectangle> rectChipRecovImg = new Dictionary<string, Rectangle>()
        {
            {"Recovery10",  new Rectangle(0, 145, 56, 47) },
            {"Recovery30",  new Rectangle(56, 145, 56, 47) },
            {"Recovery50",  new Rectangle(56*2, 145, 56, 47) },
            {"Recovery80",  new Rectangle(56*3, 145, 56, 47) },
            {"Recovery120",  new Rectangle(56*4, 145, 56, 47) },
            {"Recovery150",  new Rectangle(56*5, 145, 56, 47) },
            {"Recovery200",  new Rectangle(56*6, 145, 56, 47) },
            {"Recovery300",  new Rectangle(56*7, 145, 56, 47) },
            {"DarkRecovery",  new Rectangle(168, 336, 56, 47) },
        };
        Dictionary<string, int> recoveryHP = new Dictionary<string, int>()
        {
            {"Recovery10",  10 },
            {"Recovery30",  30 },
            {"Recovery50",  50 },
            {"Recovery80",  80 },
            {"Recovery120",  120 },
            {"Recovery150",  150 },
            {"Recovery200",  200 },
            {"Recovery300",  300 },
            {"DarkRecovery",  1000 },
        };

        public Recovery(Texture2D[] texture) 
            : base(texture)
        {
            _texture = texture;
        }

        public Recovery(Dictionary<string, Animation> animations)
            : base(animations)
        {
            _animations = animations;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            switch (Singleton.Instance.CurrentGameState)
            {
                case Singleton.GameState.GamePlaying:       
                    if(Singleton.Instance.useChipSlotIn.Count != 0 &&
                        Singleton.Instance.useNormalChip == true && 
                        rectChipRecovImg.ContainsKey(Singleton.Instance.useChipSlotIn.Peek()))
                    {
                        SoundEffects["Recovery"].Volume = Singleton.Instance.MasterSFXVolume;
                        SoundEffects["Recovery"].Play();
                        Singleton.Instance.isRecovered = true;
                        if (Singleton.Instance.useChipSlotIn.Peek() == "DarkRecovery")
                        {
                            Singleton.Instance.chooseEmotionPlayer = "DarkEmotion";
                            Singleton.Instance.statusBugHP = true;
                        }
                        if (Singleton.Instance.HeroHP + recoveryHP[Singleton.Instance.useChipSlotIn.Peek()] >= Singleton.Instance.maxHeroHP)
                        {
                            Singleton.Instance.HeroHP = Singleton.Instance.maxHeroHP;
                            Singleton.Instance.useChipSlotIn.Pop();
                        }
                        else Singleton.Instance.HeroHP += recoveryHP[Singleton.Instance.useChipSlotIn.Pop()];
                        Singleton.Instance.useNormalChip = false;
                    }
                    break;
            }
            base.Update(gameTime, sprites);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            switch (Singleton.Instance.CurrentGameState)
            {
                case Singleton.GameState.GameCustomScreen:
                    if (rectChipRecovImg.ContainsKey(chipCustomImg[Singleton.Instance.currentChipSelect.X]))
                    {
                        if (chipCustomImg[Singleton.Instance.currentChipSelect.X] == "DarkRecovery")
                        {
                            Singleton.Instance.chipClass = "Dark";
                        }
                        else
                        {
                            Singleton.Instance.chipClass = "Standard";
                        }
                        Singleton.Instance.chipType = "Normal";
                        //drawChipName
                        spriteBatch.DrawString(Singleton.Instance._font, chipCustomImg[Singleton.Instance.currentChipSelect.X], new Vector2(50, 40), Color.WhiteSmoke, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                        //drawChipImg
                        spriteBatch.Draw(_texture[0], new Vector2(16 * 3, 24 * 3 - 2),
                            rectChipRecovImg[chipCustomImg[Singleton.Instance.currentChipSelect.X]],
                            Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                    }
                    break;
            }
            base.Draw(spriteBatch);
        }
    }
}
