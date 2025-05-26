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
            nextID = 1;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Adds an event to the timeline, that will be handled after a set amount of time.
        /// </summary>
        /// <param name="time">The time untill the event is executed/handled, in milisecounds.</param>
        /// <param name="action">The action that is ran once the time has passed.</param>
        /// <returns>The events ID, which is used for removal.</returns>
        public static int AddEvent(float time, Action action)
        {
            TimeLineEvents.Add(nextID, (time, action));
            return nextID++;
        }

        public static float GetTime(int eventID)
        {
            return TimeLineEvents[eventID].time;
        }

        /// <summary>
        /// Removes the event with the given ID.
        /// </summary>
        /// <param name="ID">The given ID.</param>
        /// <returns>true, if successfully removed.</returns>
        public static bool RemoveEvent(int ID)
        {
            return TimeLineEvents.Remove(ID);
        }

        /// <summary>
        /// Updates and event time and handles the events.
        /// </summary>
        /// <param name="gameTime">Gametime object.</param>
        public static void Update(GameTime gameTime)
        {
            Dictionary<int, (float time, Action action)> tempTimeline = new Dictionary<int, (float time, Action action)>();
            foreach (KeyValuePair<int, (float time, Action action)> timeLineEvent in timeLineEvents)
            {
                (float time, Action action) @event = timeLineEvent.Value;
                @event.time -= gameTime.ElapsedGameTime.Milliseconds;
                if (@event.time <= 0)
                {
                    @event.action();
                }
                else
                {
                    tempTimeline.Add(timeLineEvent.Key, @event);
                }
            }
            TimeLineEvents = tempTimeline;
        }
        #endregion
    }
}
