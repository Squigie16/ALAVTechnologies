﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LloydStephanieRealty.Models
{
    public class Blog
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateOfPost { get; set; }
        public List<Comment> Comments { get; set; }
        public int ImageID { get; set; }
        [NotMapped]
        public ImageModel Image { get; set; }
    }
}
