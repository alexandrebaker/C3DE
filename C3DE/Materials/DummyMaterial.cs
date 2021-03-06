﻿using C3DE.Components.Renderers;
using Microsoft.Xna.Framework.Content;

namespace C3DE.Materials
{
    /// <summary>
    /// A material that does nothing. It can be used for custom debug renderable object.
    /// </summary>
    public sealed class DummyMaterial : Material
    {
        public DummyMaterial(Scene scene)
            : base(scene)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void PrePass()
        {
        }

        public override void Pass(RenderableComponent renderable)
        {    
        }
    }
}
