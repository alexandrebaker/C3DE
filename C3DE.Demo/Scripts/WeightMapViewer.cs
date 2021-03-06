﻿using C3DE.Components;
using C3DE.Components.Renderers;
using C3DE.Materials;
using C3DE.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace C3DE.Demo.Scripts
{
    public class WeightMapViewer : Behaviour
    {
        private TerrainMaterial _material;
        private Rectangle _rect;

        public override void Start()
        {
            var renderer = GetComponent<MeshRenderer>();

            if (renderer == null)
                throw new Exception("You need to attach a mesh renderer first.");
                
            var mat = renderer.Material as TerrainMaterial;

            if (mat == null)
                throw new Exception("You need to use a TerrainMaterial with a non null weightMap.");

            _material = mat;
            _rect = new Rectangle(0, 0, 150, 150);
        }

        public override void OnGUI(GUI gui)
        {
            gui.DrawTexture(_rect, _material.WeightTexture);
        }
    }
}
