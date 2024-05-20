namespace PTEC_Capstone_Project.Models.ViewModels
{
    public class CreatePostViewModel
    {
        public int GameID { get; set; }
        public string PostDescription { get; set; }
        public int NumberOfPlayers { get; set; }
        public string RankRequired { get; set; }
        public string LevelRequired { get; set; }
    }
}
