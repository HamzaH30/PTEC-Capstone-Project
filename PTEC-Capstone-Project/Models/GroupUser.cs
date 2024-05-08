﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTEC_Capstone_Project.Models
{
    public class GroupUser
    {
        // Self Properties
        [Key] public int Id { get; set; }



        // Foreign Key Properties
        [ForeignKey("User")] public int UserID { get; set; }
        [ForeignKey("Group")] public int GroupID { get; set; }
    }
}
