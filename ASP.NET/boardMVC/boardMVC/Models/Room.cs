using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace boardMVC.Models
{
    public class Room
    {
        public int RommId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Speaker> Speakers { get; set; }
    }
    public class Speaker
    {
        public int SpeakerId { get; set; }

        public string Name { get; set; }

        public int RoomId { get; set; }

        public virtual Room Room { get; set; }
    }
}