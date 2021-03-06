﻿using C3DE.Components.Renderers;
using Microsoft.Xna.Framework;

namespace C3DE.Components.Colliders
{
    /// <summary>
    /// A sphere collider component used to handle collisions by sphere.
    /// </summary>
    public class SphereCollider : Collider
    {
        private BoundingSphere _sphere;

        /// <summary>
        /// Gets the bounding sphere.
        /// </summary>
        public BoundingSphere Sphere
        {
            get { return _sphere; }
            set { _sphere = value; }
        }

        /// <summary>
        /// Create an empty sphere collider.
        /// </summary>
        public SphereCollider()
            : base()
        {
            _sphere = new BoundingSphere();
        }

        public override void Update()
        {
            if (!sceneObject.IsStatic)
                _sphere.Center = transform.Position;
        }

        public override void Compute()
        {
            var renderable = GetComponent<RenderableComponent>();

            if (renderable != null)
            {
                _sphere = renderable.boundingSphere;
            }
        }

        public override bool Collides(Collider other)
        {
            if (other is SphereCollider)
                return _sphere.Intersects((other as SphereCollider).Sphere);

            if (other is BoxCollider)
                return _sphere.Intersects((other as BoxCollider).Box);

            return false;
        }

        public override float? IntersectedBy(ref Ray ray)
        {
            return ray.Intersects(_sphere);
        }
    }
}
