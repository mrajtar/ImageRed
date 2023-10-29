using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageRed.Application.Dto
{
    public class RatingDto
    {
        public int RatingId { get; set; }
        public int Value { get; set; }
        public int UserId { get; set; }
    }
}
