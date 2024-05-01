using System;
using System.ComponentModel.DataAnnotations;

namespace ChatApp.Models.GeneralMessages
{
    public class GeneralMessageDTO
    {
        public int Id { get; set; }
        public string Role { get; set; }

        public string Desc { get; set; }

        public int PayPerHour { get; set; }

        public int AreaId { get; set; }

        public string AreaName { get; set; }

        public DateTime CreatedAt { get; set; }

        public string MessageFromUsername { get; set; }

        public string MessageFromUrl { get; set; }
    }
}
