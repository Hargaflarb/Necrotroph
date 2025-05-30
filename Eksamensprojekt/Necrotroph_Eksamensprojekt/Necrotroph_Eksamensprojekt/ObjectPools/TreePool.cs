using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Necrotroph_Eksamensprojekt.GameObjects;
using Necrotroph_Eksamensprojekt.Factories;
using Necrotroph_Eksamensprojekt.Components;
using static System.ComponentModel.Design.ObjectSelectorEditor;

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
        protected override sealed GameObject Create(Vector2 position)
        {
            Tree newTree = TreeFactory.CreateTree(position);
            Active.Add(newTree);
            
            return newTree;
        }

        /// <summary>
        /// Gets a tree
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public override GameObject GetObject(Vector2 position)
        {
            if (Inactive.OfType<Tree>().Any())
            {
                Tree selected = Inactive.OfType<Tree>().FirstOrDefault();
                Inactive.Remove(selected);
                Active.Add(selected);
                selected.Transform.WorldPosition = position;
                selected.Active = true;
                if (Tree.HasEyes)
                {
                    ((Animator)selected.GetComponent<Animator>()).PlayAnimation("Seek");
                }
                else
                {
                    ((Animator)selected.GetComponent<Animator>()).PlayAnimation("Normal");
                }
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
            if (Active.Contains(obj))
            {
                Active.Remove(obj);
                Inactive.Add(obj);
                obj.Active = false;
            }
        }
        #endregion
    }
}
