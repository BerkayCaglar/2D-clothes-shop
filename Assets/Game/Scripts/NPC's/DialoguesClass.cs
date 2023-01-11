public class DialoguesClass
{
    public class ClothesShopOwnerDialogue
    {
        public int meetingCount = 0;
        public string[] firstMeetingDialogue = new string[]
        {
            "Hello there, welcome to my shop!",
            "I sell clothes, if you want to buy some, take a look at what I have!",
            "Oh...", "And you can also sell your clothes to me, if you want to sell some, let me know!",
            "I hope you enjoy your stay here!",
        };
        public string[] nextMeetingsDialogue = new string[]
        {
            "Hello there, welcome back to my shop!",
            "I hope you are enjoying your stay here!",
        };
    }
    public static ClothesShopOwnerDialogue clothesShopOwnerDialogue = new ClothesShopOwnerDialogue();
}
