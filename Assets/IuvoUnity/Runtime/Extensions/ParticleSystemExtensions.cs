using UnityEngine;

namespace IuvoUnity
{
    namespace Extensions
    {
        /// <summary>
        /// Extension methods for Unity's ParticleSystem component to simplify configuration.
        /// </summary>
        public static class ParticleSystemExtensions
        {
            /// <summary>
            /// Sets the emission rate over time.
            /// </summary>
            /// <param name="ps">The particle system to configure.</param>
            /// <param name="emissionRate">The desired emission rate.</param>
            public static void WithEmissionRate(this ParticleSystem ps, float emissionRate)
            {
                if (ps == null) return;
                var emission = ps.emission;
                emission.rateOverTime = new ParticleSystem.MinMaxCurve(emissionRate);
            }

            /// <summary>
            /// Sets the color over lifetime using a gradient.
            /// </summary>
            /// <param name="ps">The particle system to configure.</param>
            /// <param name="gradient">The gradient to apply over the particle's lifetime.</param>
            public static void WithColorOverLifetime(this ParticleSystem ps, Gradient gradient)
            {
                if (ps == null || gradient == null) return;
                var col = ps.colorOverLifetime;
                col.enabled = true;
                col.color = gradient;
            }

            /// <summary>
            /// Sets the size over lifetime using an animation curve.
            /// </summary>
            /// <param name="ps">The particle system to configure.</param>
            /// <param name="curve">The curve defining particle size over time.</param>
            public static void WithSizeOverLifetime(this ParticleSystem ps, AnimationCurve curve)
            {
                if (ps == null || curve == null) return;
                var size = ps.sizeOverLifetime;
                size.enabled = true;
                size.size = new ParticleSystem.MinMaxCurve(1, curve);
            }

            /// <summary>
            /// Sets the start color of particles.
            /// </summary>
            /// <param name="ps">The particle system to configure.</param>
            /// <param name="color">The color to start with.</param>
            public static void WithStartColor(this ParticleSystem ps, Color color)
            {
                if (ps == null) return;
                var main = ps.main;
                main.startColor = color;
            }

            /// <summary>
            /// Sets the start size of particles.
            /// </summary>
            /// <param name="ps">The particle system to configure.</param>
            /// <param name="size">The size to start with.</param>
            public static void WithStartSize(this ParticleSystem ps, float size)
            {
                if (ps == null) return;
                var main = ps.main;
                main.startSize = size;
            }

            /// <summary>
            /// Sets the start lifetime of particles.
            /// </summary>
            /// <param name="ps">The particle system to configure.</param>
            /// <param name="lifetime">The lifetime of each particle in seconds.</param>
            public static void WithStartLifetime(this ParticleSystem ps, float lifetime)
            {
                if (ps == null) return;
                var main = ps.main;
                main.startLifetime = lifetime;
            }

            /// <summary>
            /// Sets the start speed of particles.
            /// </summary>
            /// <param name="ps">The particle system to configure.</param>
            /// <param name="speed">The speed to start with.</param>
            public static void WithStartSpeed(this ParticleSystem ps, float speed)
            {
                if (ps == null) return;
                var main = ps.main;
                main.startSpeed = speed;
            }

            /// <summary>
            /// Sets whether the particle system should loop.
            /// </summary>
            /// <param name="ps">The particle system to configure.</param>
            /// <param name="loop">True to enable looping; otherwise, false.</param>
            public static void WithLooping(this ParticleSystem ps, bool loop)
            {
                if (ps == null) return;
                var main = ps.main;
                main.loop = loop;
            }

            /// <summary>
            /// Sets the duration of the particle system.
            /// </summary>
            /// <param name="ps">The particle system to configure.</param>
            /// <param name="duration">The duration in seconds.</param>
            public static void WithDuration(this ParticleSystem ps, float duration)
            {
                if (ps == null) return;
                var main = ps.main;
                main.duration = duration;
            }

            /// <summary>
            /// Sets the shape of the particle system's emission area.
            /// </summary>
            /// <param name="ps">The particle system to configure.</param>
            /// <param name="shapeType">The shape to use for emission.</param>
            public static void WithShape(this ParticleSystem ps, ParticleSystemShapeType shapeType)
            {
                if (ps == null) return;
                var shape = ps.shape;
                shape.enabled = true;
                shape.shapeType = shapeType;
            }

            /// <summary>
            /// Enables collision and sets the layers the particles should collide with.
            /// </summary>
            /// <param name="ps">The particle system to configure.</param>
            /// <param name="collisionMask">The layers to collide with.</param>
            public static void WithCollision(this ParticleSystem ps, LayerMask collisionMask)
            {
                if (ps == null) return;
                var collision = ps.collision;
                collision.enabled = true;
                collision.type = ParticleSystemCollisionType.World;
                collision.collidesWith = collisionMask;
            }

            /// <summary>
            /// Enables trails on particles and sets the trail lifetime ratio.
            /// </summary>
            /// <param name="ps">The particle system to configure.</param>
            /// <param name="lifetimeRatio">The ratio of the particle lifetime used for trails (0 to 1).</param>
            public static void WithTrails(this ParticleSystem ps, float lifetimeRatio = 1f)
            {
                if (ps == null) return;
                var trails = ps.trails;
                trails.enabled = true;
                trails.lifetime = lifetimeRatio;
            }

            /// <summary>
            /// Enables noise and sets the strength of turbulence affecting the particles.
            /// </summary>
            /// <param name="ps">The particle system to configure.</param>
            /// <param name="strength">The strength of the noise module.</param>
            public static void WithNoise(this ParticleSystem ps, float strength)
            {
                if (ps == null) return;
                var noise = ps.noise;
                noise.enabled = true;
                noise.strength = strength;
            }

            /// <summary>
            /// Sets a constant velocity over the lifetime of each particle.
            /// </summary>
            /// <param name="ps">The particle system to configure.</param>
            /// <param name="velocity">The velocity vector to apply.</param>
            public static void WithVelocityOverLifetime(this ParticleSystem ps, Vector3 velocity)
            {
                if (ps == null) return;
                var velocityModule = ps.velocityOverLifetime;
                velocityModule.enabled = true;
                velocityModule.x = velocity.x;
                velocityModule.y = velocity.y;
                velocityModule.z = velocity.z;
            }

            /// <summary>
            /// Adds a sub-emitter to the particle system.
            /// </summary>
            /// <param name="ps">The parent particle system.</param>
            /// <param name="subEmitter">The sub-emitter to add.</param>
            /// <param name="type">The trigger type for the sub-emitter (e.g., Birth, Death).</param>
            /// <param name="properties">The properties to inherit from the parent.</param>
            public static void WithSubEmitter(this ParticleSystem ps, ParticleSystem subEmitter, ParticleSystemSubEmitterType type = ParticleSystemSubEmitterType.Birth, ParticleSystemSubEmitterProperties properties = ParticleSystemSubEmitterProperties.InheritNothing)
            {
                if (ps == null || subEmitter == null) return;
                var subEmitters = ps.subEmitters;
                subEmitters.AddSubEmitter(subEmitter, type, properties);
            }
        }
    }
}
