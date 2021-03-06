﻿using C3DE.Components;
using Microsoft.Xna.Framework;

namespace C3DE.Demo.Scripts
{
    public class RayPickingTester : Behaviour
    {
        private Scene scene;
        private Camera camera;
        private string _hit;
        private RaycastInfo[] _raycastInfo;

        public override void Start()
        {
            scene = sceneObject.Scene;
            camera = scene.MainCamera;
            _hit = "Nothing";
        }

        public override void Update()
        {
            base.Update();

            if (Input.Mouse.Clicked())
            {
                if (scene.RaycastAll(camera.GetRay(Input.Mouse.Position), 250, out _raycastInfo))
                    _hit = _raycastInfo[0].Collider.SceneObject.Name;
                else
                    _hit = "Nothing";
            }
        }

        public override void OnGUI(UI.GUI gui)
        {
            gui.Box(new Rectangle(10, 10, 150, 30), _hit);
        }
    }
}
