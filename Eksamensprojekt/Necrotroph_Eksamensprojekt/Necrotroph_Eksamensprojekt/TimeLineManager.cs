using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Necrotroph_Eksamensprojekt
{
    public static class TimeLineManager
    {
        #region Fields
        private static Dictionary<int, (float time, Action action)> timeLineEvents;
        private static int nextID;
        #endregion
        #region Properties
        public static Dictionary<int, (float time, Action action)> TimeLineEvents { get => timeLineEvents; set => timeLineEvents = value; }
        #endregion
        #region Constructors
        static TimeLineManager()
        {
            TimeLineEvents = new Dictionary<int, (float time, Action action)>();
            nextID = 0;

        }
        #endregion
        #region Methods
        /// <summary>
        /// Adds an event to the timeline, that will be handled after a set amount of time.
        /// </summary>
        /// <param name="time">The time untill the event is executed/handled.</param>
        /// <param name="action">The action that is ran once the time has passed.</param>
        public static int AddEvent(float time, Action action)
        {
            TimeLineEvents.Add(nextID, (time, action));
            return nextID++;
        }

        public static void Update(GameTime gameTime)
        {
            Dictionary<int, (float time, Action action)> tempTimeline = new Dictionary<int, (float time, Action action)>();
            foreach (KeyValuePair<int, (float time, Action action)> timeLineEvent in timeLineEvents)
            {
                (float time, Action action) @event = timeLineEvent.Value;
                @event.time -= gameTime.ElapsedGameTime.Milliseconds;
                tempTimeline.Add(timeLineEvent.Key, @event);
            }
            TimeLineEvents = tempTimeline;
        }
        #endregion
    }
}
