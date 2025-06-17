using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace Necrotroph_Eksamensprojekt
{
    /// <summary>
    /// Malthe
    /// </summary>
    public static class TimeLineManager
    {
        #region Fields
        private static Dictionary<int, (float time, Action action)> timeLineEvents;
        private static Dictionary<int, (float time, Action action)> eventsToAdd;
        private static Dictionary<int, (float time, Action action)> eventsToRemove;
        private static int nextID;
        #endregion
        #region Properties
        public static Dictionary<int, (float time, Action action)> TimeLineEvents { get => timeLineEvents; set => timeLineEvents = value; }
        #endregion
        #region Constructors
        static TimeLineManager()
        {
            TimeLineEvents = new Dictionary<int, (float time, Action action)>();
            eventsToAdd = new Dictionary<int, (float time, Action action)>();
            eventsToRemove = new Dictionary<int, (float time, Action action)>();
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
            eventsToAdd.Add(nextID, (time, action));
            nextID++;
            return nextID-1;
        }

        /// <summary>
        /// Emma
        /// </summary>
        /// <param name="eventID"></param>
        /// <returns></returns>
        public static float GetTime(int eventID)
        {
            if (timeLineEvents.ContainsKey(eventID))
            {
                return TimeLineEvents[eventID].time;
            }
            else if (eventsToAdd.ContainsKey(eventID))
            {
                return eventsToAdd[eventID].time;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// Removes the event with the given ID.
        /// </summary>
        /// <param name="ID">The given ID.</param>
        /// <returns>true, if successfully removed.</returns>
        public static bool RemoveEvent(int ID)
        {
            if (timeLineEvents.ContainsKey(ID))
            {
                eventsToRemove.Add(ID, (timeLineEvents[ID]));
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Updates and event time and handles the events.
        /// </summary>
        /// <param name="gameTime">Gametime object.</param>
        public static void Update(GameTime gameTime)
        {
            //Malthe
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

            //Emma
            foreach (KeyValuePair<int, (float time, Action action)> timeLineEvent in eventsToAdd)
            {
                timeLineEvents.Add(timeLineEvent.Key,timeLineEvent.Value);
            }
            eventsToAdd.Clear();

            foreach (KeyValuePair<int, (float time, Action action)> timeLineEvent in eventsToRemove)
            {
                timeLineEvents.Remove(timeLineEvent.Key);
            }
            eventsToRemove.Clear();
        }
        #endregion
    }
}
