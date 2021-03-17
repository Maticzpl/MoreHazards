using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Features;

namespace MoreHazards
{

    public class RandomTiming
    {
        public RandomTiming()
        {
            MinDuration = 10;
            MaxDuration = 30;

            MinCooldown = 120;
            MaxCooldown = 300;
        }
        public RandomTiming(int minDuration, int maxDuration, int minCooldown, int maxCooldown)
        {
            MinDuration = minDuration;
            MaxDuration = maxDuration;

            MinCooldown = minCooldown;
            MaxCooldown = maxCooldown;
        }

        [Description("Min Duration of the event")]
        public int MinDuration { get; set; }

        [Description("Max Duration of the event")]
        public int MaxDuration { get; set; }

        [Description("Min Cooldown between calling this event")]
        public int MinCooldown { get; set; }

        [Description("Max Cooldown between calling this event")]
        public int MaxCooldown { get; set; }

        public int GetDuration()
        {
            return UnityEngine.Random.Range(MinDuration, MaxDuration);
        }
        public int GetCooldown()
        {
            return UnityEngine.Random.Range(MinCooldown, MaxCooldown);
        }
    }
    public class RandomInterval
    {
        public RandomInterval()
        {
            MinDelay = 120;
            MaxDelay = 300;
        }
        public RandomInterval( int min, int max)
        {
            MinDelay = min;
            MaxDelay = max;
        }

        [Description("Min Delay between calling this event")]
        public int MinDelay { get; set; }

        [Description("Max Delay between calling this event")]
        public int MaxDelay { get; set; }

        public int GetInterval()
        {
            return UnityEngine.Random.Range(MinDelay, MaxDelay);
        }
    }
    public class CassieAnnouncement
    {

        [Description("Cassie Message Text")]
        public string Text { get; set; } = "";

        [Description("Cassie Message Glitch Chance (0-100)")]
        public int Glitches { get; set; } = 10;

        [Description("Cassie Message Jam Chance (0-100)")]
        public int Jams { get; set; } = 10;

        public void Speak()
        {
            Cassie.GlitchyMessage(Text, Glitches, Jams);
        }
    }

    public class CollectionUtils<TValue>
    {
        public static TValue GetRandomElement(ICollection<TValue> collection)
        {
            int index = UnityEngine.Random.Range(0,collection.Count-1);
            return collection.ElementAt(index);
        }
        public static TValue GetRandomElement(IEnumerable<TValue> enumerable)
        {
            var collection = (enumerable as IReadOnlyList<TValue>) ?? enumerable.ToArray();
            int index = UnityEngine.Random.Range(0, collection.Count - 1);
            return collection[index];
        }
    }
}
