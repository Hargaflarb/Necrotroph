using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Necrotroph_Eksamensprojekt.GameObjects;

namespace Necrotroph_Eksamensprojekt.ObjectPools
{
    public class TreePool : ObjectPool
    {
        #region Fields
        private static TreePool instance;
        #endregion
        #region Properties
        public static TreePool Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TreePool();
                }
                return instance;
            }
        }
        #endregion
        #region Constructors
        private TreePool()
        {

        }
        #endregion
        #region Methods
        protected override GameObject Create(Vector2 position)
        {
            Tree newTree = new Tree(position);
            active.Add(newTree);
            GameWorld.Instance.AddObject(newTree);
            return newTree;
        }

        /// <summary>
        /// Gets a tree
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public override GameObject GetObject(Vector2 position)
        {
            if (inactive.OfType<Tree>() != null)
            {
                Tree selected = inactive.OfType<Tree>().FirstOrDefault();
                inactive.Remove(selected);
                active.Add(selected);
                GameWorld.Instance.AddObject(selected);
                selected.Transform.WorldPosition = position;
                selected.Active = true;
                return selected;
            }
            else
            {
                return Create(position);
            }

        }

        /// <summary>
        /// I am not actually sure what this is supposed to do, it just does the same as ReleaseObject
        /// </summary>
        /// <param name="obj"></param>
        protected override void CleanUp(GameObject obj)
        {
            if (active.Contains(obj))
            {
                active.Remove(obj);
                inactive.Add(obj);
                obj.Active = false;
            }
        }
        #endregion
    }
}
