﻿namespace PTEC_Capstone_Project.Models.ViewModels
{
    public class GamePostViewModel
    {
        public int PostID { get; set; }
        public string GameName { get; set; }
        public string UserName {  get; set; }
        public DateTime TimePosted { get; set; }
        public string PostDescription { get; set; }
    }
}
