using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class TodoUpdateDto
    {
        public int TodoId { get; set; }
        public string  Title { get; set; }
        public string  Description { get; set; }
        public DateTime DueDate { get; set; } 
        public string status { get; set; }
    }
}