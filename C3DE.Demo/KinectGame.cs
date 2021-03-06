﻿using C3DE.Demo.Kinect.Scenes;
using C3DE.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace C3DE.Demo
{
    public class KinectGame : Engine
    {
        public static string[] BlueSkybox = new string[6] 
            {
                "Textures/Skybox/bluesky/px",   
                "Textures/Skybox/bluesky/nx",
                "Textures/Skybox/bluesky/py",
                "Textures/Skybox/bluesky/ny",
                "Textures/Skybox/bluesky/pz",
                "Textures/Skybox/bluesky/nz"
            };

        public static GUISkin CreateSkin(ContentManager content)
        {
            GUISkin skin = new GUISkin();
            skin.Box = content.Load<Texture2D>("Textures/UI/grey_panel");
            skin.Buttons[0] = content.Load<Texture2D>("Textures/UI/grey_button00");
            skin.Buttons[1] = content.Load<Texture2D>("Textures/UI/grey_button01");
            skin.Buttons[2] = content.Load<Texture2D>("Textures/UI/grey_button02");
            skin.Checkbox[0] = content.Load<Texture2D>("Textures/UI/grey_box");
            skin.Checkbox[1] = content.Load<Texture2D>("Textures/UI/grey_checkmarkWhite");
            skin.Checkbox[2] = content.Load<Texture2D>("Textures/UI/grey_checkmarkGrey");
            skin.Font = content.Load<SpriteFont>("Font/SegoeUILight");
            skin.TextColor = Color.Black;
            return skin;
        }

        public static void Main()
        {
            using (var game = new Engine("C3DE Kinect Demo", 1680, 1050))
            {
                Screen.ShowCursor = false;
                Application.SceneManager.Add(new KinectTerrainDemo(), true);
                game.Run();
            }
        }
    }
}
