using IuvoUnity.StateMachines.HSM;
using IuvoUnity.DataStructs;
using UnityEngine;


namespace IuvoUnity
{
    namespace Statemachines
    {
        namespace HSM
        {
            public class PlayerRoot : State
            {
                public PlayerContext ctx;
                public readonly Grounded Grounded;
                public readonly Airborne Airborne;

                public PlayerRoot(StateMachine m) : base(m, null)
                {
                    Grounded = new Grounded(m, this, ctx);
                    Airborne = new Airborne(m, this, ctx);
                }
                protected override State GetInitialState() => Grounded;
                protected override State GetTransition() => ctx.isGrounded ? null : Airborne;

            }

            #region Grounded States
            public class Grounded : State
            {
                public PlayerContext ctx;
                public readonly Idle Idle;
                public readonly Move Move;
                public Grounded(StateMachine m, State parent, PlayerContext context) : base(m, parent)
                {
                    Idle = new Idle(m, this, context);
                    Move = new Move(m, this, context);
                    ctx = context;
                }

                protected override State GetInitialState() => Idle;
                protected override State GetTransition()
                {

                    return ctx.isGrounded ? null : ((PlayerRoot)Parent).Airborne;

                }
            }

            public class Idle : State
            {
                public PlayerContext ctx;
                public Idle(StateMachine m, State parent, PlayerContext context) : base(m, parent) 
                {
                    ctx = context;
                }

                protected override State GetTransition() => Mathf.Abs(ctx.move.x) > 0.01f ? ((Grounded)Parent).Move : null;

                protected override void OnEnter()
                {
                    ctx.velocity = Vector3.zero;
                }
            }

            public class Move : State
            {
                public PlayerContext ctx;
                public Move(StateMachine m, State parent, PlayerContext context) : base(m, parent) 
                {
                    ctx = context;
                }

                protected override State GetTransition()
                {
                    if (!ctx.isGrounded) return ((PlayerRoot)Parent).Airborne;

                    return Mathf.Abs(ctx.move.x) < 0.01f ? ((Grounded)Parent).Idle : null;
                }

                protected override void OnUpdate(float deltaTime)
                {
                    var target = ctx.move.x * ctx.moveSpeed;
                    ctx.velocity.x = Mathf.MoveTowards(ctx.velocity.x, target, ctx.acceleration * deltaTime);
                }
            }

            #endregion

            #region Aitrborne States
            public class Airborne : State 
            {
                public PlayerContext ctx;
                public readonly Falling Falling;
                public readonly Jumping Jumping;
                public readonly Flying Flying;

                public Airborne(StateMachine m, State parent, PlayerContext context) : base(m, parent)
                {
                    Falling = new Falling(m, this);
                    Jumping = new Jumping(m, this);
                    Flying = new Flying(m, this);
                    ctx = context;
                }

                protected override State GetTransition() => ctx.isGrounded ? ((PlayerRoot)Parent).Grounded : null;
                protected override void OnEnter()
                {

                }
            }

            public class Jumping : State
            {
                public PlayerContext ctx;
                public readonly Falling Falling;
                public Jumping(StateMachine m, State parent) : base(m, parent) { }
            }

            public class Flying : State
            {
                public PlayerContext ctx;
                public readonly Falling Falling;
                public Flying(StateMachine m, State parent) : base(m, parent) { }
            }

            public class Falling : State
            {
                public PlayerContext ctx;

                public readonly Landing Landing;
                public readonly DoubleJump DoubleJump;
                public Falling(StateMachine m, State parent) : base(m, parent) { }
            }

            public class DoubleJump : State
            {
                public PlayerContext ctx;
                public readonly Landing Landing;

                public DoubleJump(StateMachine m, State parent) : base(m, parent) { }
            }

            public class Landing : State
            {
                public PlayerContext ctx;
                public Landing(StateMachine m, State parent) : base(m, parent) { }
            }
            #endregion

        }
    }
}

