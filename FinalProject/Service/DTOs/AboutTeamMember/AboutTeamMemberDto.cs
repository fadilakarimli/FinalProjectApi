﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.AboutTeamMember
{
    public class AboutTeamMemberDto
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Position { get; set; }
        public string FullName { get; set; }
        public string About { get; set; }
    }
}
