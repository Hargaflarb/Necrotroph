using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
        protected override GameObject Create()
        {
            throw new NotImplementedException();
        }
        public override GameObject GetObject(Vector2 position)
        {
            if ()
            {

            }
            else
            {

            }
            return null;
        }
        protected override void CleanUp(GameObject obj)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
