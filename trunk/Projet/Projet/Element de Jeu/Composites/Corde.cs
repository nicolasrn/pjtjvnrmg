using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

using Projet.HelperFarseerObject;
using FarseerPhysics.Dynamics.Joints;
using FarseerPhysics.Dynamics;

namespace Projet.Element_de_Jeu.Composites
{
    /// <summary>
    /// Objet concret représentant une Corde
    /// </summary>
    [Serializable]
    public class Corde : ObjetTexture, ISelectionnable
    {
        protected RevoluteJoint joint;
        
        /// <summary>
        /// Constructeur
        /// </summary>
        public Corde(float x, float y, float width, float height)
            : base("corde", x, y, width, height)
        {
        }

        public Corde()
            : base()
        {
        }

        public override void delete()
        {
            base.delete();
            if (joint != null)
            {
                SingletonWorld.getInstance().getWorld().RemoveJoint(joint);
                joint = null;
            }
        }

        [System.Xml.Serialization.XmlIgnore]
        public RevoluteJoint Joint
        {
            get { return joint; }
            set { joint = value; }
        }

        public float BoundsX
        {
            get { return x; }
        }

        public float BoundsY
        {
            get { return y; }
        }

        public float BoundsWidth
        {
            get { return width; }
        }

        public float BoundsHeight
        {
            get { return height; }
        }

        public void Desactiver()
        {
            if (joint != null)
            {
                joint.Enabled = false;
                SingletonWorld.getInstance().getWorld().RemoveJoint(joint);
                joint = null;
            }
        }

        protected override void specialisationInit()
        {
            item = new FarseerObject(
                SingletonWorld.getInstance().getWorld(),
                FarseerObject.FarseerObjectType.Box,
                x,
                y,
                width,
                height,
                new Rectangle(0, 0, 38, 400));
            item.Fixture.Body.BodyType = FarseerPhysics.Dynamics.BodyType.Static;
        }

        public void ignoreCollision(ObjetTexture o)
        {
            this.Item.Fixture.IgnoreCollisionWith(o.Item.Fixture);
        }
    }
}
