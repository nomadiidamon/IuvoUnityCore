using UnityEngine;
using System;
using System.Collections.Generic;
using IuvoUnity.BaseClasses.ECS;
using IuvoUnity.BaseClasses;

namespace IuvoUnity
{
    namespace ECS
    {
        public class NameComponent : IuvoComponentBase
        {
            string _name { get; set; }
        }

        public class DescriptionComponent : IuvoComponentBase
        {
            public string _description { get; set; }
        }

        public class Name_DescriptionComponent : IuvoComponentBase
        {
            public NameComponent _name { get; set; }
            public DescriptionComponent _description { get; set; }
        }

        public class TagComponent : IuvoComponentBase
        {
            string _tag { get; set; }
        }

        public class IDNumberComponent : IuvoComponentBase
        {
            public int _id { get; set; }
        }

        public class IuvoRegistryID : IDNumberComponent
        {

        }

        public class IuvoTimerID : IDNumberComponent
        {

        }


        public class IuvoWorldID : IuvoComponentBase
        {
            public IuvoRegistryID _registryID { get; set; }
            public IuvoTimerID _timerID { get; set; }
            public IuvoEntity _entity { get; set; }
        }

        public class IuvoMin_INT_Component : IuvoComponentBase
        {
            public int _min { get; set; }
        }

        public class IuvoMax_INT_Component : IuvoComponentBase
        {
            public int _max { get; set; }
        }

        public class IuvoMinMax_INT_Component : IuvoComponentBase
        {
            public IuvoMin_INT_Component Minimum { get; set; }
            public IuvoMax_INT_Component Maximum { get; set; }
        }

        public class IuvoMin_FLOAT_Component : IuvoComponentBase
        {
            public float min { get; set; }
        }

        public class IuvoMax_FLOAT_Component : IuvoComponentBase
        {
            public float max { get; set; }
        }

        public class IuvoMinMax_FLOAT : IuvoComponentBase
        {
            public IuvoMin_FLOAT_Component Minimum { get; set; }
            public IuvoMax_FLOAT_Component Maximum { get; set; }
        }

        public class TargetComponent : IuvoComponentBase
        {
            public IuvoEntity _targetEntity { get; set; }
        }

        public class PositionComponent : IuvoComponentBase
        {
            public Vector3 _position { get; set; }
        }

        public class RotationComponent : IuvoComponentBase
        {
            public Quaternion _rotation { get; set; }
        }

        public class EasyRotationComponent : IuvoComponentBase
        {
            public Quaternion _quaternion;
            public Vector3 _eulerAngles;

            public Quaternion Quaternion
            {
                get => _quaternion;
                set
                {
                    _quaternion = value;
                    _eulerAngles = _quaternion.eulerAngles;
                }
            }

            public Vector3 EulerAngles
            {
                get => _eulerAngles;
                set
                {
                    _eulerAngles = value;
                    _quaternion = Quaternion.Euler(_eulerAngles);
                }
            }

            public void ApplyTo(Transform transform)
            {
                transform.rotation = _quaternion;
            }

            public void FromTransform(Transform transform)
            {
                Quaternion = transform.rotation;
            }
        }

        public class ScaleComponent : IuvoComponentBase
        {
            public Vector3 _scale { get; set; }
        }

        public class TransformComponent : IuvoComponentBase
        {
            public PositionComponent Position { get; set; }
            public EasyRotationComponent Rotation { get; set; }
            public ScaleComponent Scale { get; set; }
        }

        public class EasyTransformComponent : IuvoComponentBase
        {
            public PositionComponent _myPosition { get; set; }
            public EasyRotationComponent _myRotation { get; set; }
            public ScaleComponent _myScale { get; set; }
            public ScaleComponent _myWorldScale { get; set; }
        }

        public class PatrolRouteComponent : IuvoComponentBase
        {
            public List<PositionComponent> _positions { get; set; }
        }

        public class VelocityComponent : IuvoComponentBase
        {
            public Vector3 Velocity { get; set; }
        }

        public class InteractionComponent : IuvoComponentBase
        {
            public Action<IuvoEntity> OnInteract { get; set; }
        }

        public class MagnetizeComponent : IuvoComponentBase
        {
            public float _magneticStrength { get; set; }
            public bool _isMagnetic { get; set; }
        }


    }
}
