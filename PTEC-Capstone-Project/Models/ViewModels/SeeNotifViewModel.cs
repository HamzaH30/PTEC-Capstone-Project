namespace PTEC_Capstone_Project.Models.ViewModels
{
    public class SeeNotifViewModel
    {
        public int postID {  get; set; }
        public string recieverID { get; set; }
        public string senderID { get; set; }
        public string userName { get; set; }
        public string type { get; set; }
        public DateTime timstamp { get; set; }
        public bool isRead { get; set; }
    }
}
