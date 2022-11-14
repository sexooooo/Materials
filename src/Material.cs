using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DuckGame;

[assembly: AssemblyTitle("RainbowMaterial")]
[assembly: AssemblyDescription("|RED|.|ORANGE|.|YELLOW|.|GREEN|.|DGBLUE|.|BLUE|.|PURPLE|.")]
[assembly: AssemblyCompany("Автор")]

[assembly: AssemblyCopyright("Copyright ©  2021")]
[assembly: AssemblyProduct("")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: AssemblyConfiguration("")]

[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]


namespace DuckGame.src
{
    public class ModName : DisabledMod
    {
        public override Priority priority
        {
            get { return base.priority; }
        }

        protected override void OnPreInitialize()
        {
            base.OnPreInitialize();
        }

        protected override void OnPostInitialize()
        {
            (typeof(Game).GetField("updateableComponents", BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic).GetValue(MonoMain.instance) as List<IUpdateable>).Add(new ModUpdate());
            (typeof(Game).GetField("drawableComponents", BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic).GetValue(MonoMain.instance) as List<IDrawable>).Add(new DrawMod());

            base.OnPostInitialize();
        }
    }

    internal class ModUpdate : IUpdateable
    {
        public bool Enabled
        {
            get
            {
                return true;
            }
        }

        public int UpdateOrder
        {
            get
            {
                return 1;
            }
        }


        public event EventHandler<EventArgs> EnabledChanged;
        public event EventHandler<EventArgs> UpdateOrderChanged;
        public void Update(GameTime gameTime)
        {
            Classes.Update();
        }
    }

    internal class DrawMod : IDrawable
    {
        public bool Visible
        {
            get
            {
                return true;
            }
        }

        public int DrawOrder
        {
            get
            {
                return 1;
            }
        }

        public event EventHandler<EventArgs> VisibleChanged;
        public event EventHandler<EventArgs> DrawOrderChanged;

        public void Draw(GameTime gameTime)
        {
            Graphics.screen.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Matrix.Identity);
            Classes.Draw();
            Graphics.screen.End();
        }
    }

    public class Classes
    {
        public static void Update()
        {
        if (Keyboard.Pressed(Keys.RightAlt))
        {
              w = !w;
            }
        }

        public static void Draw()
        {
            if (w)
            {
                foreach (Holdable holdable in Level.CheckCircleAll<Holdable>(new Vec2(0f, 0f), 15000f))
                {
                    if (!(holdable.material is MaterialGrid))
                    {
                        holdable.material = new MaterialGrid(holdable);
                    }
                }
            }
                if (!w)
                {
                    foreach (Holdable holdable in Level.CheckCircleAll<Holdable>(new Vec2(0f, 0f), 15000f))
                    {
                    { 
                            holdable.material = null;
                        }
                    }
               }
            }
        public static bool w;
    }
}
