using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet.HelperFarseerObject
{
    public class SingletonWorld
    {
        private static SingletonWorld sworld;

        private FarseerPhysics.Dynamics.World world;

        private SingletonWorld()
        {
            world = new FarseerPhysics.Dynamics.World(new Microsoft.Xna.Framework.Vector2(0, 1));
        }

        public static SingletonWorld getInstance()
        {
            if (sworld == null)
                sworld = new SingletonWorld();
            return sworld;
        }

        public FarseerPhysics.Dynamics.World getWorld()
        {
            return world;
        }
    }
}
